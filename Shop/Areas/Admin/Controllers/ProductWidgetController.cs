using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Helpers;

namespace Shop.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class ProductWidgetController : Controller
{
    private readonly IRepository<WidgetInstance> _widgetInstanceRepository;
    private readonly IRepository<WidgetZone> _widgetZoneRepository;
    private readonly ICategoryService _categoryService;

    public ProductWidgetController(IRepository<WidgetInstance> widgetInstanceRepository, 
        IRepository<WidgetZone> widgetZoneRepository, ICategoryService categoryService)
    {
        _widgetInstanceRepository = widgetInstanceRepository;
        _widgetZoneRepository = widgetZoneRepository;
        _categoryService = categoryService;
    }

    [HttpGet]
    public  async Task<IActionResult> Create()
    {
        var widgetZones = _widgetZoneRepository.Query().ToList();
        var category = await _categoryService.GetAll();
        var model = new ProductWidgetForm();
        model.PublishStart = DateTimeOffset.Now;
        model.WidgetZoneItems = widgetZones.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        model.Setting.CategoryListItems = category.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
       
        return View(model);
    }
    [HttpPost]
    public IActionResult Create(ProductWidgetForm model)
    {
        if (ModelState.IsValid)
        {
            var widgetInstance = new WidgetInstance
            {
                Name = model.Name,
                WidgetId = "ProductWidget",
                WidgetZoneId = model.WidgetZoneId,
                PublishStart = model.PublishStart,
                PublishEnd = model.PublishEnd,
                DisplayOrder = model.DisplayOrder,
                Data = JsonConvert.SerializeObject(model.Setting)
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
        var widgetZones = _widgetZoneRepository.Query().ToList();
        var category = await _categoryService.GetAll();
        var model = new ProductWidgetForm
        {
            Id = widgetInstance.Id,
            Name = widgetInstance.Name,
            WidgetZoneId = widgetInstance.WidgetZoneId,
            PublishStart = widgetInstance.PublishStart,
            PublishEnd = widgetInstance.PublishEnd,
            DisplayOrder = widgetInstance.DisplayOrder,
            Setting = JsonConvert.DeserializeObject<ProductWidgetSetting>(widgetInstance.Data)
        };
        model.WidgetZoneItems = widgetZones.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        model.Setting.CategoryListItems = category.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        var demo = EnumHelper.ToDictionary(typeof(ProductWidgetOrderBy));
        model.Setting.OrderByListItems = demo.Select(x => new SelectListItem()
        { 
            Value = x.Key.ToString(),
            Text = x.Key.ToString(),
            Selected = x.Key.ToString() == model.Setting.OrderBy.ToString() ? true : false
        }).ToList();
        return View(model);
    }
    [HttpPost("{id}")]
    public IActionResult Edit(long id,ProductWidgetForm model)
    {
        if (ModelState.IsValid)
        {
            var widgetInstance = _widgetInstanceRepository.Query().FirstOrDefault(x => x.Id == id);
            widgetInstance.Name = model.Name;
            widgetInstance.WidgetZoneId = model.WidgetZoneId;
            widgetInstance.PublishStart = model.PublishStart;
            widgetInstance.PublishEnd = model.PublishEnd;
            widgetInstance.DisplayOrder = model.DisplayOrder;
            widgetInstance.Data = JsonConvert.SerializeObject(model.Setting);

            _widgetInstanceRepository.SaveChanges();
            return Accepted();
        }

        return BadRequest(ModelState);
    }

}
