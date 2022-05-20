namespace Shop.Areas.Admin.Controllers;

[Area("Shipments")]
[Authorize(Roles = "admin")]
[Route("api/shipments")]
public class ShipmentApiController : Controller
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Shipment> _shipmentRepository;
    private readonly IWorkContext _workContext;
    private readonly IShipmentService _shipmentService;

    public ShipmentApiController(IRepository<Order> orderRepository,
    IRepository<Shipment> shipmentRepository, IWorkContext workContext, IShipmentService shipmentService)
    {
        _orderRepository = orderRepository;
        _shipmentRepository = shipmentRepository;
        _workContext = workContext;
        _shipmentService = shipmentService;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var shipment = await _shipmentRepository.Query()
            .Select(x => new {
                x.Id,
                x.OrderId,
                WarehouseName = x.Warehouse.Name,
                x.TrackingNumber,
                x.CreatedOn,
                ShippingAddress = new
                {
                    x.Order.ShippingAddress.AddressLine1,
                    x.Order.ShippingAddress.AddressLine2,
                    x.Order.ShippingAddress.ContactName,
                    DistrictName = x.Order.ShippingAddress.District.Name,
                    StateOrProvinceName = x.Order.ShippingAddress.StateOrProvince.Name,
                    x.Order.ShippingAddress.Phone
                },
                Items = x.Items.Select(i => new
                {
                    i.Id,
                    ProductId = i.Product.Id,
                    ProductName = i.Product.Name,
                    ProductSku = i.Product.Sku,
                    i.Quantity
                })
            })
            .FirstOrDefaultAsync(x => x.Id == id);

        if (shipment == null)
        {
            return NotFound();
        }

        var currentUser = await _workContext.GetCurrentUser();
        if (!User.IsInRole("admin") /*&& shipment.VendorId != currentUser.VendorId*/)
        {
            return BadRequest(new { error = "You don't have permission to manage this order" });
        }

        return Ok(shipment);
    }
    [HttpGet("{orderId}/shipments")]
    public async Task<IActionResult> GetByOrder(long orderId)
    {
        var order = _orderRepository.Query().FirstOrDefault(x => x.Id == orderId);
        if(order == null)
        {
            return NotFound();
        }
        var shipment = await _shipmentRepository.Query().Where(x=> x.OrderId == orderId)
            .Select(x => new
            {
                x.Id,
                x.OrderId,
                WarehouseName = x.Warehouse.Name,
                x.TrackingNumber,
                x.CreatedOn,
                ShippingAddress = new
                {
                    x.Order.ShippingAddress.AddressLine1,
                    x.Order.ShippingAddress.AddressLine2,
                    x.Order.ShippingAddress.ContactName,
                    DistrictName = x.Order.ShippingAddress.District.Name,
                    StateOrProvinceName = x.Order.ShippingAddress.StateOrProvince.Name,
                    x.Order.ShippingAddress.Phone
                },
                Items = x.Items.Select(i => new
                {
                    i.Id,
                    ProductId = i.Product.Id,
                    ProductName = i.Product.Name,
                    ProductSku = i.Product.Sku,
                    i.Quantity
                })
            }).ToListAsync();
             

        if (shipment == null)
        {
            return NotFound();
        }

        var currentUser = await _workContext.GetCurrentUser();
        if (!User.IsInRole("admin") /*&& shipment.VendorId != currentUser.VendorId*/)
        {
            return BadRequest(new { error = "You don't have permission to manage this order" });
        }

        return Ok(shipment);
    }
    [HttpGet("/api/orders/{orderId}/items-to-ship")]
    public async Task<IActionResult> GetItemsToShip(long orderId, long warehouseId)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var order = _orderRepository.Query().FirstOrDefault(x => x.Id == orderId);
        if (order == null)
        {
            return NotFound();
        }

        if (!User.IsInRole("admin") /* && order.VendorId != currentUser.VendorId*/)
        {
            return BadRequest(new { error = "You don't have permission to manage this order" });
        }

        var model = new ShipmentForm
        {
            OrderId = orderId,
            WarehouseId = warehouseId
        };

        var itemsToShip = await _shipmentService.GetItemToShip(orderId, warehouseId);
        foreach (var item in itemsToShip)
        {
            item.QuantityToShip = item.OrderedQuantity - item.ShippedQuantity;
        }

        model.Items = itemsToShip;
        return Ok(model);
    }
}
