using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class ProductAttributesController : Controller
{
    private readonly IRepository<ProductAttribute> _attributeRepository;
    private readonly IRepository<ProductAttributeGroup> _groupRepository;
    public ProductAttributesController(IRepository<ProductAttribute> attributeRepository, IRepository<ProductAttributeGroup> groupRepository)
    {
        _attributeRepository = attributeRepository;
        _groupRepository = groupRepository;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var attribute = _attributeRepository.Query().Include(x=> x.Group).ToList();
        return View(attribute);
    }
    [HttpGet]
    public IActionResult Create()
    {
        var Group = _groupRepository.Query().ToList();
        var productAttributeFormVm = new ProductAttributeFormVm();
        productAttributeFormVm.Groups = Group.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name

        }).ToList();
        return View(productAttributeFormVm);
    }
    [HttpPost]
    public IActionResult Create(ProductAttributeFormVm model)
    {
        if (ModelState.IsValid)
        {
            var attribute = new ProductAttribute()
            {
                Name = model.Name,
                GroupId = model.GroupId
            };
            _attributeRepository.Add(attribute);
            _attributeRepository.SaveChanges();
            return Accepted();
        }
        return BadRequest(ModelState);
    }
    [HttpGet("{id}")]
    public IActionResult Edit(long id)
    {
        var attribute = _attributeRepository.Query().FirstOrDefault(x => x.Id == id);
        var Group = _groupRepository.Query().ToList();
        var productAttributeFormVm = new ProductAttributeFormVm();
        productAttributeFormVm.GroupId = attribute.GroupId;
        productAttributeFormVm.Name = attribute.Name;
        productAttributeFormVm.Groups = Group.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name,
            Selected = attribute.GroupId == x.Id  ? true : false

        }).ToList();

        if (attribute == null)
        {
            return NotFound();
        }

        return View(productAttributeFormVm);
    }
    [HttpPost("{id}")]
    public IActionResult Edit(long id, ProductAttributeFormVm model)
    {

        if (ModelState.IsValid)
        {
            var attribute = _attributeRepository.Query().FirstOrDefault(x => x.Id == id);
            if (attribute == null)
            {
                return NotFound();
            }
            attribute.Name = model.Name;
            attribute.GroupId = model.GroupId;
            _attributeRepository.SaveChanges();
            return Accepted();
        }

        return BadRequest(ModelState);
    }
    [HttpPost]
    public IActionResult Delete(long id)
    {
        var attribute = _attributeRepository.Query().FirstOrDefault(x => x.Id == id);
        if (attribute == null)
        {
            return NotFound();
        }
        else
        {
            _attributeRepository.Remove(attribute);
            _attributeRepository.SaveChanges();
            return Accepted();
        }
    }
}

