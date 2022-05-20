using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("[controller]/[action]")]
    public class CarouselWidgetController : Controller
    {
        private readonly IRepository<WidgetInstance> _widgetInstanceRepository;
        private readonly IRepository<WidgetZone> _widgetZoneRepository;
        private readonly IMediaService _mediaService;

        public CarouselWidgetController(IRepository<WidgetInstance> widgetInstanceRepository, IRepository<WidgetZone> widgetZoneRepository, IMediaService mediaService)
        {
            _widgetInstanceRepository = widgetInstanceRepository;
            _mediaService = mediaService;
            _widgetZoneRepository = widgetZoneRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var wigetZone = _widgetZoneRepository.Query().ToList();
            var model = new CarouselWidgetForm();
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
        public async Task<IActionResult> Create(CarouselWidgetForm model)
        {
            ModelBindUploadFiles(model);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (model.Items.Any(x => x.UploadImage == null))
            {
                ModelState.AddModelError("Images", "Images is required");
                return BadRequest(ModelState);
            }

            foreach (var item in model.Items)
            {
                item.Image = await SaveFile(item.UploadImage);
            }

            var widgetInstance = new WidgetInstance
            {
                Name = model.Name,
                WidgetId = "CarouselWidget",
                WidgetZoneId = model.WidgetZoneId,
                PublishStart = model.PublishStart,
                PublishEnd = model.PublishEnd,
                DisplayOrder = model.DisplayOrder,
                Data = JsonConvert.SerializeObject(model.Items)
            };

            _widgetInstanceRepository.Add(widgetInstance);
            await _widgetInstanceRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            var widgetInstance = await _widgetInstanceRepository.Query().FirstOrDefaultAsync(x => x.Id == id);
            var wigetZone = _widgetZoneRepository.Query().ToList();
            var total = _widgetInstanceRepository.Query().Count();
            var model = new CarouselWidgetForm
            {
                Id = widgetInstance.Id,
                Name = widgetInstance.Name,
                WidgetZoneId = widgetInstance.WidgetZoneId,
                PublishStart = widgetInstance.PublishStart,
                PublishEnd = widgetInstance.PublishEnd,
                DisplayOrder = widgetInstance.DisplayOrder,
                Items = JsonConvert.DeserializeObject<IList<CarouselWidgetItemForm>>(widgetInstance.Data)
            };
            model.WidgetZoneItems = wigetZone.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == model.WidgetZoneId ? true : false
            }).ToList();
            model.numberOfWidgets = total;
            foreach (var item in model.Items)
            {
                item.ImageUrl = _mediaService.GetMediaUrl(item.Image);
            }

            return View(model);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(long id,CarouselWidgetForm model)
        {
            ModelBindUploadFiles(model);
            if (!ModelState.IsValid) return BadRequest(ModelState);
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
            widgetInstance.Data = JsonConvert.SerializeObject(model.Items);

            await _widgetInstanceRepository.SaveChangesAsync();
            return Ok();
        }

        private CarouselWidgetForm ModelBindUploadFiles(CarouselWidgetForm model)
        {
            for (var i = 0; i < model.Items.Count; i++)
            {
                model.Items[i].UploadImage = Request.Form.Files[$"Items[{i}].UploadImage"];
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
}
