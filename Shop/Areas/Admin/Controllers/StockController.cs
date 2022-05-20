using System.Linq.Dynamic.Core;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class StockController : Controller
{
    private readonly IRepository<Warehouse> _warehouseRepository;
    private readonly IRepository<Stock> _stockRepository;
    private readonly IRepository<StockHistory> _stockHistoryRepository;
    private readonly IStockService _stockService;
    private readonly IWorkContext _workContext;
    public StockController(IRepository<Warehouse> warehouseRepository,
    IRepository<Stock> stockRepository,
    IRepository<StockHistory> stockHistoryRepository,
    IStockService stockService, IWorkContext workContext)
    {
        _warehouseRepository = warehouseRepository;
        _stockRepository = stockRepository;
        _stockHistoryRepository = stockHistoryRepository;
        _stockService = stockService;
        _workContext = workContext;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var warehouse = _warehouseRepository.Query().ToList();
        var warehouseProductForms = new WarehouseProductForm();
        warehouseProductForms.Warehouse = _warehouseRepository.Query().Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name,
        }).ToList();
        return View(warehouseProductForms);
    }
    [HttpPost("{warehouseId}")]
    public async Task<IActionResult> List(long warehouseId)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var warehouse = _warehouseRepository.Query().FirstOrDefault(x => x.Id == warehouseId);
        if (warehouse == null)
        {
            return NotFound();
        }
        if (!User.IsInRole("admin") && warehouse.VendorId != currentUser.VendorId)
        {
            return BadRequest(new { error = "You don't have permission to manage this warehouse" });
        }

        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;
        var query = _stockRepository.Query().Include(x => x.Product).Where(x => x.WarehouseId == warehouseId && !x.Product.HasOptions && !x.Product.IsDeleted);

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            query = query.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            query = query.Where(m => m.Product.Name.Contains(searchValue));
        }
        recordsTotal = query.Count();
        var data = query.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }
    [HttpPost("{warehouseId}")]
    public async Task<IActionResult> Put(long warehouseId, IList<StockVm> stockVms)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var warehouse = _warehouseRepository.Query().FirstOrDefault(x => x.Id == warehouseId);
        if (warehouse == null)
        {
            return NotFound();
        }

        if (!User.IsInRole("admin") && warehouse.VendorId != currentUser.VendorId)
        {
            return BadRequest(new { error = "You don't have permission to manage this warehouse" });
        }

        foreach (var item in stockVms)
        {
            if (item.AdjustedQuantity == 0)
            {
                continue;
            }

            var stockUpdateRequest = new StockUpdateRequest
            {
                WarehouseId = warehouseId,
                ProductId = item.ProductId,
                AdjustedQuantity = item.AdjustedQuantity,
                Note = item.Note,
                UserId = currentUser.Id
            };

            await _stockService.UpdateStock(stockUpdateRequest);
        }

        return Accepted();
    }
    [HttpGet("{warehouseId}/{productId}")]
    public async Task<IActionResult> GetStockHistory(int warehouseId, int productId)
    {
        var query = _stockHistoryRepository.Query().Where(x => x.WarehouseId == warehouseId && x.ProductId == productId);
        var stockHistory = await query.Select(x => new StockHistoryVm
        {
            Id = x.Id,
            ProductName =  x.Product.Name,
            CreatedOn = x.CreatedOn,
            UserFullName =  x.CreatedBy.FullName,
            AdjustedQuantity = x.AdjustedQuantity,
            Note =  x.Note
        }).ToListAsync();
        return View(stockHistory);
    }
}

