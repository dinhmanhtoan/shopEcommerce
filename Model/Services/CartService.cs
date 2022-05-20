namespace Model.Services;

public class CartService : ICartService
{
    private readonly IRepository<Cart> _cartRepository;
    private readonly IRepository<CartItem> _cartItemRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IMediaService _mediaService;
    private readonly ICouponService _couponService;
    private readonly ICurrencyService _currencyService;
    public CartService(
         IRepository<Cart> cartRepository,IRepository<CartItem> cartItemRepository, IRepository<Product> productRepository,
    IMediaService mediaService,ICouponService couponService, ICurrencyService currencyService)
    {
        _cartRepository = cartRepository;
        _cartItemRepository = cartItemRepository;
        _productRepository = productRepository;
        _mediaService = mediaService;
        _couponService = couponService;
        _currencyService = currencyService;
    }
    public async Task<Result> AddToCart(long customerId, long productId, int quantity)
    {
        return await AddToCart(customerId, customerId, productId, quantity);
    }

    public async Task<Result> AddToCart(long customerId, long createdById, long productId , int quantity)
    {
        var cart = await GetActiveCart(customerId, createdById);
        if (cart == null)
        {
            cart = new Cart
            {
                CustomerId = customerId,
                CreatedById = createdById,
                IsProductPriceIncludeTax = true
            };
            _cartRepository.Add(cart);
        }
        else
        {
            if (cart.LockedOnCheckout)
            {
                return Result.Fail("Cart is locked for checkout. Please complete the checkout first.");
            }

            cart = await _cartRepository.Query().Include(x => x.cartItems).FirstOrDefaultAsync(x => x.Id == cart.Id);
        }

        var cartItem = cart.cartItems.FirstOrDefault(x => x.ProductId == productId);
        if (cartItem == null)
        {
            cartItem = new Models.CartItem
            {
                Cart = cart,
                ProductId = productId,
                Quantity = quantity,
                CreatedOn = DateTimeOffset.Now
            };
                
            cart.cartItems.Add(cartItem);
            // stock can sua 
            var product = await _productRepository.Query().FirstOrDefaultAsync(x => x.Id == cartItem.ProductId);
            if (product.StockTrackingIsEnabled && product.StockQuantity < quantity)
            {
                return Result.Fail($"There are only {cartItem.Product.StockQuantity} items available for {cartItem.Product.Name}");
            }
        }
        else
        {
                cartItem.Quantity = cartItem.Quantity + quantity;
            if (cartItem.Product.StockTrackingIsEnabled && cartItem.Product.StockQuantity < cartItem.Quantity)
            {
                return Result.Fail($"There are only {cartItem.Product.StockQuantity} items available for {cartItem.Product.Name}");
            }
           
        }
        await _cartRepository.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Cart> GetActiveCart(long customerId)
    {
        return  await GetActiveCart(customerId, customerId);
    }

    public async Task<Cart> GetActiveCart(long customerId, long createdById)
    {
        return await _cartRepository.Query()
            .Include(x => x.cartItems).ThenInclude(x=> x.Product)
            .Where(x => x.CustomerId == customerId && x.CreatedById == createdById && x.IsActive).FirstOrDefaultAsync();
    }
    public async Task<CartVm> GetActiveCartDetails(long customerId)
    {
        return await GetActiveCartDetails(customerId, customerId);
    }

    // TODO separate getting product thumbnail, varation options from here
    public async Task<CartVm> GetActiveCartDetails(long customerId, long createdById)
    {
        var cart = await GetActiveCart(customerId, createdById);
        if (cart == null)
        {
            return null;
        }

        var cartVm = new CartVm(_currencyService)
        {
            Id = cart.Id,
            CouponCode = cart.CouponCode,
            IsProductPriceIncludeTax = cart.IsProductPriceIncludeTax,
            TaxAmount = cart.TaxAmount,
            ShippingAmount = cart.ShippingAmount,
            OrderNote = cart.OrderNote,
            LockedOnCheckout = cart.LockedOnCheckout
        };

        cartVm.Items = _cartItemRepository.Query()
            .Include(x => x.Product).ThenInclude(p => p.Thumbnail)
            .Include(x => x.Product).ThenInclude(p => p.OptionCombinations).ThenInclude(o => o.Option)
            .Where(x => x.CartId == cart.Id).ToList()
            .Select(x => new CartItemVm(_currencyService)
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                ProductPrice = x.Product.Price,
                ProductStockQuantity = x.Product.StockQuantity,
                ProductStockTrackingIsEnabled = x.Product.StockTrackingIsEnabled,
                IsProductAvailabeToOrder = x.Product.IsAllowToOrder && x.Product.IsPublished && !x.Product.IsDeleted,
                ProductImage = _mediaService.GetThumbnailUrl(x.Product.Thumbnail),
                Quantity = x.Quantity,
                VariationOptions = CartItemVm.GetVariationOption(x.Product)
            }).ToList();

        cartVm.SubTotal = cartVm.Items.Sum(x => x.Quantity * x.ProductPrice);
        if (!string.IsNullOrWhiteSpace(cartVm.CouponCode))
        {
            var cartInfoForCoupon = new CartInfoForCoupon()
            {
                Items = cartVm.Items.Select(x => new CartItemForCoupon { ProductId = x.ProductId, Quantity = x.Quantity }).ToList()
            };
            var couponValidationResult = await _couponService.Validate(customerId, cartVm.CouponCode, cartInfoForCoupon);
            if (couponValidationResult.Succeeded)
            {
                cartVm.Discount = couponValidationResult.DiscountAmount;
            }
            else
            {
                cartVm.CouponValidationErrorMessage = couponValidationResult.ErrorMessage;
            }
        }

        return cartVm;
    }

    public async Task<CouponValidationResult> ApplyCoupon(long cartId, string couponCode)
    {
        var cart = _cartRepository.Query().Include(x => x.cartItems).FirstOrDefault(x => x.Id == cartId);

        var cartInfoForCoupon = new CartInfoForCoupon
        {
            Items = cart.cartItems.Select(x => new CartItemForCoupon { ProductId = x.ProductId, Quantity = x.Quantity }).ToList()
        };
        var couponValidationResult = await _couponService.Validate(cart.CustomerId, couponCode, cartInfoForCoupon);
        if (couponValidationResult.Succeeded)
        {
            cart.CouponCode = couponCode;
            cart.CouponRuleName = couponValidationResult.CouponRuleName;
            _cartItemRepository.SaveChanges();
        }

        return couponValidationResult;
    }
    public async Task MigrateCart(long fromUserId, long toUserId)
    {
        var cartFrom = await GetActiveCart(fromUserId);
        if (cartFrom != null && cartFrom.cartItems.Any())
        {
            var cartTo = await GetActiveCart(toUserId);
            if (cartTo == null)
            {
                cartTo = new Cart
                {
                    CustomerId = toUserId,
                    CreatedById = toUserId,
                    IsProductPriceIncludeTax = true
                };

                _cartRepository.Add(cartTo);
            }

            foreach (var fromItem in cartFrom.cartItems)
            {
                var toItem = cartTo.cartItems.FirstOrDefault(x => x.ProductId == fromItem.ProductId);
                if (toItem == null)
                {
                    toItem = new CartItem
                    {
                        Cart = cartTo,
                        ProductId = fromItem.ProductId,
                        Quantity = fromItem.Quantity,
                        CreatedOn = DateTimeOffset.Now
                    };
                    cartTo.cartItems.Add(toItem);
                }
                else
                {
                    toItem.Quantity = toItem.Quantity + fromItem.Quantity;
                }
            }

            await _cartRepository.SaveChangesAsync();
        }
    }
    public async Task UnlockCart(Cart cart)
    {
        if (cart == null)
        {
            throw new ArgumentNullException(nameof(cart));
        }

        if (cart.LockedOnCheckout)
        {
            cart.LockedOnCheckout = false;
            await _cartRepository.SaveChangesAsync();
        }
    }
}

