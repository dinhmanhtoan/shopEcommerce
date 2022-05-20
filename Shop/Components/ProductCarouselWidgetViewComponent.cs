namespace Shop.Components;
public class ProductCarouselWidgetViewComponent : ViewComponent
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Brand> _brandRespository;
    private readonly IMediaService _mediaService;
    private readonly IProductPricingService _productPricingService;
    private readonly IContentLocalizationService _contentLocalizationService;


    public ProductCarouselWidgetViewComponent(IRepository<Product> productRepository,
        IRepository<Brand> brandRespository,
        IMediaService mediaService,
        IProductPricingService productPricingService,
        IContentLocalizationService contentLocalizationService
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
        var model = new ProductCarouselWidgetComponentVm
        {
            Id = widgetInstance.Id,
            WidgetName = _contentLocalizationService.GetLocalizedProperty(nameof(WidgetInstance), widgetInstance.Id, nameof(widgetInstance.Name), widgetInstance.Name),
            Setting = JsonConvert.DeserializeObject<ProductWidgetSetting>(widgetInstance.Data)
        };

        var query = _productRepository.Query().Where(x => x.IsPublished && x.IsVisibleIndividually);
        if (model.Setting.CategoryId.HasValue && model.Setting.CategoryId.Value > 0)
        {
            query = query.Where(x => x.Categories.Any(c => c.CategoryId == model.Setting.CategoryId.Value));
        }
        var ListBrand = query.ToList().Select(x => x.BrandId).Distinct();
        model.Brands = _brandRespository.Query().Where(x => ListBrand.Contains(x.Id)).ToList();
        if (model.Setting.FeaturedOnly)
        {
            query = query.Where(x => x.IsFuture);
        }
        switch (model.Setting.OrderBy.ToString())
        {
            case "Newest":
                query = query.OrderByDescending(x => x.CreatedOn);
                break;
            case "BestSelling":
                query = query.Where(x => x.IsFuture);
                break;
            case "Discount":
                query = query.Where(x => x.SpecialPrice.Value > 0 && (x.SpecialPriceEnd > System.DateTime.Now || x.SpecialPriceStart > System.DateTime.Now));
                break;

        }
        model.Products = query
          .Include(x => x.Thumbnail)
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
