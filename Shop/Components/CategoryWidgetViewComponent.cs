namespace Shop.Components;

public class CategoryWidgetViewComponent : ViewComponent
{
    private readonly IRepository<Category> _categoriesRepository;
    private readonly IMediaService _mediaService;
    private readonly IContentLocalizationService _contentLocalizationService;

    public CategoryWidgetViewComponent(IRepository<Category> categoriesRepository, IMediaService mediaService, IContentLocalizationService contentLocalizationService)
    {
        _categoriesRepository = categoriesRepository;
        _mediaService = mediaService;
        _contentLocalizationService = contentLocalizationService;
    }

    public IViewComponentResult Invoke(WidgetInstanceViewModel widgetInstance)
    {
        var model = new CategoryWidgetComponentVm()
        {
            Id = widgetInstance.Id,
            WidgetName = _contentLocalizationService.GetLocalizedProperty(nameof(WidgetInstance), widgetInstance.Id, nameof(widgetInstance.Name), widgetInstance.Name),
        };
        var settings = JsonConvert.DeserializeObject<CategoryWidgetSettings>(widgetInstance.Data);
        if (settings != null)
        {
            var category = _categoriesRepository.Query()
                .Include(c => c.Thumbnail)
                .FirstOrDefault(c => c.Id == settings.CategoryId);
            model.Category = new CategoryThumbnail()
            {
                Id = category.Id,
                //Description = category.Description,
                Name = category.Name,
                Slug = category.Slug,
                ThumbnailImage = category.Thumbnail,
                ThumbnailUrl = _mediaService.GetThumbnailUrl(category.Thumbnail)
            };
        }

        return View(model);
    }
}
