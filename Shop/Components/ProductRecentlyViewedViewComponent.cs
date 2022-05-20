namespace Shop.Components;

public class ProductRecentlyViewedViewComponent : ViewComponent
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<RecentlyViewedProduct> _recentlyViewedProductRepository;
    private readonly IProductPricingService _productPricingService;
    private readonly IMediaService _mediaService;
    private readonly IWorkContext _workContext;
    public ProductRecentlyViewedViewComponent(IRepository<Product> productRepository,
        IRepository<RecentlyViewedProduct> recentlyViewedProductRepository,
        IProductPricingService productPricingService,
        IMediaService mediaService, IWorkContext workContext)
    {
        _productRepository = productRepository;
        _recentlyViewedProductRepository = recentlyViewedProductRepository;
        _productPricingService = productPricingService;
        _mediaService = mediaService;
        _workContext = workContext;
    }
    public async Task<IViewComponentResult> InvokeAsync(long? productId, int itemCount = 4)
    {
        var user = await _workContext.GetCurrentUser();
        var query = from p in _productRepository.Query()
                    join r in _recentlyViewedProductRepository.Query() on p.Id equals r.ProductId
                    where r.UserId == user.Id
                    orderby r.LatestViewedOn descending
                    select p;
        query = query.Include(x => x.Thumbnail).Where(x=> !x.IsDeleted);
        if (productId.HasValue)
        {
            query = query.Where(x => x.Id != productId);
        }
        var model =  query.Take(itemCount).Select(x => ProductThumbnail.FromProduct(x)).ToList();
        foreach (var product in model)
        {
            product.Name = product.Name;
            product.ThumbnailUrl = _mediaService.GetThumbnailUrl(product.ThumbnailImage);
            product.CalculatedProductPrice = _productPricingService.CalculateProductPrice(product);
        }
        return View(model);
    }
}

