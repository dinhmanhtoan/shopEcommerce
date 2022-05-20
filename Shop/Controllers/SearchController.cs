using SearchOption = Model.ViewModel.SearchOption;

namespace Shop.Controllers;

public class SearchController : Controller
{
    private int _pageSize = 24;
    private readonly IRepository<Query> _queryRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMediaService _mediaService;
    private readonly IProductPricingService _productPricingService;

    public SearchController(shopContext context, IMediaService mediaService,
        IRepository<Query> queryRepository,
        IRepository<Product> productRepository,
        IRepository<Category> categoryRepository,
        IProductPricingService productPricingService
        )
    {
        _queryRepository = queryRepository;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mediaService = mediaService;
        _productPricingService = productPricingService;
    }
    [HttpGet("Search")]
    public IActionResult Search(SearchOption searchOption)
    {
        if (string.IsNullOrWhiteSpace(searchOption.Query))
        {
            return Redirect("~/");
        }
        var model = new SearchResult()
        {
            CurrentSearchOption = searchOption,
            FilterOption = new FilterOption()
        };
        var query = _productRepository.Query().Where(x => x.Name.Contains(searchOption.Query) && x.IsPublished && !x.IsDeleted && x.IsVisibleIndividually);
        if (!query.Any())
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
        var brands = searchOption.GetBrands().ToArray();
        if (brands.Any())
        {
            query = query.Where(x => x.BrandId != null && brands.Contains(x.Brand.Slug));
        }
        var categories = searchOption.GetCategories();
        if (categories.Any())
        {
            query = query.Where(p => p.Categories.Select(c => c.CategoryId).Intersect(_categoryRepository.Query().Where(cat => categories.Contains(cat.Slug)).Select(c => c.Id)).Any());
        }
         
        model.TotalProduct = query.Count();
        var currentPageNum = searchOption.Page <= 0 ? 1 : searchOption.Page;
        var offset = (_pageSize * currentPageNum) - _pageSize;
        while (currentPageNum > 1 && offset >= model.TotalProduct)
        {
            currentPageNum--;
            offset = (_pageSize * currentPageNum) - _pageSize;
        }

          SaveSearchQuery(searchOption, model);

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

    private void AppendFilterOptionsToModel(SearchResult model, IQueryable<Product> query)
    {
        model.FilterOption.Price.MaxPrice = query.Max(x => x.Price);
        model.FilterOption.Price.MinPrice = query.Min(x => x.Price);

   

        model.FilterOption.Categories = query
            .SelectMany(x => x.Categories)
            .GroupBy(x => new
            {
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
            }).ToList();

        // TODO an EF Core bug, so we have to do evaluation in client
        model.FilterOption.Brands = query.Include(x => x.Brand)
            .Where(x => x.BrandId != null).ToList()
            .GroupBy(x => x.Brand)
            .Select(g => new FilterBrand
            {
                Id = (int)g.Key.Id,
                Name = g.Key.Name,
                Slug = g.Key.Slug,
                Count = g.Count()
            }).ToList();
    }

    private void SaveSearchQuery(SearchOption searchOption, SearchResult model)
    {
        var query = new Query
        {
            CreatedOn = DateTimeOffset.Now,
            QueryText = searchOption.Query,
            ResultsCount = model.TotalProduct
        };

        _queryRepository.Add(query);
        _queryRepository.SaveChanges();
    }
}

