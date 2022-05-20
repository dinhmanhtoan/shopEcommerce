namespace Shop.Components;

public class ProductFutureViewComponent : ViewComponent
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMediaService _mediaService;
    public ProductFutureViewComponent(IRepository<Product> productRepository, IMediaService mediaService)
    {
        _productRepository = productRepository;
        _mediaService = mediaService;

    }
    public IViewComponentResult Invoke()
    {
        var query = _productRepository.Query().Where(x => x.IsFuture == true && !x.IsDeleted && x.IsVisibleIndividually).Include(x => x.Images).ThenInclude(x=> x.Media).Include(x => x.Brand).Include(x => x.Categories)
            .Include(x => x.Thumbnail).Include(x => x.OptionValues).ThenInclude(x => x.Option).ToList().Take(12);
        var productvms = new List<ProductVm>();

        foreach (var item in query)
        {
            var productvm = new ProductVm()
            {
                Id = item.Id,
                Sku = item.Sku,
                Name = item.Name,
                Slug = item.Slug,
                NormalizedName = item.NormalizedName,
                Description = item.Description,
                ShortDescription = item.ShortDescription,
                BrandId = item.BrandId,
                Brand = item.Brand,
                categories = item.Categories.Select(x=> x.Category).ToList(),
                ReviewsCount = item.ReviewsCount,
                RatingAverage = item.RatingAverage,
                Price = item.Price,
                ThumbnailImageUrl = _mediaService.GetMediaUrl(item.Thumbnail)
            };
            foreach (var productMedia in item.Images.Where(x => x.Media.MediaType == MediaType.Image))
            {
                productvm.ProductImages.Add(new ProductMediaVm
                {
                    Id = productMedia.Id,
                    MediaUrl = _mediaService.GetThumbnailUrl(productMedia.Media),
                    Media = productMedia.Media.FileName
                });
            }
            productvm.Options = item.OptionValues.OrderBy(x => x.SortIndex).Select(x => new ProductOptionVm()
            {
                Id = x.OptionId,
                Name = x.Option.Name,
                DisplayType = x.DisplayType,
                Values = JsonConvert.DeserializeObject<IList<ProductOptionValueVm>>(x.Value)
            }).ToList();

            productvms.Add(productvm);

    }
        return View(productvms);
    }
}



