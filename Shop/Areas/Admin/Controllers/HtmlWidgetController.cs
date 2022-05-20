
namespace Shop.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class HtmlWidgetController : Controller
{
    private readonly IRepository<WidgetInstance> _widgetInstanceRepository;
    private readonly IRepository<WidgetZone> _widgetZoneRepository;

    public HtmlWidgetController(IRepository<WidgetInstance> widgetInstanceRepository, IRepository<WidgetZone> widgetZoneRepository)
    {
        _widgetInstanceRepository = widgetInstanceRepository;
        _widgetZoneRepository = widgetZoneRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new HtmlWidgetForm();
        var wigetZone = await _widgetZoneRepository.Query().ToListAsync();
        var total = await _widgetInstanceRepository.Query().CountAsync();
        model.WidgetZoneItems = wigetZone.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        model.PublishStart = DateTimeOffset.Now;
        model.numberOfWidgets = total;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(HtmlWidgetForm model)
    {
        if (ModelState.IsValid)
        {
            var widgetInstance = new WidgetInstance
            {
                Name = model.Name,
                WidgetId = "HtmlWidget",
                WidgetZoneId = model.WidgetZoneId,
                HtmlData = model.HtmlContent,
                PublishStart = model.PublishStart,
                PublishEnd = model.PublishEnd,
                DisplayOrder = model.DisplayOrder,
            };

            _widgetInstanceRepository.Add(widgetInstance);
            await _widgetInstanceRepository.SaveChangesAsync();
            return RedirectToAction(nameof(WidgetController.Index),"Widget");
        }
        // ifEroor
        var wigetZone = await _widgetZoneRepository.Query().ToListAsync();
        var total = await _widgetInstanceRepository.Query().CountAsync();
        model.WidgetZoneItems = wigetZone.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        model.numberOfWidgets = total;
        return View(model);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Edit(long id)
    {
        var widget = await _widgetInstanceRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
        var wigetZone = await _widgetZoneRepository.Query().ToListAsync();
        var total = await _widgetInstanceRepository.Query().CountAsync();
        var model = new HtmlWidgetForm
        {
            Id = widget.Id,
            Name = widget.Name,
            WidgetZoneId = widget.WidgetZoneId,
            HtmlContent = widget.HtmlData,
            PublishStart = widget.PublishStart,
            PublishEnd = widget.PublishEnd,
            DisplayOrder = widget.DisplayOrder,
        };
        model.WidgetZoneItems = wigetZone.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        model.numberOfWidgets = total;
        return View(model);
    }
    [HttpPost("{id}")]
    public async Task<IActionResult> Edit(long id,HtmlWidgetForm model)
    {
        if (ModelState.IsValid)
        {
            var widgetInstance = await _widgetInstanceRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
            widgetInstance.Name = model.Name;
            widgetInstance.WidgetZoneId = model.WidgetZoneId;
            widgetInstance.HtmlData = model.HtmlContent;
            widgetInstance.PublishStart = model.PublishStart;
            widgetInstance.PublishEnd = model.PublishEnd;
            widgetInstance.DisplayOrder = model.DisplayOrder;

            await _widgetInstanceRepository.SaveChangesAsync();
            return RedirectToAction(nameof(WidgetController.Index),"Widget");
        }

        // ifEroor
        var wigetZone = await _widgetZoneRepository.Query().ToListAsync();
        var total = await _widgetInstanceRepository.Query().CountAsync();
        model.WidgetZoneItems = wigetZone.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        model.numberOfWidgets = total;
        return View(model);
    }
}
