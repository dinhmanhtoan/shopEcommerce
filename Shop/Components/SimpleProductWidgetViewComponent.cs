namespace Shop.Components;
public class SimpleProductWidgetViewComponent : ViewComponent
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMediaService _mediaService;
    private readonly IProductPricingService _productPricingService;
    private readonly IContentLocalizationService _contentLocalizationService;

    public SimpleProductWidgetViewComponent(IRepository<Product> productRepository,
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
        var model = new SimpleProductWidgetComponentVm
        {
            Id = widgetInstance.Id,
            WidgetName = _contentLocalizationService.GetLocalizedProperty(nameof(WidgetInstance), widgetInstance.Id, nameof(widgetInstance.Name), widgetInstance.Name),
            Setting = JsonConvert.DeserializeObject<SimpleProductWidgetSetting>(widgetInstance.Data)
        };

        foreach (var item in model.Setting.Products)
        {
            var product = _productRepository.Query().Where(x => x.Id == item.Id).Include(x => x.Thumbnail).FirstOrDefault();

            if (product != null)
            {
                var productThumbnail = ProductThumbnail.FromProduct(product);
                productThumbnail.Name = _contentLocalizationService.GetLocalizedProperty(nameof(Product), productThumbnail.Id, nameof(product.Name), productThumbnail.Name);
                productThumbnail.ThumbnailUrl = _mediaService.GetThumbnailUrl(product.Thumbnail);
                productThumbnail.CalculatedProductPrice = _productPricingService.CalculateProductPrice(product);
                model.Products.Add(productThumbnail);
            }
        }

        return View(model);
    }
}
