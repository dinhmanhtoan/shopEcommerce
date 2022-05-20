
using Microsoft.Extensions.Localization;

namespace Shop.Components;

public class CarouselWidgetViewComponent : ViewComponent
{
    private readonly IMediaService _mediaService;
    // private readonly IStringLocalizer _localizer;
    //IStringLocalizerFactory stringLocalizerFactory
    public CarouselWidgetViewComponent(IMediaService mediaService)
    {
        _mediaService = mediaService;
       // _localizer = stringLocalizerFactory.Create(null);
    }

    public IViewComponentResult Invoke(WidgetInstanceViewModel widgetInstance)
    {
        if (widgetInstance == null)
        {
            throw new ArgumentNullException(nameof(widgetInstance));
        }

        var model = new CarouselWidgetViewComponentVm
        {
            Id = widgetInstance.Id,
            Items = JsonConvert.DeserializeObject<IList<CarouselWidgetViewComponentItemVm>>(widgetInstance.Data)
        };

        foreach (var item in model.Items)
        {
            item.Image = _mediaService.GetMediaUrl(item.Image);

            //if (!string.IsNullOrWhiteSpace(item.Caption)) { item.Caption = _localizer.GetString(item.Caption); }
            //if (!string.IsNullOrWhiteSpace(item.SubCaption)) { item.SubCaption = _localizer.GetString(item.SubCaption); }
            //if (!string.IsNullOrWhiteSpace(item.LinkText)) { item.LinkText = _localizer.GetString(item.LinkText); }
        }
        return View(model);
    }
}

