namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class RecentlyViewedWidgetController : Controller
{
    private readonly IRepository<WidgetInstance> _widgetInstanceRepository;
    private readonly IRepository<WidgetZone> _widgetZoneRepository;
    private readonly IMediaService _mediaService;

    public RecentlyViewedWidgetController(IRepository<WidgetInstance> widgetInstanceRepository, IRepository<WidgetZone> widgetZoneRepository, IMediaService mediaService)
    {
        _widgetInstanceRepository = widgetInstanceRepository;
        _widgetZoneRepository = widgetZoneRepository;
        _mediaService = mediaService;
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new RecentlyViewedWidgetForm();
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
    [AllowAnonymous]
    public async Task<IActionResult> Create(RecentlyViewedWidgetForm model)
    {
        if (ModelState.IsValid)
        {
            var widgetInstance = new WidgetInstance
            {
                Name = model.Name,
                WidgetId = "RecentlyViewedWidget",
                WidgetZoneId = model.WidgetZoneId,
                Data = model.ItemCount.ToString(),
                PublishStart = model.PublishStart,
                PublishEnd = model.PublishEnd,
                DisplayOrder = model.DisplayOrder,
            };

            _widgetInstanceRepository.Add(widgetInstance);
            await _widgetInstanceRepository.SaveChangesAsync();
            return Accepted();
        }
        return BadRequest(ModelState);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Edit(long id)
    {
        var widgetInstance = await _widgetInstanceRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
        var wigetZone = _widgetZoneRepository.Query().ToList();
        var total = _widgetInstanceRepository.Query().Count();

        var model = new RecentlyViewedWidgetForm
        {
            Id = widgetInstance.Id,
            Name = widgetInstance.Name,
            WidgetZoneId = widgetInstance.WidgetZoneId,
            PublishStart = widgetInstance.PublishStart,
            PublishEnd = widgetInstance.PublishEnd,
            DisplayOrder = widgetInstance.DisplayOrder,
            ItemCount = JsonConvert.DeserializeObject<int>(widgetInstance.Data)
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
    public async Task<IActionResult> Edit(long id, RecentlyViewedWidgetForm model)
    {
        if (ModelState.IsValid)
        { 
            var widgetInstance = await _widgetInstanceRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
            if (widgetInstance == null)
            {
                return NotFound();
            }

            widgetInstance.Name = model.Name;
            widgetInstance.PublishStart = model.PublishStart;
            widgetInstance.PublishEnd = model.PublishEnd;
            widgetInstance.WidgetZoneId = model.WidgetZoneId;
            widgetInstance.DisplayOrder = model.DisplayOrder;
            widgetInstance.Data = model.ItemCount.ToString();

            await _widgetInstanceRepository.SaveChangesAsync();
            return Accepted();
        }
        return BadRequest(ModelState);
    }
}
