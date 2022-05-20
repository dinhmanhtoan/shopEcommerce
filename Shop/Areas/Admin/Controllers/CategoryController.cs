using System.Net.Http.Headers;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class CategoryController : Controller
{
    private readonly ICategoryService _CategoryServices;
    private readonly IMediaService _mediaService;
    private readonly IRepository<Category> _categoryRepository;
    public CategoryController(ICategoryService CategoryServices, IMediaService mediaService,
        IRepository<Category> categoryRepository)
    {
        _CategoryServices = CategoryServices;
        _mediaService = mediaService;
        _categoryRepository = categoryRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var query = await _categoryRepository.Query().Where(x => !x.IsDeleted).Include(x => x.Thumbnail).ToListAsync();
        var cartegoryVms = new List<CategoryVm>();

        foreach (var item in query)
        {
            var category = new CategoryVm()
            {
                Id = item.Id,
                Name = item.Name,
                Slug = item.Slug,
                MetaTitle = item.MetaTitle,
                MetaKeywords = item.MetaKeywords,
                MetaDescription = item.MetaDescription,
                DisplayOrder = item.DisplayOrder,
                ParentId = item.ParentId,
                IncludeInMenu = item.IncludeInMenu,
                IsPublished = item.IsPublished,
                ThumbnailImageUrl = _mediaService.GetMediaUrl(item.Thumbnail)
            };
            cartegoryVms.Add(category);
        }
        return View(cartegoryVms);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categoriesList = await _CategoryServices.GetAll();
        var CategoryVm = new CategoryVm();
        CategoryVm.categoriesList = categoriesList;
        return View(CategoryVm);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CategoryVm model)
    {

        if (ModelState.IsValid)
        {
            var Category = new Category()
            {
                Name = model.Name,
                Slug = model.Slug,
                MetaTitle = model.MetaTitle,
                MetaKeywords = model.MetaKeywords,
                MetaDescription = model.MetaDescription,
                DisplayOrder = model.DisplayOrder,
                ParentId = model.ParentId,
                IncludeInMenu = model.IncludeInMenu,
                IsPublished = model.IsPublished
            };
            await SaveCategory(model, Category);
            _CategoryServices.AddCategory(Category);
            return Accepted();
        }
        return BadRequest(ModelState);
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> Updated(long Id)
    {
        var Category = _CategoryServices.getById(Id);
        if (Category == null)
        {
            return NotFound();
        }
        var Categories = _categoryRepository.Query().Where(x => !x.IsDeleted).ToList();
        var categoriesList = await _CategoryServices.GetAll();
        var CategoryVm = new CategoryVm()
        {
            Id = Category.Id,
            Name = Category.Name,
            Slug = Category.Slug,
            MetaTitle = Category.MetaTitle,
            MetaKeywords = Category.MetaKeywords,
            MetaDescription = Category.MetaDescription,
            DisplayOrder = Category.DisplayOrder,
            IsPublished = Category.IsPublished,
            IncludeInMenu = Category.IncludeInMenu,
            ParentId = Category.ParentId,
            ThumbnailImageUrl = _mediaService.GetMediaUrl(Category.Thumbnail)
        };
        CategoryVm.categoriesList = categoriesList;
        return View(CategoryVm);
    }
    [HttpPost("{Id}")]
    public async Task<IActionResult> Updated(long Id, CategoryVm model)
    {
        if (ModelState.IsValid)
        {
            var Category = _CategoryServices.getById(Id);
            if (Category == null)
            {
                return NotFound();
            }
            Category.Name = model.Name;
            Category.Slug = model.Slug;
            Category.MetaTitle = model.MetaTitle;
            Category.MetaKeywords = model.MetaKeywords;
            Category.MetaDescription = model.MetaDescription;
            Category.DisplayOrder = model.DisplayOrder;
            Category.IncludeInMenu = model.IncludeInMenu;
            Category.IsPublished = model.IsPublished;
            Category.ParentId = model.ParentId;
            if (Category.ParentId.HasValue && await HaveCircularNesting(Category.Id, Category.ParentId.Value))
            {
                ModelState.AddModelError("ParentId", "Parent category cannot be itself children");
                return BadRequest(ModelState);
            }
            await SaveCategory(model, Category);
                _CategoryServices.UpdateCategory(Category);
            return Accepted();
        }
        return BadRequest(ModelState);
    }
    [HttpPost]
    public IActionResult Delete(long Id)
    {
        var category = _categoryRepository.Query().FirstOrDefault(x => x.Id == Id);
        if (category != null)
        {
            _CategoryServices.Delete(Id);
            return Accepted();
        }
        return NotFound();
    }
    public async Task SaveCategory(CategoryVm model, Category category)
    {
        if (model.ThumbnailImage != null)
        {
            var fileName = await SaveFile(model.ThumbnailImage);
            if (category.Thumbnail != null)
            {
                RemoveFile(category.Thumbnail.FileName);
                category.Thumbnail.FileName = fileName;
            }
            else
            {
                category.Thumbnail = new Media { FileName = fileName };
            }
        
        }
    }
    private void RemoveFile(string file)
    {
        _mediaService.DeleteMediaAsync(file);
    }
    private async Task<string> SaveFile(IFormFile file)
    {
        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, file.ContentType);
        return fileName;
    }
    private async Task<bool> HaveCircularNesting(long childId, long parentId)
    {
        var category = await _categoryRepository.Query().FirstOrDefaultAsync(x => x.Id == parentId);
        var parentCategoryId = category.ParentId;
        while (parentCategoryId.HasValue)
        {
            if (parentCategoryId.Value == childId)
            {
                return true;
            }

            var parentCategory = await _categoryRepository.Query().FirstAsync(x => x.Id == parentCategoryId);
            parentCategoryId = parentCategory.ParentId;
        }

        return false;
    }
}

