namespace Shop.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class WidgetZoneController : Controller
{
    private readonly IRepository<WidgetZone> _widgetZoneRespository;

    public WidgetZoneController(IRepository<WidgetZone> widgetZoneRespository)
    {
        _widgetZoneRespository = widgetZoneRespository;
    }
    public IActionResult Index()
    {
        var widgetZones = _widgetZoneRespository.Query().Select(x => new
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
        }).ToListAsync();
        return View(widgetZones);
    }
}

