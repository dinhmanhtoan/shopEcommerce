using System.Linq.Dynamic.Core;
namespace Shop.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class ProductPriceController : Controller
{
    private readonly IRepository<Product> _productRepository;
    private readonly IWorkContext _workContext;
    public ProductPriceController(IRepository<Product> productRepository, IWorkContext workContext)
    {
        _productRepository = productRepository;
        _workContext = workContext;
    }
    // GET: ProductPriceController
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult query()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;
        var Product = _productRepository.Query().Where(x => !x.IsDeleted).AsQueryable();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            Product = Product.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            Product = Product.Where(m => m.Name.Contains(searchValue));

        }
        recordsTotal = Product.Count();
        var data = Product.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }
    [HttpPost]
    public async Task<ActionResult> changePrice(IList<ProductPriceItemForm> model)
    {
        var currentUser = await _workContext.GetCurrentUser();
        foreach (var item in model)
        {
            if (!item.NewOldPrice.HasValue && !item.NewPrice.HasValue)
            {
                continue;
            }

            var product = _productRepository.Query().FirstOrDefault(x => x.Id == item.Id);
            if (product != null && (User.IsInRole("admin") || product.VendorId == currentUser.VendorId))
            {
                var productPriceHistory = new ProductPriceHistory
                {
                    Product = product,
                    CreatedBy = currentUser,
                    //  OldPrice = product.OldPrice,
                    Price = product.Price,
                    // SpecialPrice = product.SpecialPrice,
                    //   SpecialPriceStart = product.SpecialPriceStart,
                    //  SpecialPriceEnd = product.SpecialPriceEnd
                };

                if (item.NewOldPrice.HasValue)
                {
                    //      product.OldPrice = productPriceHistory.OldPrice = item.NewOldPrice.Value;
                }

                if (item.NewPrice.HasValue)
                {
                    product.Price = item.NewPrice.Value;
                    productPriceHistory.Price = item.NewPrice.Value;
                }

                product.PriceHistories.Add(productPriceHistory);
            }
        }
        await _productRepository.SaveChangesAsync();
        return Accepted();
    }

}

