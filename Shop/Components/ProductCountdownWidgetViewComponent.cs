namespace Shop.Components;
public class ProductCountdownWidgetViewComponent : ViewComponent
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Brand> _brandRespository;
    private readonly IMediaService _mediaService;
    private readonly IProductPricingService _productPricingService;
    private readonly IContentLocalizationService _contentLocalizationService;


    public ProductCountdownWidgetViewComponent(IRepository<Product> productRepository,
        IRepository<Brand> brandRespository,
        IMediaService mediaService,
        IProductPricingService productPricingService,
        ContentLocalizationService contentLocalizationService
        )
    {
        _productRepository = productRepository;
        _mediaService = mediaService;
        _productPricingService = productPricingService;
        _contentLocalizationService = contentLocalizationService;
        _brandRespository = brandRespository;
    }

    public IViewComponentResult Invoke(WidgetInstanceViewModel widgetInstance)
    {
        var Setting = JsonConvert.DeserializeObject<ProductWidgetSetting>(widgetInstance.Data);
        var query = _productRepository.Query().Where(x => x.IsPublished && x.IsVisibleIndividually && x.Id == Setting.ProductId);
        var model = new ProductCountDownWidgetComponentVm
        {
            Id = widgetInstance.Id,
            WidgetName = _contentLocalizationService.GetLocalizedProperty(nameof(WidgetInstance), widgetInstance.Id, nameof(widgetInstance.Name), widgetInstance.Name),
            Products = query.Include(x => x.Thumbnail).Select(x => ProductThumbnail.FromProduct(x)).SingleOrDefault()
        };
        model.Products.ThumbnailUrl = _mediaService.GetThumbnailUrl(model.Products.ThumbnailImage);
        model.Products.CalculatedProductPrice = _productPricingService.CalculateProductPrice(model.Products);
        return View(model);
    }
}
