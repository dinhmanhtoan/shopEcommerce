namespace Shop.Components;
public class RecentlyViewedWidgetViewComponent : ViewComponent
{
    private IMediaService _mediaService;

    public RecentlyViewedWidgetViewComponent(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public IViewComponentResult Invoke(WidgetInstanceViewModel widgetInstance)
    {
        var model = new RecentlyViewedWidgetViewComponentVm
        {
            Id = widgetInstance.Id,
            ItemCount = JsonConvert.DeserializeObject<int>(widgetInstance.Data)
        };

        return View(model);
    }
}
