namespace Shop.Components;

public class CategoryFutureViewComponent : ViewComponent
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<ProductCategory> _productCategoryRepository;
    public CategoryFutureViewComponent(IRepository<Category> categoryRepository, IRepository<ProductCategory> productCategoryRepository)
    {
        _categoryRepository = categoryRepository;
        _productCategoryRepository = productCategoryRepository;
    }

    public IViewComponentResult Invoke()
    {
        var model = _categoryRepository.Query().Include(x => x.Thumbnail).Where(x=> !x.IsDeleted && x.ParentId == null).ToList().Take(3);
        var CategoryVms = new List<CategoryVm>();
        foreach (var item in model)
        {
            var count = _productCategoryRepository.Query().Include(x=> x.Product).Include(x=> x.Category).Where(x => x.CategoryId == item.Id)
                .Select(x=> x.Category).Count();
            var CategoryVm = new CategoryVm()
            {
                ThumbnailImageUrl = item.Thumbnail.FileName,
                Id = item.Id,
                Name = item.Name,
                Slug = item.Slug,
                MetaDescription = item.MetaDescription,
                MetaTitle = item.MetaTitle,
                MetaKeywords = item.MetaKeywords,
                CountProduct = count,
            };
            CategoryVms.Add(CategoryVm);
        }
        
        return View(CategoryVms);
    }
}
