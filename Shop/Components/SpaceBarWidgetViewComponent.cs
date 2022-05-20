using Microsoft.Extensions.Localization;

namespace Shop.Components;
public class SpaceBarWidgetViewComponent : ViewComponent
{
    private readonly IMediaService _mediaService;
    //private readonly IStringLocalizer _localizer;
   // private readonly IContentLocalizationService _contentLocalizationService;
   // IStringLocalizerFactory stringLocalizerFactory, IContentLocalizationService contentLocalizationService
    public SpaceBarWidgetViewComponent(IMediaService mediaService)
    {
        _mediaService = mediaService;
       // _localizer = stringLocalizerFactory.Create(null);
        //_contentLocalizationService = contentLocalizationService;
    }
   // _contentLocalizationService.GetLocalizedProperty(nameof(WidgetInstance), widgetInstance.Id, nameof(widgetInstance.Name), widgetInstance.Name),
    public IViewComponentResult Invoke(WidgetInstanceViewModel widgetInstance)
    {
        var model = new SpaceBarWidgetComponentVm
        {
            Id = widgetInstance.Id,
            WidgetName = widgetInstance.Name,
            Items = JsonConvert.DeserializeObject<List<SpaceBarWidgetSetting>>(widgetInstance.Data)
        };

        foreach (var item in model.Items)
        {
           // if (!string.IsNullOrWhiteSpace(item.Title)) { item.Title = _localizer[item.Title]; }
           // if (!string.IsNullOrWhiteSpace(item.Description)) { item.Description = _localizer[item.Description]; }

            if (!string.IsNullOrEmpty(item.Image))
            {
                item.ImageUrl = _mediaService.GetMediaUrl(item.Image);
            }
        }

        return View(model);
    }
}
