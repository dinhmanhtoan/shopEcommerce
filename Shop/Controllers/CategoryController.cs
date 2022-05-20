using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchOption = Model.ViewModel.SearchOption;
namespace Shop.Controllers;

public class CategoryController : Controller
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IMediaService _mediaService;
    private readonly IProductPricingService _productPricingService;
    int _pageSize = 24;
    public CategoryController(IRepository<Category> categoryRepository, IRepository<Product> productRepository,
        IMediaService mediaService, IProductPricingService productPricingService)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _mediaService = mediaService;
        _productPricingService = productPricingService;
    }
    public IActionResult CategoryDetail(long Id, SearchOption searchOption)
        {
        var category = _categoryRepository.Query().FirstOrDefault(x => x.Id == Id);
        if (category == null)
        {
            return NotFound();
        }
        var model = new ProductsByCategory()
        {
            CategoryId = category.Id,
            ParentCategorId = category.ParentId,
            CategoryName = category.Name,
            CategorySlug = category.Slug,
            CategoryMetaTitle = category.MetaTitle,
            CategoryMetaKeywords = category.MetaKeywords,
            CategoryMetaDescription = category.MetaDescription,
            CurrentSearchOption = searchOption,
            FilterOption = new FilterOption()
        };
        var query = _productRepository.Query().Where(x => x.Categories.Any(x => x.CategoryId == category.Id) && x.IsPublished && !x.IsDeleted && x.IsVisibleIndividually);
        
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
        var categories = searchOption.GetCategories();
        if (categories.Any())
        {
            query = query.Where(p => p.Categories.Select(c => c.CategoryId).Intersect(_categoryRepository.Query().Where(cat => categories.Contains(cat.Slug)).Select(c => c.Id)).Any());
        }
        var brands = searchOption.GetBrands().ToArray();
        if (brands.Any())
        {
            query = query.Where(x => x.BrandId != null && brands.Contains(x.Brand.Slug));
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
                        .Select(x => ProductThumbnail.FromProduct(x)).ToList();

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
    private void AppendFilterOptionsToModel(ProductsByCategory model , IQueryable<Product> query)
    {
        model.FilterOption.Price.MaxPrice = query.Max(x => x.Price);
        model.FilterOption.Price.MinPrice = query.Min(x => x.Price);

        model.FilterOption.Categories = query.SelectMany(x=> x.Categories).GroupBy(x => new
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
        model.FilterOption.Brands = query.Include(x => x.Brand)
            .Where(x => x.BrandId != null).ToList()
            .GroupBy(x => x.Brand)
            .Select(g => new FilterBrand
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                Slug = g.Key.Slug,
                Count = g.Count()
            }).ToList();
    }
}

