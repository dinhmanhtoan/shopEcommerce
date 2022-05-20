namespace Shop.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IWorkContext _workContext;
    private readonly IRepository<Order> _orderRepository;
    private readonly IMediaService _mediaService;
    private readonly ICurrencyService _currencyService;

    public OrderController(IWorkContext workContext, IRepository<Order> orderRepository,
        IMediaService mediaService, ICurrencyService currencyService)
    {
        _workContext = workContext;
        _orderRepository = orderRepository;
        _mediaService = mediaService;
        _currencyService = currencyService;
    }
    [HttpGet("user/orders")]
    public async Task<IActionResult> OrderHistoryList()
    {
        var user = await _workContext.GetCurrentUser();
        var model = _orderRepository.Query().Where(x => x.CustomerId == user.Id && x.ParentId == null)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.Thumbnail)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.OptionCombinations).ThenInclude(x => x.Option)
                    .OrderByDescending(x => x.CreatedOn).AsQueryable();
        var model2 = model.Select(x => new OrderHistoryListItem()
        {
            Id = x.Id,
            CreatedOn = x.CreatedOn,
            SubTotal = x.SubTotal,
            OrderStatus = x.OrderStatus,
            OrderItems = x.OrderItems.Select(i => new OrderHistoryProductVm()
            {
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                ThumbnailImage = i.Product.Thumbnail.FileName,
                ProductOptions = i.Product.OptionCombinations.Select(x => x.Value)
            }).ToList()
        }).ToList();
        foreach (var item in model2)
        {
            foreach (var product in item.OrderItems)
            {
                product.ThumbnailImage = _mediaService.GetMediaUrl(product.ThumbnailImage);
            }
        }
        return View(model2);
    }
    [HttpGet("user/orders/{orderId}")]
    public async Task<IActionResult> OrderDetails(long OrderId)
    {
        var user = await _workContext.GetCurrentUser();
        var order = await _orderRepository.Query()
                    .Include(x => x.ShippingAddress).ThenInclude(x => x.District)
                    .Include(x => x.ShippingAddress).ThenInclude(x => x.StateOrProvince)
                    .Include(x => x.ShippingAddress).ThenInclude(x => x.Country)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.Thumbnail)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.OptionCombinations).ThenInclude(x => x.Option)
                    .Include(x=> x.Customer)
                    .FirstOrDefaultAsync(x => x.Id == OrderId);

        if (order == null)
        {
            return NotFound();
        }
        if (order.CustomerId != user.Id)
        {
            return BadRequest(new { error = "You don't have permission to view this order" });
        }
        var model = new OrderDetailVm(_currencyService)
        {
            Id = order.Id,
            IsMasterOrder = order.IsMasterOrder,
            CreatedOn = order.CreatedOn,
            OrderStatus = (int)order.OrderStatus,
            OrderStatusString = order.OrderStatus.ToString(),
            CustomerId = order.CustomerId,
            CustomerName = order.Customer.FullName,
            CustomerEmail = order.Customer.Email,
            ShippingMethod = order.ShippingMethod,
            PaymentMethod = order.PaymentMethod,
            PaymentFeeAmount = order.PaymentFeeAmount,
            Subtotal = order.SubTotal,
            DiscountAmount = order.DiscountAmount,
            SubTotalWithDiscount = order.SubTotalWithDiscount,
            TaxAmount = order.TaxAmount,
            ShippingAmount = order.ShippingFeeAmount,
            OrderTotal = order.OrderTotal,
            OrderNote = order.OrderNote,
            ShippingAddress = new ShippingAddressVm
            {
                AddressLine1 = order.ShippingAddress.AddressLine1,
                CityName = order.ShippingAddress.City,
                ZipCode = order.ShippingAddress.ZipCode,
                ContactName = order.ShippingAddress.ContactName,
                DistrictName = order.ShippingAddress.District?.Name,
                StateOrProvinceName = order.ShippingAddress.StateOrProvince.Name,
                Phone = order.ShippingAddress.Phone
            },
            OrderItems = order.OrderItems.Select(x => new OrderItemVm(_currencyService)
            {
                Id = x.Id,
                ProductId = x.Product.Id,
                ProductName = x.Product.Name,
                ProductPrice = x.ProductPrice,
                Quantity = x.Quantity,
                DiscountAmount = x.Order.DiscountAmount,
                ProductImage = x.Product.Thumbnail.FileName,
                TaxAmount = x.Order.TaxAmount,
                //TaxPercent = x.Order.TaxPercent,
                VariationOptions = OrderItemVm.GetVariationOption(x.Product)
            }).ToList()
        };
        foreach (var item in model.OrderItems)
        {
            item.ProductImage = _mediaService.GetMediaUrl(item.ProductImage);

        }
        return View(model);
    }
}

