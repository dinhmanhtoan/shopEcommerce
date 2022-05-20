using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class ShipmentController : Controller
{
    private readonly IRepository<Warehouse> _warehouseRepository;
    private readonly IRepository<Shipment> _shipmentRepository;
    private readonly IWorkContext _workContext;
    private readonly IShipmentService _shipmentService;
    public ShipmentController(IRepository<Warehouse> warehouseRepository,
    IRepository<Shipment> shipmentRepository, IWorkContext workContext, IShipmentService shipmentService)
    {
        _warehouseRepository = warehouseRepository;
        _shipmentRepository = shipmentRepository;
        _workContext = workContext;
        _shipmentService = shipmentService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View();
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
        var Shipments = _shipmentRepository.Query().Include(x=> x.Warehouse).AsQueryable();

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            Shipments = Shipments.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            Shipments = Shipments.Where(m => m.TrackingNumber.Contains(searchValue));
        }
        recordsTotal = Shipments.Count();
        var data = Shipments.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get(long Id)
    {
        var query = await _shipmentRepository.Query().Include(x => x.Warehouse).Include(x => x.Items).ThenInclude(x => x.Product)
                            .Include(x => x.Order).ThenInclude(x => x.ShippingAddress).FirstOrDefaultAsync(x=> x.Id == Id);
            var shipmentvm = new ShipmentVm()
            {
                Id = query.Id,
                CreateOn = query.CreatedOn,
                WarehouseName = query.Warehouse.Name,
                TrakingNumber = query.TrackingNumber,
                OrderId = query.OrderId,
                ShippingAddress = query.Order.ShippingAddress,
                ProductVms = query.Items.Select(x => new ShipmentItemVm
                {
                        ProductId  = x.Product.Id,
                        ProductName = x.Product.Name,
                        ProductSku = x.Product.Sku,
                        QuantityToShip = x.Quantity
                    }).ToList(),
            };
   
        return View(shipmentvm);
    }
    [HttpGet("{orderId}")]
    public async Task<IActionResult> Create(long orderId)
    {
        var shipmentItem = await _shipmentService.GetShipmentItem(orderId);
        var warehouse = _warehouseRepository.Query().OrderBy(x => x.Name);
        var shipmentForm = new ShipmentForm();
        shipmentForm.OrderId = orderId;
        shipmentForm.Items = shipmentItem;
        shipmentForm.Warehouse = await warehouse.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToListAsync();
        return View(shipmentForm);
    }


}

