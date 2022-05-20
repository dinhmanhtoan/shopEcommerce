namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class BrandController : Controller
{

    private readonly IBrandService _brandService;
    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var Brand = await _brandService.GetAll();
        return View(Brand);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(BrandForm model)
    {
     
        if (ModelState.IsValid)
        {
            var brand = new Brand
            {
                Name = model.Name,
                Slug = model.Slug,
                Description = model.Description,
                IsPublished = model.IsPublished
            };
            _brandService.AddBrand(brand);
            return Accepted();
        }
        return BadRequest(ModelState);

    }
    [HttpGet("{Id}")]
    public IActionResult Updated(long Id)
    {
        var Brand = _brandService.getById(Id);
        if (Brand != null)
        {
            return View(Brand);
        }
        return NotFound();

    }

    [HttpPost("{Id}")]
    public IActionResult Updated(long Id, BrandForm model)
    {
        var brand = _brandService.getById(Id);
        if (brand == null)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            brand.Name = model.Name;
            brand.Slug = model.Slug;
            brand.Description = model.Description;
            brand.IsPublished = model.IsPublished;
            _brandService.UpdateBrand(brand);
            return Accepted();
        }
        return BadRequest(ModelState);
    }
    [HttpPost]
    public IActionResult DeletedById(long Id)
    {
        _brandService.Delete(Id);
        return Accepted();
    }
}

