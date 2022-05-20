using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class CategoryWidgetController : Controller
{
    private readonly IRepository<WidgetInstance> _widgetInstanceRepository;
    private readonly IRepository<WidgetZone> _widgetZoneRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly ICategoryService _categoryService;
    public CategoryWidgetController(IRepository<WidgetInstance> widgetInstanceRepository, IRepository<WidgetZone> widgetZoneRepository, IRepository<Category> categoryRepository, ICategoryService categoryService)
    {
        _widgetInstanceRepository = widgetInstanceRepository;
        _widgetZoneRepository = widgetZoneRepository;
        _categoryRepository = categoryRepository;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new CategoryWidgetForm();
        var widgetZone = _widgetZoneRepository.Query().ToList();
        var categoryListList = await _categoryService.GetAll();
        model.WidgetZoneItems = widgetZone.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        model.PublishStart = DateTimeOffset.Now;
        var total = _widgetInstanceRepository.Query().Count();
        model.numberOfWidgets = total;
        model.Settings.CategoryListItems = categoryListList.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
        return View(model);
    }

    [HttpPost]
    public  IActionResult Create(CategoryWidgetForm model)
    {
        if (ModelState.IsValid)
        {
            var widgetInstance = new WidgetInstance
            {
                Name = model.Name,
                WidgetId = "CategoryWidget",
                WidgetZoneId = model.WidgetZoneId,
                PublishStart = model.PublishStart,
                PublishEnd = model.PublishEnd,
                DisplayOrder = model.DisplayOrder,
                Data = JsonConvert.SerializeObject(model.Settings)
            };

            _widgetInstanceRepository.Add(widgetInstance);
            _widgetInstanceRepository.SaveChanges();
            return Accepted();
        }
        return BadRequest(ModelState);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Edit(long id)
    {
        var widgetInstance = _widgetInstanceRepository.Query().FirstOrDefault(x => x.Id == id);
        var categoryListList = await _categoryService.GetAll();
        var total = _widgetInstanceRepository.Query().Count();
        var widgetZone = _widgetZoneRepository.Query().ToList();
        var model = new CategoryWidgetForm
        {
            Id = widgetInstance.Id,
            Name = widgetInstance.Name,
            WidgetZoneId = widgetInstance.WidgetZoneId,
            PublishStart = widgetInstance.PublishStart,
            PublishEnd = widgetInstance.PublishEnd,
            DisplayOrder = widgetInstance.DisplayOrder,
            Settings = JsonConvert.DeserializeObject<CategoryWidgetSettings>(widgetInstance.Data)
        };
        model.WidgetZoneItems = widgetZone.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        model.numberOfWidgets = total;
        model.Settings.CategoryListItems = categoryListList.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        return View(model);
    }
    [HttpPost("{id}")]
    public async Task<IActionResult> Edit(long id,CategoryWidgetForm model)
    {
        if (ModelState.IsValid)
        {
            var widgetInstance = await _widgetInstanceRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
            widgetInstance.Name = model.Name;
            widgetInstance.WidgetZoneId = model.WidgetZoneId;
            widgetInstance.PublishStart = model.PublishStart;
            widgetInstance.PublishEnd = model.PublishEnd;
            widgetInstance.DisplayOrder = model.DisplayOrder;
            widgetInstance.Data = JsonConvert.SerializeObject(model.Settings);
            _widgetInstanceRepository.SaveChanges();
            return Accepted();
        }
        return BadRequest(ModelState);
    }
}
