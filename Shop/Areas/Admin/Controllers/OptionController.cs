namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class OptionController : Controller
{
    private readonly IRepository<ProductOption> _optionRepository;
    public OptionController(IRepository<ProductOption> optionRepository)
    {
        _optionRepository = optionRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var option = _optionRepository.Query().ToList();
        return View(option);
    }
    [HttpGet]
    public IActionResult Create()
    {
        var model = new ProductOptionForm();
        return View(model);
    }
    [HttpPost]
    public IActionResult Create(ProductOptionForm model)
    {
        if (ModelState.IsValid)
        {
            var productOption = new ProductOption();
            productOption.Name = model.Name;
            _optionRepository.Add(productOption);
            _optionRepository.SaveChanges();
            return Accepted();
        }
        return BadRequest(ModelState);
    }
    [HttpGet("{Id}")]
    public IActionResult Updated(long Id)
    {
        var option = _optionRepository.Query().FirstOrDefault(x => x.Id == Id);
        if (option == null) return NotFound();
        var productOptionForm = new ProductOptionForm();
        productOptionForm.Name = option.Name;
        return View(productOptionForm);
    }
    [HttpPost("{Id}")]
    public IActionResult Updated(long Id, ProductOptionForm model)
    {
        if (ModelState.IsValid)
        {
            var option = _optionRepository.Query().FirstOrDefault(x => x.Id == Id);
            option.Name = model.Name;
            _optionRepository.SaveChanges();
            return Accepted();
        }
        return BadRequest(ModelState);
    }
    [HttpPost]
    public IActionResult DeletedById(long Id)
    {
        var option = _optionRepository.Query().FirstOrDefault(x => x.Id == Id);
        if (option == null) return BadRequest();
        _optionRepository.Remove(option);
        _optionRepository.SaveChanges();
        return Accepted();
    }
}

