namespace Shop.Components;

public class CategoryMenuViewComponent : ViewComponent
{
    public IRepository<Category> _categoryRepository;
    public CategoryMenuViewComponent(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }



    public IViewComponentResult Invoke()
    {
        var categories = _categoryRepository.Query().Where(x => !x.IsDeleted && x.IncludeInMenu).ToList();

        var categoryMenuItems = new List<CategoryMenuItem>();
        var topCategories = categories.Where(x => !x.ParentId.HasValue).OrderByDescending(x => x.DisplayOrder);
        foreach (var category in topCategories)
        {
            var categoryMenuItem = Map(category);
            categoryMenuItems.Add(categoryMenuItem);
        }

        return View(categoryMenuItems);
    }
    private CategoryMenuItem Map(Category category)
    {
        var categoryMenuItem = new CategoryMenuItem
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug
        };
        var childCategories = category.Children;
        foreach (var childCategory in childCategories.OrderByDescending(x => x.DisplayOrder))
        {
            var childCategoryMenuItem = Map(childCategory);
            categoryMenuItem.AddChildItem(childCategoryMenuItem);
        }

        return categoryMenuItem;
    }
}

