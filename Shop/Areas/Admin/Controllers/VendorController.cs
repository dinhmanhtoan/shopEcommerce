using System.Linq.Dynamic.Core;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class VendorController : Controller
{
    private readonly IRepository<Vender> _venderRepository;
    public VendorController(IRepository<Vender> venderRepository)
    {
        _venderRepository = venderRepository;
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
        var Vender = _venderRepository.Query().Where(x => !x.IsDeleted).AsQueryable();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            Vender = Vender.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            Vender = Vender.Where(m => m.Name.Contains(searchValue));

        }
        recordsTotal = Vender.Count();
        var data = Vender.Skip(skip).Take(pageSize).ToList();
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
    public ActionResult Create(Vender model)
    {
        if (ModelState.IsValid)
        {
            var Vender = new Vender()
            {
                Name = model.Name,
                Slug = model.Slug,
                Email = model.Email,
                Descriptions = model.Descriptions,
                IsActive = model.IsActive
            };
            _venderRepository.Add(Vender);
            _venderRepository.SaveChanges();
            return Accepted();
        }

        return BadRequest(ModelState);
    }

    [HttpGet("{id}")]
    public ActionResult Edit(int id)
    {
        var vendor = _venderRepository.Query().FirstOrDefault(x => x.Id == id);
        return View(vendor);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(long id,Vender model)
    {
        if (ModelState.IsValid)
        {
            var vender = _venderRepository.Query().FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (vender == null)
            {
                return NotFound();
            }
            vender.Name = model.Name;
            vender.Slug = model.Slug;
            vender.Email = model.Email;
            vender.Descriptions = model.Descriptions;
            vender.IsActive = model.IsActive;
            vender.LatestUpdatedOn = model.LatestUpdatedOn;

            _venderRepository.SaveChanges();
            return Accepted();
        }
        return BadRequest(ModelState);
    }
        
    [HttpPost]
    public ActionResult Delete(long id)
    {
        var query = _venderRepository.Query().FirstOrDefault(x => x.Id == id);
        if (query != null) {
            _venderRepository.Remove(query);
            _venderRepository.SaveChanges();
            return Accepted();
        }
        else
        {
            return NotFound();
        }
    }
}
