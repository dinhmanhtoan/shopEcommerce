using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Dynamic.Core;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class WarehouseController : Controller
{
    private readonly IRepositoryWithTypedId<Country, string> _countryRepository;
    private readonly IRepository<StateOrProvince> _stateOrProvinceRepository;
    private readonly IRepository<District> _districtRepository;
    private readonly IRepository<Warehouse> _warehouseRepository;
    private readonly IRepository<Vender> _venderRepository;
    private readonly IRepository<Address> _addressRepository;
    private readonly IWorkContext _workContext;
    public WarehouseController(IWorkContext workContext,
        IRepositoryWithTypedId<Country, string> countryRepository,
        IRepository<StateOrProvince> stateOrProvinceRepositor,
        IRepository<District> districtRepository,
        IRepository<Warehouse> warehouseRepository,
        IRepository<Vender> venderRepository,
        IRepository<Address> addressRepository


        )
    {
        _workContext = workContext;
        _countryRepository = countryRepository;
        _stateOrProvinceRepository = stateOrProvinceRepositor;
        _districtRepository = districtRepository;
        _warehouseRepository = warehouseRepository;
        _venderRepository = venderRepository;
        _addressRepository = addressRepository;

    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var query = _warehouseRepository.Query();
        var currentUser = await _workContext.GetCurrentUser();
        if (!User.IsInRole("admin"))
        {
            query = query.Where(x => x.VendorId == currentUser.VendorId);
        }
        var warehouses = await query.ToListAsync();

        return View(warehouses);
    }
    [HttpPost]
    public async Task<IActionResult> List()
    {

        var currentUser = await _workContext.GetCurrentUser();
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;
        var Warehouses = _warehouseRepository.Query().AsQueryable();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            Warehouses = Warehouses.OrderBy(sortColumn + " " + sortColumnDirection);
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            Warehouses = Warehouses.Where(m => m.Name.Contains(searchValue));
        }
        recordsTotal = Warehouses.Count();
        var data = Warehouses.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }
    [HttpGet]
    public IActionResult Create()
    {
        var warehouse = new WarehouseVm();
        PopulateWarehouse(warehouse);
        return View(warehouse);
    }
    [HttpPost]
    public async Task<IActionResult> Create(WarehouseVm model)
    {
        if (ModelState.IsValid)
        {
            var address = new Address
            {
                ContactName = model.ContactName,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                Phone = model.Phone,
                StateOrProvinceId = model.StateOrProvinceId,
                CountryId = model.CountryId,
                City = model.City,
                DistrictId = model.DistrictId,
                ZipCode = model.ZipCode
            };
            var warehouse = new Warehouse
            {
                Name = model.Name,
                Address = address
            };
            var currentUser = await _workContext.GetCurrentUser();
            if (!User.IsInRole("admin"))
            {
                warehouse.VendorId = currentUser.VendorId;
            }
            _warehouseRepository.Add(warehouse);
            await _warehouseRepository.SaveChangesAsync();
            return Accepted();
        }
       //PopulateWarehouse(model);
        return BadRequest(ModelState);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Edit(long id)
    {
        var warehouse = await _warehouseRepository.Query().Include(w => w.Address).FirstOrDefaultAsync(w => w.Id == id);
        if (warehouse == null)
        {
            return NotFound();
        }
        var currentUser = await _workContext.GetCurrentUser();
        if (!User.IsInRole("admin") && warehouse.VendorId != currentUser.VendorId)
        {
            return BadRequest(new { error = "You don't have permission to manage this warehouse" });
        }
        var address = warehouse.Address ?? new Address();
        var model = new WarehouseVm
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            AddressId = address.Id,
            ContactName = address.ContactName,
            AddressLine1 = address.AddressLine1,
            AddressLine2 = address.AddressLine2,
            Phone = address.Phone,
            StateOrProvinceId = address.StateOrProvinceId,
            CountryId = address.CountryId,
            City = address.City,
            DistrictId = address.DistrictId,
            ZipCode = address.ZipCode
        };
        PopulateWarehouse(model);
        return View(model);
    }
    [HttpPost("{id}")]
    public async Task<IActionResult> Edit(long id,WarehouseVm model)
    {
        if (!ModelState.IsValid)
        {
            //PopulateWarehouse(model);
            return BadRequest(ModelState);
        }

        var warehouse = await _warehouseRepository.Query().Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
        if (warehouse == null)
        {
            return NotFound();
        }

        var currentUser = await _workContext.GetCurrentUser();
        if (!User.IsInRole("admin") && warehouse.VendorId != currentUser.VendorId)
        {
            return BadRequest(new { error = "You don't have permission to manage this warehouse" });
        }

        warehouse.Name = model.Name;
        if (warehouse.Address == null)
        {
            warehouse.Address = new Address();
            _addressRepository.Add(warehouse.Address);
        }

        warehouse.Address.ContactName = model.ContactName;
        warehouse.Address.Phone = model.Phone;
        warehouse.Address.ZipCode = model.ZipCode;
        warehouse.Address.StateOrProvinceId = model.StateOrProvinceId;
        warehouse.Address.CountryId = model.CountryId;
        warehouse.Address.DistrictId = model.DistrictId;

        await _warehouseRepository.SaveChangesAsync();
        return Accepted();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        var warehouse = await _warehouseRepository.Query().Include(w => w.Address).FirstOrDefaultAsync(x => x.Id == id);
        if (warehouse == null)
        {
            return NotFound();
        }

        var currentUser = await _workContext.GetCurrentUser();
        if (!User.IsInRole("admin") && warehouse.VendorId != currentUser.VendorId)
        {
            return BadRequest(new { error = "You don't have permission to manage this warehouse" });
        }

        try
        {
            _warehouseRepository.Remove(warehouse);
            _addressRepository.Remove(warehouse.Address);

            await _warehouseRepository.SaveChangesAsync();
            return Accepted();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { Error = $"The warehouse {warehouse.Name} can't not be deleted because it is referenced by other tables" });
        }

    }
    private void PopulateWarehouse(WarehouseVm model)
    {
        var Countries = _countryRepository.Query()
            .OrderBy(x => x.Name);

        model.Contries = Countries
            .Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

        var selectedCountryId = !string.IsNullOrEmpty(model.CountryId) ? model.CountryId : model.Contries.First().Value;
        var selectedCountry = Countries.FirstOrDefault(c => c.Id == selectedCountryId);


        model.StateOrProvinces = _stateOrProvinceRepository.Query()
            .Where(x => x.CountryId == selectedCountryId)
            .OrderBy(x => x.Name)
            .Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

        if (model.StateOrProvinceId > 0)
        {
            model.Districts = _districtRepository.Query()
                .Where(x => x.StateOrProvinceId == model.StateOrProvinceId)
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
        }
    }
}

