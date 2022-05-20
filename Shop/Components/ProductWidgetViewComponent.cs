namespace Shop.Components;
public class ProductWidgetViewComponent : ViewComponent
{

    private readonly IRepository<Product> _productRepository;
    private readonly IMediaService _mediaService;
    private readonly IProductPricingService _productPricingService;
    private readonly IContentLocalizationService _contentLocalizationService;

    public ProductWidgetViewComponent(IRepository<Product> productRepository,
        IMediaService mediaService,
        IProductPricingService productPricingService,

        IContentLocalizationService contentLocalizationService)
    {
        _productRepository = productRepository;
        _mediaService = mediaService;
        _productPricingService = productPricingService;
        _contentLocalizationService = contentLocalizationService;
    }

    public IViewComponentResult Invoke(WidgetInstanceViewModel widgetInstance)
    {
        var model = new ProductWidgetComponentVm
        {
            Id = widgetInstance.Id,
            WidgetName = _contentLocalizationService.GetLocalizedProperty(nameof(WidgetInstance), widgetInstance.Id, nameof(widgetInstance.Name), widgetInstance.Name),
            Setting = JsonConvert.DeserializeObject<ProductWidgetSetting>(widgetInstance.Data)
        };
        var query = _productRepository.Query()
                                      .Include(x => x.Thumbnail)
                                      .Include(x => x.Categories)
                                      .Where(x => x.IsPublished && x.IsVisibleIndividually && !x.IsDeleted);


        if (model.Setting.CategoryId.HasValue && model.Setting.CategoryId.Value > 0)
        {
            query = query.Where(x => x.Categories.Any(c => c.CategoryId == model.Setting.CategoryId.Value));
        }

        if (model.Setting.FeaturedOnly)
        {
            query = query.Where(x => x.IsFuture);
        }
        model.Products = query
          .OrderByDescending(x => x.CreatedOn)
          .Take(model.Setting.NumberOfProducts)
          .Select(x => ProductThumbnail.FromProduct(x)).ToList();

        foreach (var product in model.Products)
        {
            product.Name = _contentLocalizationService.GetLocalizedProperty(nameof(Product), product.Id, nameof(product.Name), product.Name);
            product.ThumbnailUrl = _mediaService.GetThumbnailUrl(product.ThumbnailImage);
            product.CalculatedProductPrice = _productPricingService.CalculateProductPrice(product);
        }

        return View(model);
    }
}
