namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class ProductAttributeGroupsController : Controller
{
    private IRepository<ProductAttributeGroup> _attributeGroupRepository;
    public ProductAttributeGroupsController(IRepository<ProductAttributeGroup> attributeGroupRepository)
    {
        _attributeGroupRepository = attributeGroupRepository;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var attributeGroup = _attributeGroupRepository.Query().ToList();
        return View(attributeGroup);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(ProductAttributeGroup model)
    {
        if (ModelState.IsValid)
        {
            var productAttributeGroup = new ProductAttributeGroup()
            {
                Name = model.Name,
            };
            _attributeGroupRepository.Add(productAttributeGroup);
            _attributeGroupRepository.SaveChanges();
          return Accepted();
        }
        return BadRequest(ModelState);
    }
    [HttpGet("{id}")]
    public IActionResult Edit(long id)
    {
        var attributeGroupRepository = _attributeGroupRepository.Query().FirstOrDefault(x => x.Id == id);
        if (attributeGroupRepository == null)
        {
            return NotFound();
        }

        return View(attributeGroupRepository);
    }
    [HttpPost("{id}")]
    public IActionResult Edit(long id, ProductAttributeGroup model)
    {
        
        if (ModelState.IsValid)
        {
            var productAttributeGroup = _attributeGroupRepository.Query().FirstOrDefault(x => x.Id == id);
            if (productAttributeGroup == null)
            {
                return NotFound();
            }
            productAttributeGroup.Name = model.Name;
            _attributeGroupRepository.SaveChanges();
            return Accepted();
        }

        return BadRequest(ModelState);
    }
    [HttpPost]
    public IActionResult Delete(long id)
    {
        var attributeGroupRepository = _attributeGroupRepository.Query().FirstOrDefault(x => x.Id == id);
        if (attributeGroupRepository == null)
        {
            return NotFound();
        }
        else
        {
            _attributeGroupRepository.Remove(attributeGroupRepository);
            _attributeGroupRepository.SaveChanges();
            return Accepted();
        }
    }

}

