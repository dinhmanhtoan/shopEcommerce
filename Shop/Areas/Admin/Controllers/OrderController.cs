using Model.Helpers;
using System.Linq.Dynamic.Core;
namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class OrderController : Controller
{

    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<OrderHistory> _orderHistoryRepository;
    private readonly IWorkContext _workContext;
    private readonly IShipmentService _shipmentService;
    private readonly ICurrencyService _currencyService;
    public OrderController(IRepository<Order> orderRepository, IRepository<OrderHistory> orderHistoryRepository,
        IWorkContext workContext, IShipmentService shipmentService, ICurrencyService currencyService)
    {
        _orderRepository = orderRepository;
        _orderHistoryRepository = orderHistoryRepository;
        _workContext = workContext;
        _shipmentService = shipmentService;
        _currencyService = currencyService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Get()
    {
        var Order = _orderRepository.Query().Include(x => x.Customer).AsQueryable();
        var orders =  Order.OrderByDescending(x => x.CreatedOn).Take(5).Select(x => new OrderVm(_currencyService)
        {
            OrderId = x.Id,
            CustomerName = x.Customer.FullName,
            OrderTotal = x.OrderTotal,
            OrderStatus = x.OrderStatus.ToString(),
            CreatedOn = x.CreatedOn
        });

        return Json(orders);
    }

    [HttpPost]
    public async Task<IActionResult> List()
    {
        var currentUser = await _workContext.GetCurrentUser();
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;
        var Order = _orderRepository.Query().Include(x=> x.Customer).AsQueryable();

        var orders = Order.Select(x => new OrderVm(_currencyService)
        {
            OrderId = x.Id,
            CustomerName = x.Customer.FullName,
            OrderTotal = x.OrderTotal,
            OrderStatus = x.OrderStatus.ToString(),
            CreatedOn = x.CreatedOn
        });

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            orders = orders.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            orders = orders.Where(m => m.CustomerName.Contains(searchValue));
        }


        recordsTotal = orders.Count();
        var data = orders.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }
        
    [HttpGet("{OrderId}")]
    public async Task<IActionResult> Get(long OrderId){

        var order = _orderRepository.Query().Include(x => x.ShippingAddress).ThenInclude(x => x.District)
            .Include(x => x.ShippingAddress).ThenInclude(x => x.StateOrProvince)
            .Include(x => x.ShippingAddress).ThenInclude(x => x.Country)
            .Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.Thumbnail)
            .Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.OptionCombinations).ThenInclude(x => x.Option)
            .Include(x => x.Customer)

            .FirstOrDefault(x => x.Id == OrderId);
        if (order == null)
        {
            return NotFound();
        }
        var currentUser = await _workContext.GetCurrentUser();
        if (!User.IsInRole("admin") && order.VendorId != currentUser.VendorId)
        {
            return BadRequest(new { error = "You don't have permission to manage this order" });
        }
        var model = new OrderDetailVm(_currencyService)
        {
            Id = order.Id,
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
                DiscountAmount = x.DiscountAmount,
                TaxAmount = x.TaxAmount,
                TaxPercent = x.TaxPercent,
                VariationOptions = OrderItemVm.GetVariationOption(x.Product)
            }).ToList()
        };
        return View(model);
    }
    [HttpPost("change-order-status/{id}")]
    public async Task<IActionResult> ChangeStatus(long id, [FromBody] OrderStatusForm model)
    {
        var order = _orderRepository.Query().Include(x=> x.Customer).FirstOrDefault(x => x.Id == id);
        if (order == null)
        {
            return NotFound();
        }

        var currentUser = await _workContext.GetCurrentUser();
        if (!User.IsInRole("admin") && order.VendorId != currentUser.VendorId)
        {
            return BadRequest(new { error = "You don't have permission to manage this order" });
        }

        if (Enum.IsDefined(typeof(OrderStatus), model.StatusId))
        {
            var oldStatus = order.OrderStatus;
            order.OrderStatus = (OrderStatus)model.StatusId;
            await _orderRepository.SaveChangesAsync();

            var orderHistory = new OrderHistory
            {
                OrderId = order.Id,
                CreatedOn = DateTimeOffset.Now,
                CreatedById = currentUser.Id,
                OldStatus = oldStatus,
                NewStatus = order.OrderStatus,
                Note = model.Note,
            };
            _orderHistoryRepository.Add(orderHistory);
            await _orderRepository.SaveChangesAsync();
            return Ok(oldStatus);
        }

        return BadRequest(new { Error = "unsupported order status" });
    }

    [HttpGet("order-status")]
    public IActionResult GetOrderStatus()
    {
        var model = EnumHelper.ToDictionary(typeof(OrderStatus)).Select(x => new { Id = x.Key, Name = x.Value });
        return Json(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ShipmentForm model)
    {
        if (ModelState.IsValid)
        {
            var currentUser = await _workContext.GetCurrentUser();
            var shipment = new Shipment
            {
                OrderId = model.OrderId,
                WarehouseId = model.WarehouseId,
                CreatedById = currentUser.Id,
                TrackingNumber = model.TrackingNumber,
                CreatedOn = DateTimeOffset.Now,
                LatestUpdatedOn = DateTimeOffset.Now
            };

            //if (!User.IsInRole("admin"))
            //{
            //    shipment.VendorId = currentUser.VendorId;
            //}
            foreach (var item in model.Items)
            {
                if (item.QuantityToShip <= 0)
                {
                    continue;
                }

                var shipmentItem = new ShipmentItem
                {
                    Shipment = shipment,
                    Quantity = item.QuantityToShip,
                    ProductId = item.ProductId,
                    OrderItemId = item.OrderItemId
                };

                shipment.Items.Add(shipmentItem);
            }

            var result = await _shipmentService.CreateShipment(shipment);
            if (result.Success)
            {

                return Ok();
            }

            return BadRequest(result.Error);
        }

        return BadRequest(ModelState);
    }

}

