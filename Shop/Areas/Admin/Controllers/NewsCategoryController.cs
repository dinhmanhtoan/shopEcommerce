using System.Linq.Dynamic.Core;
namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles="admin")]
[Route("[controller]/[action]")]
public class NewsCategoryController : Controller
{
    private readonly IRepository<NewsCategory> _newsCategoryRepository;
    private readonly INewsCategoryService _categoryService;

    public NewsCategoryController(IRepository<NewsCategory> newsCategoryRepository, INewsCategoryService categoryService)
    {
        _newsCategoryRepository = newsCategoryRepository;
        _categoryService = categoryService;
    }
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public ActionResult query()
    {

        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;
        var NewsCategory = _newsCategoryRepository.Query().Where(x => !x.IsDeleted).AsQueryable();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            NewsCategory = NewsCategory.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            NewsCategory = NewsCategory.Where(m => m.Name.Contains(searchValue));

        }
        recordsTotal = NewsCategory.Count();
        var data = NewsCategory.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);

    }
    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(NewsCategory model)
    {
        if (ModelState.IsValid)
        {
            var category = new NewsCategory()
            {

                Name = model.Name,
                Slug = model.Slug,
                MetaTitle = model.MetaTitle,
                MetaKeywords = model.MetaKeywords,
                MetaDescription = model.MetaDescription,
                Description = model.Description,
                DisplayOrder = model.DisplayOrder,
                IsPublished = true
            };
            await _categoryService.Create(category);
            return Accepted();
        }

        return BadRequest(ModelState);
    }

    [HttpGet("{id}")]
    public ActionResult Edit(long id)
    {
        var newsCategory = _newsCategoryRepository.Query().FirstOrDefault(x => x.Id == id);
        return View(newsCategory);
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> Edit(long id, NewsCategory model)
    {
        if (ModelState.IsValid)
        {
            var category = _newsCategoryRepository.Query().FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (category == null)
            {
                return NotFound();
            }
            category.Name = model.Name;
            category.Slug = model.Slug;
            category.MetaTitle = model.MetaTitle;
            category.MetaKeywords = model.MetaKeywords;
            category.MetaDescription = model.MetaDescription;
            category.IsPublished = model.IsPublished;

            await _categoryService.Update(category);
            return Accepted();
        }
        return BadRequest(ModelState);
    }

    [HttpPost]
    public async Task<ActionResult> Delete(long id)
    {
        var category = _newsCategoryRepository.Query().FirstOrDefault(x => x.Id == id);
        if (category != null)
        {
            await _categoryService.Delete(category);
            return Accepted();
        }
        else
        {
            return NotFound();
        }
    }
}

