using System.Linq.Dynamic.Core;
namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class CartRuleUsageController : Controller
{
    public readonly IRepository<CartRuleUsage> _cartRuleUsageRepository;
    public CartRuleUsageController(IRepository<CartRuleUsage> cartRuleUsageRepository)
    {
        _cartRuleUsageRepository = cartRuleUsageRepository;
    }
    [HttpGet]
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
        var cartRuleUsage = _cartRuleUsageRepository.Query().Include(x=> x.CartRule).Include(x => x.User).Include(x => x.Coupon).AsQueryable();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            cartRuleUsage = cartRuleUsage.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            cartRuleUsage = cartRuleUsage.Where(m => m.User.UserName.Contains(searchValue));

        }
        recordsTotal = cartRuleUsage.Count();
        var data = cartRuleUsage.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }
}

