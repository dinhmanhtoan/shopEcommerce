using System.Net.Http.Headers;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class SpaceBarWidgetController : Controller
{
    private readonly IRepository<WidgetInstance> _widgetInstanceRepository;
    private readonly IRepository<WidgetZone> _widgetZoneRepository;
    private readonly IMediaService _mediaService;

    public SpaceBarWidgetController(IRepository<WidgetInstance> widgetInstanceRepository, IRepository<WidgetZone> widgetZoneRepository, IMediaService mediaService)
    {
        _widgetInstanceRepository = widgetInstanceRepository;
        _widgetZoneRepository = widgetZoneRepository;
        _mediaService = mediaService;
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new SpaceBarWidgetForm();
        var wigetZone = _widgetZoneRepository.Query().ToList();
        model.WidgetZoneItems = wigetZone.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        model.PublishStart = DateTimeOffset.Now;
        var total = _widgetInstanceRepository.Query().Count();
        model.numberOfWidgets = total;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(IFormCollection formCollection)
    {
        var model = ToSpaceBarWidgetFormModel(formCollection);
        if (ModelState.IsValid)
        {
            foreach (var item in model.Items)
            {
                if (item.UploadImage != null)
                {
                    item.Image = await SaveFile(item.UploadImage);
                }
            }

            var widgetInstance = new WidgetInstance
            {
                Name = model.Name,
                WidgetId = "SpaceBarWidget",
                WidgetZoneId = model.WidgetZoneId,
                PublishStart = model.PublishStart,
                PublishEnd = model.PublishEnd,
                DisplayOrder = model.DisplayOrder,
                Data = JsonConvert.SerializeObject(model.Items)
            };

            _widgetInstanceRepository.Add(widgetInstance);
            _widgetInstanceRepository.SaveChanges();
            return Accepted();
        }

        return BadRequest(ModelState);
    }
    [HttpGet("{id}")]
    public IActionResult Edit(long id)
    {
        var widgetInstance = _widgetInstanceRepository.Query().FirstOrDefault(x => x.Id == id);
        var wigetZone = _widgetZoneRepository.Query().ToList();
        var total = _widgetInstanceRepository.Query().Count();
        var model = new SpaceBarWidgetForm
        {
            Id = widgetInstance.Id,
            Name = widgetInstance.Name,
            WidgetZoneId = widgetInstance.WidgetZoneId,
            PublishStart = widgetInstance.PublishStart,
            PublishEnd = widgetInstance.PublishEnd,
            DisplayOrder = widgetInstance.DisplayOrder,
            Items = JsonConvert.DeserializeObject<IList<SpaceBarWidgetSetting>>(widgetInstance.Data)
        };
      
        model.WidgetZoneItems = wigetZone.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
        model.numberOfWidgets = total;
        foreach (var item in model.Items)
        {
            if (string.IsNullOrEmpty(item.Image))
            {
                continue;
            }

            item.ImageUrl = _mediaService.GetMediaUrl(item.Image);
        }

        return View(model);
    }
    [HttpPost("{id}")]
    public async Task<IActionResult> Edit(long id, IFormCollection formCollection)
    {
       var model = ToSpaceBarWidgetFormModel(formCollection);

        foreach (var item in model.Items)
        {
            if (item.UploadImage != null)
            {
                if (!string.IsNullOrWhiteSpace(item.Image))
                {
                    await _mediaService.DeleteMediaAsync(item.Image);
                }
                item.Image = await SaveFile(item.UploadImage);
            }
        }

        if (ModelState.IsValid)
        {
            var widgetInstance = _widgetInstanceRepository.Query().FirstOrDefault(x => x.Id == id);
            widgetInstance.Name = model.Name;
            widgetInstance.PublishStart = model.PublishStart;
            widgetInstance.PublishEnd = model.PublishEnd;
            widgetInstance.WidgetZoneId = model.WidgetZoneId;
            widgetInstance.DisplayOrder = model.DisplayOrder;
            widgetInstance.Data = JsonConvert.SerializeObject(model.Items);
            _widgetInstanceRepository.SaveChanges();
            return Accepted();
        }

        return BadRequest(ModelState);
    }

    private SpaceBarWidgetForm ToSpaceBarWidgetFormModel(IFormCollection formCollection)
    {
        var model = new SpaceBarWidgetForm();
        model.Name = formCollection["Name"];
        model.WidgetZoneId = int.Parse(formCollection["WidgetZoneId"]);
        int.TryParse(formCollection["DisplayOrder"], out int displayOrder);
        model.DisplayOrder = displayOrder;
        if (DateTimeOffset.TryParse(formCollection["PublishStart"], out DateTimeOffset publishStart))
        {
            model.PublishStart = publishStart;
        }

        if (DateTimeOffset.TryParse(formCollection["PublishEnd"], out DateTimeOffset publishEnd))
        {
            model.PublishEnd = publishEnd;
        }

        int numberOfItems = int.Parse(formCollection["NumberOfItems"]);
        for (var i = 0; i < numberOfItems; i++)
        {
            var item = new SpaceBarWidgetSetting();
            item.Title = formCollection[$"items[{i}].Title"];
            item.Description = formCollection[$"items[{i}].Description"];
            item.IconHtml = formCollection[$"items[{i}].IconHtml"];
            item.Image = formCollection[$"items[{i}].Image"];
            item.UploadImage = formCollection.Files[$"items[{i}].UploadImage"];
            model.Items.Add(item);
        }

        return model;
    }

    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, file.ContentType);
        return fileName;
    }
}
