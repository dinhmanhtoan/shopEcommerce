namespace Shop.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;
    private readonly IMediaService _mediaService;
    private readonly IRepository<Product> _productRepository;
    private readonly IMediator _mediator;
    private readonly IProductPricingService _productPricingService;
    public ProductController
    (
        ILogger<HomeController> logger, IProductService productService,
        ICategoryService categoryService, IRepository<Product> productRepository, IBrandService brandService,
        IMediaService mediaService , IMediator mediator,
        IProductPricingService productPricingService
    )
    {
        _logger = logger;
        _productService = productService;
        _categoryService = categoryService;
        _productRepository = productRepository;
        _brandService = brandService;
        _mediaService = mediaService;
        _mediator = mediator;
        _productPricingService = productPricingService;

    }
    [HttpGet]
    public async Task<IActionResult> ProductDetail(long Id)
    {
        var product = await _productRepository.Query().Include(x => x.OptionValues)
            .Include(x => x.Brand)
            .Include(x=> x.AttributeValues).ThenInclude(x=> x.Attribute)
            .Include(x => x.ProductLinks).ThenInclude(p => p.LinkedProduct).ThenInclude(m => m.Thumbnail)
            .Include(x => x.Categories).ThenInclude(x => x.Category)
            .Include(x => x.Images).ThenInclude(x => x.Media)
            .Include(x => x.Thumbnail).FirstOrDefaultAsync(x => x.Id == Id && x.IsPublished);
        if (product == null)
        {
            return NotFound();
        }
        var model = new ProductDetail()
        {
            Id = product.Id,
            Name = product.Name,
            Brand = product.Brand,
            NormalizedName = product.NormalizedName,
            Slug = product.Slug,
            Sku = product.Sku,
            Gtin = product.Gtin,
            CalculatedProductPrice = _productPricingService.CalculateProductPrice(product),
            IsCallForPricing = product.IsCallForPricing,
            IsAllowToOrder = product.IsAllowToOrder,
            StockTrackingIsEnabled = product.StockTrackingIsEnabled,
            StockQuantity = product.StockQuantity,
            Description = product.Description,
            ShortDescription = product.ShortDescription,
            MetaTitle = product.MetaTitle,
            MetaKeywords = product.MetaKeywords,
            MetaDescription = product.MetaDescription,
            Price = product.Price,
            OldPrice = product.OldPrice,
            SpecialPrice = product.SpecialPrice,
            SpecialPriceStart = product.SpecialPriceStart,
            SpecialPriceEnd = product.SpecialPriceEnd,
            Specification = product.Specification,
            Present = product.Present,
            ReviewsCount = product.ReviewsCount,
            RatingAverage = product.RatingAverage,
            Attributes = product.AttributeValues.Select(x => new ProductDetailAttribute { Name = x.Attribute.Name, Value = x.Value }).ToList(),
            Categories = product.Categories.Select(x => new ProductDetailCategory { Id = x.CategoryId, Name = x.Category.Name, Slug = x.Category.Slug }).ToList()
    
        };
        MapProductVariantToProductVm(product, model);
        MapProductOptionToProductVm(product, model);
        MapProductImagesToProductVm(product, model);
        MapRelatedProductToProductVm(product, model);
        await _mediator.Publish(new EntityViewed { EntityId = product.Id, EntityTypeId = "Product" });
        await _productRepository.SaveChangesAsync();
        return View(model);
    }
    private void MapProductVariantToProductVm(Product product,ProductDetail model)
    {
        if (!product.ProductLinks.Any(x => x.LinkType == ProductLinkType.Super))
        {
            return;
        }
        var variations = _productRepository.Query()
            .Include(x => x.OptionCombinations).ThenInclude(o => o.Option)
            .Include(x => x.Images).ThenInclude(m => m.Media)
            .Where(x => x.LinkedProductLinks.Any(link => link.ProductId == product.Id && link.LinkType == ProductLinkType.Super))
            .Where(x => x.IsPublished)
            .ToList();
        foreach (var variation in variations)
        {
            var variationVm = new ProductDetailVariation
            {
                Id = variation.Id,
                Name = variation.Name,
                NormalizedName = variation.NormalizedName,
                IsAllowToOrder = variation.IsAllowToOrder,
                IsCallForPricing = variation.IsCallForPricing,
                StockTrackingIsEnabled = variation.StockTrackingIsEnabled,
                StockQuantity = variation.StockQuantity,
                CalculatedProductPrice = _productPricingService.CalculateProductPrice(variation),
                Images = variation.Images.Where(x => x.Media.MediaType == MediaType.Image).Select(productMedia => new MediaViewModel
                {
                    Url = _mediaService.GetMediaUrl(productMedia.Media),
                    ThumbnailUrl = _mediaService.GetThumbnailUrl(productMedia.Media)
                }).ToList()
            };
            var optionCombinations = variation.OptionCombinations.OrderBy(x => x.SortIndex);
            foreach (var combination in optionCombinations)
            {
                variationVm.Options.Add(new ProductDetailVariationOption
                {
                    OptionId = combination.OptionId,
                    OptionName = combination.Option.Name,
                    Value = combination.Value
                });
            }

            model.Variations.Add(variationVm);
        }
    }
    private void MapProductImagesToProductVm(Product product, ProductDetail model)
    {
        model.Images = product.Images.Where(x=> x.Media.MediaType ==  MediaType.Image).Select(productMedia => new MediaViewModel
        {
            Url = _mediaService.GetMediaUrl(productMedia.Media),
            ThumbnailUrl = _mediaService.GetThumbnailUrl(productMedia.Media)
        }).ToList();
    }
    private void MapProductOptionToProductVm(Product product, ProductDetail model)
    {
        foreach (var item in product.OptionValues)
        {
            var optionValues = JsonConvert.DeserializeObject<IList<ProductOptionValueVm>>(item.Value);
            foreach (var value in optionValues)
            {
                if (!model.OptionDisplayValues.ContainsKey(value.Key))
                {
                    model.OptionDisplayValues.Add(value.Key, new ProductOptionDisplay { DisplayType = item.DisplayType, Value = value.Display });
                }
            }
        }
    }

    private void MapRelatedProductToProductVm(Product product, ProductDetail model)
    {
        var publishedProductLinks = product.ProductLinks.Where(x => x.LinkedProduct.IsPublished && (x.LinkType == ProductLinkType.Related || x.LinkType == ProductLinkType.CrossSell));
        foreach (var productLink in publishedProductLinks)
        {
            var linkedProduct = productLink.LinkedProduct;
            var productThumbnail = ProductThumbnail.FromProduct(linkedProduct);
            productThumbnail.Name = productThumbnail.Name;
            productThumbnail.ThumbnailUrl = _mediaService.GetThumbnailUrl(linkedProduct.Thumbnail);
            productThumbnail.CalculatedProductPrice = _productPricingService.CalculateProductPrice(linkedProduct);

            if (productLink.LinkType == ProductLinkType.Related)
            {
                model.RelatedProducts.Add(productThumbnail);
            }

            if (productLink.LinkType == ProductLinkType.CrossSell)
            {
                model.CrossSellProducts.Add(productThumbnail);
            }
        }
    }
}

