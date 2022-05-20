using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class ProductTemplateController : Controller
{
    private readonly IRepository<ProductTemplate> _templateRepository;
    private readonly IRepository<ProductAttributeGroup> _attributeGroupRepository;
    private readonly IRepository<ProductTemplateProductAttribute> _templateProductAttributeRepository;
    public ProductTemplateController(IRepository<ProductTemplate> templateRepository, IRepository<ProductAttributeGroup> attributeGroupRepository, IRepository<ProductTemplateProductAttribute> templateProductAttributeRepository)
    {
        _templateRepository = templateRepository;
        _attributeGroupRepository = attributeGroupRepository;
        _templateProductAttributeRepository = templateProductAttributeRepository;
    }
    [HttpPost]
    public IActionResult Get(long id)
    {
        var productTemplate = _templateRepository
            .Query()
            .Include(x => x.ProductAttributes).ThenInclude(x => x.ProductAttribute).ThenInclude(x => x.Group)
            .FirstOrDefault(x => x.Id == id);
        var model = new ProductTemplateFrom
        {
            Id = productTemplate.Id,
            Name = productTemplate.Name,
            Attributes = productTemplate.ProductAttributes.Select(
                x => new ProductAttributeVm()
                {
                    Id = x.ProductAttributeId,
                    Name = x.ProductAttribute.Name,
                    GroupName = x.ProductAttribute.Group.Name
                }).ToList()
        };

        return Json(model);
    }
    [HttpGet]
    public IActionResult Index()
    {
        var template = _templateRepository.Query().ToList();
        return View(template);
    }
    [HttpGet]
    public IActionResult Create()
    {
        var attributeGroup = _attributeGroupRepository.Query().Include(x=> x.Attributes).ToList();
        var productTemplateFrom = new ProductTemplateFrom();

        foreach (var group in attributeGroup)
        {
            var optionGroup = new SelectListGroup() { Name = group.Name };
            foreach (var attr in group.Attributes)
            {
                productTemplateFrom.AttributeList.Add(new SelectListItem() { Value = attr.Id.ToString(), Text = attr.Name, Group = optionGroup });
            }
        }
        return View(productTemplateFrom);
    }
    [HttpPost]
    public IActionResult Create( ProductTemplateFrom model)
    {
        if (ModelState.IsValid)
        {
            var template = new ProductTemplate()
            {
                Name = model.Name
            };
            foreach (var attr in model.Attributes)
            {
                template.AddAttribute(attr.Id);
            }
            _templateRepository.Add(template);
            _templateRepository.SaveChanges();
            return Accepted();
        }
        return BadRequest(ModelState);
    }
    [HttpGet("{id}")]
    public IActionResult Edit(long id)
    {
        var attributeGroup = _attributeGroupRepository.Query().Include(x => x.Attributes).ToList();
        var productTemplateFrom = new ProductTemplateFrom();

        foreach (var group in attributeGroup)
        {
            var optionGroup = new SelectListGroup() { Name = group.Name };
            foreach (var attr in group.Attributes)
            {
                productTemplateFrom.AttributeList.Add(new SelectListItem() { Value = attr.Id.ToString(), Text = attr.Name, Group = optionGroup });
            }
        }
        var template = _templateRepository.Query().Include(x=> x.ProductAttributes).ThenInclude(x=> x.ProductAttribute).FirstOrDefault(x => x.Id == id);
        if (template == null)
        {
            return NotFound();
        }
        productTemplateFrom.Id = template.Id;
        productTemplateFrom.Name = template.Name;
        var productAttributeVm = new List<ProductAttributeVm>();
        foreach (var item in template.ProductAttributes)
        {
            var attributeVm = new ProductAttributeVm()
            {
                Id = item.ProductAttribute.Id,
                Name = item.ProductAttribute.Name
            };
            productAttributeVm.Add(attributeVm);
        }
        productTemplateFrom.Attributes = productAttributeVm;
        return View(productTemplateFrom);
    }
    [HttpPost("{id}")]
    public IActionResult Edit(long id, ProductTemplateFrom model)
    {

        if (ModelState.IsValid)
        {
            var template = _templateRepository
                             .Query()
                             .Include(x => x.ProductAttributes)
                             .FirstOrDefault(x => x.Id == id);

            template.Name = model.Name;

            foreach (var attribute in model.Attributes)
            {
                if (template.ProductAttributes.Any(x => x.ProductAttributeId == attribute.Id))
                {
                    continue;
                }

                template.AddAttribute(attribute.Id);
            }

            var deletedAttributes = template.ProductAttributes.Where(attr => !model.Attributes.Select(x => x.Id).Contains(attr.ProductAttributeId));

            foreach (var deletedAttribute in deletedAttributes)
            {
                deletedAttribute.ProductTemplate = null;
                _templateProductAttributeRepository.Remove(deletedAttribute);
            }
            _templateRepository.SaveChanges();
            return Accepted();
        }

        return BadRequest(ModelState);
    }
    [HttpPost]
    public IActionResult Delete(long id)
    {
        var template = _templateRepository.Query().FirstOrDefault(x => x.Id == id);
        if (template == null)
        {
            return NotFound();
        }
        else
        {
            _templateRepository.Remove(template);
            _templateRepository.SaveChanges();
            return Accepted();
        }
    }
}

