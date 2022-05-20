using System.Linq.Dynamic.Core;
namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class CustomerGroupController : Controller
{
    private readonly IRepository<CustomerGroup> _customerGroupRepository;
    public CustomerGroupController(IRepository<CustomerGroup> customerGroupRepository)
    {
        _customerGroupRepository = customerGroupRepository;
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
        var CustomerGroup = _customerGroupRepository.Query().Where(x => !x.IsDeleted).AsQueryable();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            CustomerGroup = CustomerGroup.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            CustomerGroup = CustomerGroup.Where(m => m.Name.Contains(searchValue));

        }
        recordsTotal = CustomerGroup.Count();
        var data = CustomerGroup.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);

    }
    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(CustomerGroup model)
    {
        if (ModelState.IsValid)
        {
            var CustomerGroup = new CustomerGroup()
            {
                Name = model.Name,
                Description = model.Description,
                IsActive = model.IsActive
            };
            _customerGroupRepository.Add(CustomerGroup);
            _customerGroupRepository.SaveChanges();
            return Accepted();
        }

        return BadRequest(ModelState);
    }

    [HttpGet("{id}")]
    public ActionResult Edit(int id)
    {
        var vendor = _customerGroupRepository.Query().FirstOrDefault(x => x.Id == id);
        return View(vendor);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, CustomerGroup model)
    {
        if (ModelState.IsValid)
        {
            var customerGroup = _customerGroupRepository.Query().FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (customerGroup == null)
            {
         
                return NotFound();
            }

            customerGroup.Name = model.Name;
            customerGroup.Description = model.Description;
            customerGroup.IsActive = model.IsActive;
            customerGroup.LatestUpdatedOn = DateTimeOffset.Now;

            _customerGroupRepository.SaveChanges();
            return Accepted();
        }
        return BadRequest(ModelState);
    }

    [HttpPost]
    public ActionResult Delete(int id)
    {
        var query = _customerGroupRepository.Query().FirstOrDefault(x => x.Id == id);
        if (query != null)
        {
            _customerGroupRepository.Remove(query);
            _customerGroupRepository.SaveChanges();
            return Accepted();
        }
        else
        {
            return NotFound();
        }
    }
}

