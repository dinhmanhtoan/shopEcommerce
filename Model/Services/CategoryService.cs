namespace Model.Services;

public interface ICategoryService
{
    public Task<IList<CategoryListItem>> GetAll();
    public void AddCategory(Category entity);
    public void UpdateCategory(Category entity);
    public void Delete(long Id);
    public Category getById(long Id);

}
public class CategoryService : ICategoryService
{
    private const string CategoryEntityTypeId = "Category";
    private readonly IEntityService _entityService;

    private readonly IRepository<Category> _categoryRepository;

    public CategoryService(IRepository<Category> categoryRepository, IEntityService entityService)
    {
        _categoryRepository = categoryRepository;
        _entityService = entityService;
    }
    public async Task<IList<CategoryListItem>> GetAll()
    {
        var categories = await _categoryRepository.Query().Where(x => !x.IsDeleted).ToListAsync();
        var categoriesList = new List<CategoryListItem>();
        foreach (var category in categories)
        {
            var categoryListItem = new CategoryListItem
            {
                Id = category.Id,
                IsPublished = category.IsPublished,
                IncludeInMenu = category.IncludeInMenu,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder,
                ParentId = category.ParentId
            };

            var parentCategory = category.Parent;
            while (parentCategory != null)
            {
                categoryListItem.Name = $"{parentCategory.Name} >> {categoryListItem.Name}";
                parentCategory = parentCategory.Parent;
            }

            categoriesList.Add(categoryListItem);
        }

        return categoriesList.OrderBy(x => x.Name).ToList();
    }
    public  void AddCategory(Category category)
    {
        category.Slug = _entityService.ToSafeSlug(category.Slug, category.Id, CategoryEntityTypeId);
        _categoryRepository.Add(category);
        _categoryRepository.SaveChanges();
        _entityService.Add(category.Name, category.Slug, category.Id, CategoryEntityTypeId);
        _categoryRepository.SaveChanges();
    }

    public void UpdateCategory(Category category)
    {
        category.Slug = _entityService.ToSafeSlug(category.Slug, category.Id, CategoryEntityTypeId);
        _entityService.Update(category.Name, category.Slug, category.Id, CategoryEntityTypeId);
        _categoryRepository.SaveChanges();
    }

    public void Delete(long Id)
    {
        var Category = _categoryRepository.Query().FirstOrDefault(x => x.Id == Id);
        if (Category != null)
        {
            Category.IsDeleted = true;
            _entityService.Remove(Id, CategoryEntityTypeId);
            _categoryRepository.SaveChanges();
        }
    }
    public  Category getById(long Id)
    {
        var res = _categoryRepository.Query().Include(x=> x.Thumbnail).Where(x=> !x.IsDeleted).FirstOrDefault(x => x.Id == Id);
        return res;
    }
}

