using SearchOption = Model.ViewModel.SearchOption;
namespace Shop.Controllers;

public class BrandController : Controller
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Brand> _brandRepository;
    private readonly IMediaService _mediaService;
    private readonly IProductPricingService _productPricingService;
    int _pageSize = 24;
    public BrandController(IRepository<Product> productRepository, IRepository<Brand> brandRepository,
        IMediaService mediaService, IProductPricingService productPricingService)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _mediaService = mediaService;
        _productPricingService = productPricingService;
    }
    public IActionResult BrandDetail(long id, SearchOption searchOption)
    {
        var brand = _brandRepository.Query().FirstOrDefault(x => x.Id == id);
        if (brand == null)
        {
            return NotFound();
        }

        var model = new ProductsByBrand
        {
            BrandId = id,
            BrandName = brand.Name,
            BrandSlug = brand.Slug,
            CurrentSearchOption = searchOption,
            FilterOption = new FilterOption()
        };

        var query = _productRepository.Query().Where(x => x.BrandId == id && x.IsPublished && !x.IsDeleted && x.IsVisibleIndividually);

        if (query.Count() == 0)
        {
            model.TotalProduct = 0;
            return View(model);
        }

        AppendFilterOptionsToModel(model, query);

        if (searchOption.MinPrice.HasValue)
        {
            query = query.Where(x => x.Price >= searchOption.MinPrice.Value);
        }

        if (searchOption.MaxPrice.HasValue)
        {
            query = query.Where(x => x.Price <= searchOption.MaxPrice.Value);
        }

        var categories = searchOption.GetCategories().ToArray();
        if (categories.Any())
        {
            query = query.Where(x => x.Categories.Any(c => categories.Contains(c.Category.Slug)));
        }

        model.TotalProduct = query.Count();
        var currentPageNum = searchOption.Page <= 0 ? 1 : searchOption.Page;
        var offset = (_pageSize * currentPageNum) - _pageSize;
        while (currentPageNum > 1 && offset >= model.TotalProduct)
        {
            currentPageNum--;
            offset = (_pageSize * currentPageNum) - _pageSize;
        }

        query = ApplySort(searchOption, query);

        var products = query
            .Include(x => x.Thumbnail)
            .Skip(offset)
            .Take(_pageSize)
            .Select(x => ProductThumbnail.FromProduct(x))
            .ToList();

        foreach (var product in products)
        {
            product.Name = product.Name;
            product.ThumbnailUrl = _mediaService.GetThumbnailUrl(product.ThumbnailImage);
            product.CalculatedProductPrice = _productPricingService.CalculateProductPrice(product);
        }

        model.Products = products;
        model.CurrentSearchOption.PageSize = _pageSize;
        model.CurrentSearchOption.Page = currentPageNum;

        return View(model);
    }

    private static IQueryable<Product> ApplySort(SearchOption searchOption, IQueryable<Product> query)
    {
        var sortBy = searchOption.Sort ?? string.Empty;
        switch (sortBy.ToLower())
        {
            case "title-asc":
                query = query.OrderBy(x => x.Name);
                break;
            case "title-desc":
                query = query.OrderByDescending(x => x.Name);
                break;
            case "price-asc":
                query = query.OrderBy(x => x.Price);
                break;
            case "price-desc":
                query = query.OrderByDescending(x => x.Price);
                break;
        }
        return query;
    }

    private void AppendFilterOptionsToModel(ProductsByBrand model, IQueryable<Product> query)
    {
        model.FilterOption.Price.MaxPrice = query.Max(x => x.Price);
        model.FilterOption.Price.MinPrice = query.Min(x => x.Price);


        model.FilterOption.Categories = query
            .SelectMany(x => x.Categories).Where(x => x.Category.IsPublished)
            .GroupBy(x => new {
                x.Category.Id,
                x.Category.Name,
                x.Category.Slug,
                x.Category.ParentId
            })
            .Select(g => new FilterCategory
            {
                Id = (int)g.Key.Id,
                Name = g.Key.Name,
                Slug = g.Key.Slug,
                ParentId = g.Key.ParentId,
                Count = g.Count()
            })
            .ToList();
    }
}

