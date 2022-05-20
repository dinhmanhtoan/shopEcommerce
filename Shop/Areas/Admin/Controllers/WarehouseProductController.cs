using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Dynamic.Core;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class WarehouseProductController : Controller
{
    private readonly IRepository<Warehouse> _warehouseRepository;
    private readonly IRepository<Stock> _stockRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IWorkContext _workContext;
    private readonly IStockService _stockService;
    public WarehouseProductController(IRepository<Warehouse> warehouseRepository,
    IRepository<Stock> stockRepository, IRepository<Product> productRepository , IWorkContext workContext, IStockService stockService)
    {
        _warehouseRepository = warehouseRepository;
        _stockRepository = stockRepository;
        _productRepository  = productRepository;
        _workContext = workContext;
        _stockService = stockService;
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
    [HttpPost("{warehouseId}/products")]
    public IActionResult post(long warehouseId)
    {
        var warehouse = _warehouseRepository.Query().ToList();
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;
        var Product = _productRepository.Query().Where(x => !x.HasOptions && !x.IsDeleted);
        var joinedQuery = Product.GroupJoin
            (
                _stockRepository.Query().Where(x => x.WarehouseId == warehouseId),
                product => product.Id, stock => stock.ProductId,
                (product, stock) => new { product, stock }
            )
            .SelectMany(x => x.stock.DefaultIfEmpty(), (x, stock) => new MangeWarehouseProductItemVm
            {
                Id = x.product.Id,
                Name = x.product.Name,
                Sku = x.product.Sku,
                Quantity = stock.Quantity
            });
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            joinedQuery = joinedQuery.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            joinedQuery = joinedQuery.Where(m => m.Name.Contains(searchValue));

        }
        recordsTotal = joinedQuery.Count();
        var data = joinedQuery.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }
    [HttpPost("{warehouseId}/add-products")]
    public async Task<IActionResult> AddProducts(long warehouseId, IList<long> productIds)
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
        var productIdsArray = productIds.ToArray();
        var existedProductIds = await _stockRepository.Query().Where(x => x.WarehouseId == warehouseId && productIdsArray.Contains(x.ProductId))
                                        .Select(x => x.ProductId).ToListAsync();
        foreach (var id in existedProductIds)
        {
            productIds.Remove(id);
        }
        var stocks = productIds.Select(x => new Stock
        {
            ProductId = x,
            WarehouseId = warehouseId,
            Quantity = 0
        });
        _stockRepository.AddRange(stocks);
        _stockRepository.SaveChanges();
        return Accepted();
    }
    [HttpPost("{warehouseId}/add-all-products")]
    public async Task<IActionResult> AddAllProducts(long warehouseId)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var warehouse = _warehouseRepository.Query().FirstOrDefault(x => x.Id == warehouseId);
        if (warehouse == null)
        {
            return BadRequest();
        }
        if (!User.IsInRole("admin") && warehouse.VendorId != currentUser.VendorId)
        {
            return BadRequest(new { error = "You don't have permission to manage this warehouse" });
        }
        await _stockService.AddAllProduct(warehouse);
        return Accepted();
    }

}

