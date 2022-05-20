namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class TaxClassController : Controller
{
    private readonly IRepository<TaxClass> _taxClassRepository;
    private readonly IWorkContext _workContext;
    public TaxClassController(IRepository<TaxClass> taxClassRepository, IWorkContext workContext)
    {
        _taxClassRepository = taxClassRepository;
        _workContext = workContext;

    }
    [HttpGet]
    public IActionResult Index()
    {
        var taxClass = _taxClassRepository.Query().ToList();
        return View(taxClass);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(TaxClass model)
    {
        if (ModelState.IsValid)
        {
            var taxclass = new TaxClass()
            {
                Name = model.Name,
            };
            _taxClassRepository.Add(taxclass);
            _taxClassRepository.SaveChanges();
            return Accepted();
        }

        return BadRequest(ModelState);
    }
    [HttpGet("{Id}")]
    public IActionResult Edit(long Id)
    {
        var taxclass = _taxClassRepository.Query().FirstOrDefault(x => x.Id == Id);
        if (taxclass == null)
        {
            return NotFound();
        }
        return View(taxclass);
    }
    [HttpPost("{Id}")]
    public IActionResult Edit(long Id,TaxClass model)
    {
        var taxclass = _taxClassRepository.Query().FirstOrDefault(x => x.Id == Id);
        if (taxclass == null)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            taxclass.Name = model.Name;
            _taxClassRepository.SaveChanges();
            return Accepted();
        }
        return BadRequest(ModelState);
    }
    [HttpPost]
    public IActionResult Delete(long Id)
    {
        var taxclass = _taxClassRepository.Query().FirstOrDefault(x=> x.Id == Id);
        if (taxclass == null)
        {
            return NotFound();
        }
        _taxClassRepository.Remove(taxclass);
        _taxClassRepository.SaveChanges();
        return Accepted();
    }
}

