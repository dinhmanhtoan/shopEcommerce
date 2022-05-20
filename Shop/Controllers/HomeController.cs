namespace Shop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWidgetInstanceService _widgetInstanceService;
    private readonly IContentLocalizationService _contentLocalizationService;

    public HomeController(ILogger<HomeController> logger, IWidgetInstanceService widgetInstanceService, IContentLocalizationService contentLocalizationService)
    {
        _logger = logger;
        _widgetInstanceService = widgetInstanceService;
        _contentLocalizationService = contentLocalizationService;
    }

    public IActionResult Index()
    {
        var model = new HomeViewModel();
        //var getWidgetInstanceTranslations = _contentLocalizationService.GetLocalizationFunction<WidgetInstance>();
        //getWidgetInstanceTranslations(x.Id, nameof(x.Name), x.Name),
        //getWidgetInstanceTranslations(x.Id, nameof(x.HtmlData), x.HtmlData),
        model.WidgetInstances = _widgetInstanceService.GetPublished()
            .OrderBy(x => x.DisplayOrder)
            .Select(x => new WidgetInstanceViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ViewComponentName = x.Widget.ViewComponentName,
                WidgetId = x.WidgetId,
                WidgetZoneId = x.WidgetZoneId,
                Data = x.Data,
                HtmlData = x.HtmlData,
            }).ToList();

        return View(model);
    }
    public IActionResult Privacy()
    {
        return View();
    }


    [Route("/Home/ErrorWithCode/{statusCode}")]
    public IActionResult ErrorWithCode(int statusCode)
    {
        if (statusCode == 404)
        {
            return View("404");
        }

        return View("Error");
    }
}

