namespace Shop.Controllers;

public class CartController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICartService _cartService;
    private readonly IWorkContext _workContext;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<CartItem> _cartItemRepository;
    private readonly IRazorViewRenderer _html;
    public const string CARTKEY = "cart";
    public const string WISHKEY = "wishlist";
    public const string RECENTLYVIEWED = "recentlyviewed";
    public CartController(
        ILogger<HomeController> logger, IWorkContext workContext,
        IRepository<Product> productRepository, IRepository<CartItem> cartItemRepository,
        ICartService cartService,
        IRazorViewRenderer html
        )
    {
        _logger = logger;
        _workContext = workContext;
        _productRepository = productRepository;
        _cartItemRepository = cartItemRepository;
        _cartService = cartService;
        _html = html;

    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddToCart(long productid, int quantity)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var result = await _cartService.AddToCart(currentUser.Id, productid, quantity);
        if (result.Success)
        {
            return RedirectToAction("AddToCartResult", new { productId = productid });
        }
        else
        {
            return Ok(new { Error = true, Message = result.Error });
        }
    }
    [HttpGet]
    public async Task<IActionResult> AddToCartResult(long productId)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCartDetails(currentUser.Id);
        var model = new AddToCartResult()
        {
            CartItemCount = cart.Items.Count,
            CartAmount = cart.SubTotal
        };
        var addedProduct = cart.Items.First(x => x.ProductId == productId);
        model.ProductName = addedProduct.ProductName;
        model.ProductImage = addedProduct.ProductImage;
        model.ProductPrice = addedProduct.ProductPrice;
        model.Quantity = addedProduct.Quantity;
        return PartialView(model);
    }
    [Route("cart")]
    public async Task<IActionResult> Cart()
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cartVm = await _cartService.GetActiveCartDetails(currentUser.Id);
        return View(cartVm);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(CartQuantityUpdate model)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCart(currentUser.Id);
        if (cart == null)
        {
            return NotFound();
        }
        if (cart.LockedOnCheckout)
        {
            return CreateCartLockedResult();
        }
        var cartItem = _cartItemRepository.Query().Include(x => x.Product).FirstOrDefault(x => x.Id == model.CartItemId && x.Cart.CreatedById == currentUser.Id);
        if (cartItem == null)
        {
            return NotFound();
        }

        if (model.Quantity > cartItem.Quantity) // always allow user to descrease the quality
        {
            if (cartItem.Product.StockTrackingIsEnabled && cartItem.Product.StockQuantity < model.Quantity)
            {
                return Ok(new { Error = true, Message = $"There are only {cartItem.Product.StockQuantity} items available for {cartItem.Product.Name}" });
            }
        }
        cartItem.Quantity = model.Quantity;
        _cartItemRepository.SaveChanges();

        var cartVm = await _cartService.GetActiveCartDetails(currentUser.Id);
        return Json(cartVm);
        //return Json(await _html.RenderViewToStringAsync("/Views/Cart/_Cart.cshtml", cartVm));
    }
    [HttpPost("cart/apply-coupon")]
    public async Task<IActionResult> ApplyCoupon(ApplyCouponForm model)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCart(currentUser.Id);
        if (cart == null)
        {
            return NotFound();
        }
        if (cart.LockedOnCheckout)
        {
            return CreateCartLockedResult();
        }
        var validationResult = await _cartService.ApplyCoupon(cart.Id, model.CouponCode);
        if (validationResult.Succeeded)
        {
            var cartVm = await _cartService.GetActiveCartDetails(currentUser.Id);
            return Json(cartVm);
            //return Json(await _html.RenderViewToStringAsync("/Views/Cart/_Cart.cshtml", cartVm));
        }
        return Json(validationResult);
    }

    [HttpPost("cart/save-ordernote")]
    public async Task<IActionResult> SaveOrderNote(string OrderNote)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCart(currentUser.Id);
        if (cart == null)
        {
            return NotFound();
        }
        cart.OrderNote = OrderNote;
        await _cartItemRepository.SaveChangesAsync();
        return Accepted();
    }
    [HttpPost]
    public async Task<IActionResult> Remove(long CartId)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCart(currentUser.Id);
        if(cart == null)
        {
            return NotFound();
        }
        if (cart.LockedOnCheckout)
        {
            return CreateCartLockedResult();
        }
        var cartItem = _cartItemRepository.Query().FirstOrDefault(x => x.Id == CartId && x.Cart.CreatedById == currentUser.Id);
        if (cartItem == null)
        {
            return NotFound();
        }
        _cartItemRepository.Remove(cartItem);
        _cartItemRepository.SaveChanges();

        var cartVm = await _cartService.GetActiveCartDetails(currentUser.Id);
        return Json(cartVm);
        //return Json(await _html.RenderViewToStringAsync("/Views/Cart/_Cart.cshtml", cartVm));
    }
    [HttpPost("cart/unlock")]
    public async Task<IActionResult> Unlock()
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCart(currentUser.Id);
        if (cart == null)
        {
            return NotFound();
        }

        await _cartService.UnlockCart(cart);
        return Accepted();
    }

    private IActionResult CreateCartLockedResult()
    {
        return Ok(new { Error = true, Message = "Cart is locked for checkout. Please complete or cancel the checkout first." });
    }
    [HttpGet]
    public List<Product> GetViewed()
    {
        var cookieValueFromReq = Get(RECENTLYVIEWED);
        if (cookieValueFromReq != null)
        {
            var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);
            var ListWishList = new List<Product>();
            foreach (var item in jsonConvert)
            {
                var product = _productRepository.Query().Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
                .Where(p => p.Id == item).FirstOrDefault();
                ListWishList.Add(product);
            };
            return ListWishList;
        }

        return new List<Product>();
    }
    public string Get(string key)
    {
        return Request.Cookies[key];
    }

    List<long> GetViewedItems()
    {
        var cookieValueFromReq = Get(RECENTLYVIEWED);
        if (cookieValueFromReq != null)
        {
            var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);

            return jsonConvert;
        }
        return new List<long>();
    }

    [HttpPost]
    public IActionResult RecentlyViewed(long productid)
    {
        var product = GetViewed();
        if (product == null)
        {
            return NotFound("Không có sản phẩm yêu thích");
        }
        var count = 0;
        var Viewed = GetViewedItems();
        foreach (var item in Viewed)
        {
            if (item == productid)
            {
                count++;
            }

        }
        if (count > 0)
        {
            return Ok();
        }

        else
        {
            Viewed.Add(productid);
        }
        // convert sang json
        var jsonCart = JsonConvert.SerializeObject(Viewed, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
    // luu vao cookies
    SaveCookies(RECENTLYVIEWED, jsonCart, 30);

        return Ok();
    }
    public void ClearCookies(string Key)
    {
        Response.Cookies.Delete(Key);
    }
    public void SaveCookies(string key, string value, int? expireTime)
    {
        CookieOptions option = new CookieOptions();
        if (expireTime.HasValue)
            option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
        else
            option.Expires = DateTime.Now.AddMinutes(10);
        Response.Cookies.Append(key, value, option);
    }

    [HttpGet]
    [Route("/wishlist")]
    public IActionResult WishList()
    {
        return View(GetWishList());
    }
    [HttpGet]
    public List<Product> GetWishList()
        {
            var cookieValueFromReq = Get(WISHKEY);
            if (cookieValueFromReq != null)
            {

                var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);
                var ListWishList = new List<Product>();
                foreach (var item in jsonConvert)
                {
                    var product = _productRepository.Query().Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
                    .Where(p => p.Id == item).FirstOrDefault();
                    ListWishList.Add(product);
                };
                return ListWishList;
            }

            return new List<Product>();
        }
    List<long> GetWishListItems()
        {
            var cookieValueFromReq = Get(WISHKEY);
            if (cookieValueFromReq != null)
            {
                var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);

                return jsonConvert;
            }
            return new List<long>();
        }
    [HttpPost]
    public IActionResult AddtoWishList(long productid)
    {
        var count = 0;
        var wishList = GetWishListItems();
        foreach (var item in wishList)
        {
            if (item == productid)
            {
                count++;
            }

        }
        if (count > 0)
        {
            return Ok();
        }

        else
        {
            wishList.Add(productid);
        }

        // convert sang json
        var jsonCart =   JsonConvert.SerializeObject(wishList, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
    // luu vao cookies
    SaveCookies(WISHKEY, jsonCart, 30);

        return Ok();
    }
    [HttpPost]
    public IActionResult RemoveWishList(long productid)
        {
            var count = 0;
            var wishList = GetWishListItems();
            foreach (var item in wishList)
            {
                if (item == productid)
                {
                    count++;
                }

            }
            if (count > 0)
            {
                // Đã tồn tại, tăng thêm 1
                wishList.Remove(productid);
            }
            var jsonCart = JsonConvert.SerializeObject(wishList);
        // luu vao cookies
        SaveCookies(WISHKEY, jsonCart, 30);
            return RedirectToAction("Cart", "Cart");
        }
}


