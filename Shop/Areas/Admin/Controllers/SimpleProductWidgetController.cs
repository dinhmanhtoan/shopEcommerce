namespace Shop.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class SimpleProductWidgetController : Controller
{
    private readonly IRepository<WidgetInstance> _widgetInstanceRepository;
    private readonly IRepository<WidgetZone> _widgetZoneRepository;
    private readonly IRepository<Product> _productRepository;
    public SimpleProductWidgetController(IRepository<WidgetInstance> widgetInstanceRepository, IRepository<WidgetZone> widgetZoneRepository, IRepository<Product> productRepository)
    {
        _widgetInstanceRepository = widgetInstanceRepository;
        _widgetZoneRepository = widgetZoneRepository;
        _productRepository = productRepository;
    }
    [HttpGet]
    public IActionResult Create()
    {
        var model = new SimpleProductWidgetForm();
        var wigetZone = _widgetZoneRepository.Query().ToList();
        model.WidgetZoneItems = wigetZone.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        model.PublishStart = DateTimeOffset.Now;
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Create(SimpleProductWidgetForm model)
    {
        if (ModelState.IsValid)
        {
            var productLinks = new List<ProductLinkVm>();
            foreach (var item in model.Setting.Products)
            {
                var product = _productRepository.Query().FirstOrDefault(x => x.Id == item.Id);
                if (product != null)
                {
                    var productLink = new ProductLinkVm()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        IsPublished = product.IsPublished
                    };
                    productLinks.Add(productLink);
                }
            }
            model.Setting.Products = productLinks;
            var widgetInstance = new WidgetInstance
            {
                Name = model.Name,
                WidgetId = "SimpleProductWidget",
                WidgetZoneId = model.WidgetZoneId,
                PublishStart = model.PublishStart,
                PublishEnd = model.PublishEnd,
                DisplayOrder = model.DisplayOrder,
                Data = JsonConvert.SerializeObject(model.Setting)
            };

            _widgetInstanceRepository.Add(widgetInstance);
            await _widgetInstanceRepository.SaveChangesAsync();
            return Accepted();
        }

        return BadRequest(ModelState);
    }

    [HttpGet("{id}")]
    public IActionResult Edit(long id)
    {
        var widgetInstance = _widgetInstanceRepository.Query().FirstOrDefault(x => x.Id == id);
        var wigetZone = _widgetZoneRepository.Query().ToList();
        var model = new SimpleProductWidgetForm()
        {
            Id = widgetInstance.Id,
            Name = widgetInstance.Name,
            WidgetZoneId = widgetInstance.WidgetZoneId,
            PublishStart = widgetInstance.PublishStart,
            PublishEnd = widgetInstance.PublishEnd,
            DisplayOrder = widgetInstance.DisplayOrder,
            Setting = JsonConvert.DeserializeObject<SimpleProductWidgetSetting>(widgetInstance.Data)
        };
        model.WidgetZoneItems = wigetZone.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        return View(model);
    }
    [HttpPost("{id}")]
    public async Task<IActionResult> Edit(long id,SimpleProductWidgetForm model)
    {
        if (ModelState.IsValid)
        {
            var productLinks = new List<ProductLinkVm>();
            foreach (var item in model.Setting.Products)
            {
                var product = _productRepository.Query().FirstOrDefault(x => x.Id == item.Id);
                if (product != null)
                {
                    var productLink = new ProductLinkVm()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        IsPublished = product.IsPublished
                    };
                    productLinks.Add(productLink);
                }
            }
            model.Setting.Products = productLinks;
            var widgetInstance = _widgetInstanceRepository.Query().FirstOrDefault(x => x.Id == id);
            widgetInstance.Name = model.Name;
            widgetInstance.WidgetZoneId = model.WidgetZoneId;
            widgetInstance.PublishStart = model.PublishStart;
            widgetInstance.PublishEnd = model.PublishEnd;
            widgetInstance.DisplayOrder = model.DisplayOrder;
            widgetInstance.Data = JsonConvert.SerializeObject(model.Setting);

            await _widgetInstanceRepository.SaveChangesAsync();
            return Accepted();
        }

        return BadRequest(ModelState);
    }
}

