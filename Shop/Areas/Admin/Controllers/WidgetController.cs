using System.Linq.Dynamic.Core;
namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class WidgetController : Controller
{
    private readonly IRepositoryWithTypedId<Widget, string> _WidgetRepository;
    private readonly IRepository<WidgetInstance> _widgetInstanceRepository;
    public WidgetController(
        IRepositoryWithTypedId<Widget, string> WidgetRepository,
        IRepository<WidgetInstance> widgetInstanceRepository)
    {
        _WidgetRepository = WidgetRepository;
        _widgetInstanceRepository = widgetInstanceRepository;
    }
    [HttpGet]
    public ActionResult Index()
    {
        var widget = _WidgetRepository.Query().ToList();
        return View(widget);
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
        var widgetInstance = _widgetInstanceRepository.Query().Include(x=> x.Widget).AsQueryable();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            widgetInstance = widgetInstance.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            widgetInstance = widgetInstance.Where(m => m.Name.Contains(searchValue));

        }
        recordsTotal = widgetInstance.Count();
        var data = widgetInstance.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);

    }

    [HttpPost]
    public ActionResult Delete(long Id)
    {
        var widgetInstance = _widgetInstanceRepository.Query().FirstOrDefault(x=> x.Id == Id);
        if (widgetInstance == null)
        {
            return NotFound();
        }
        _widgetInstanceRepository.Remove(widgetInstance);
        _widgetInstanceRepository.SaveChanges();
        return Accepted();
    }
}
