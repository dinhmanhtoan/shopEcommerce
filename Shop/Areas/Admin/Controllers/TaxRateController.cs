using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class TaxRateController : Controller
{
    private readonly IRepository<TaxRate> _taxRateRepository;
    private readonly IRepository<TaxClass> _taxClassRepository;
    private readonly IRepositoryWithTypedId<Country, string> _countryRepository;
    private readonly IRepository<StateOrProvince> _stateOrProvinceRepository;
    private readonly IWorkContext _workContext;
    public TaxRateController(IRepository<TaxRate> taxRateRepository, IRepository<TaxClass> taxClassRepository,
                IRepositoryWithTypedId<Country, string> countryRepository,
                IRepository<StateOrProvince> stateOrProvinceRepository, IWorkContext workContext)
    {
        _taxRateRepository = taxRateRepository;
        _taxClassRepository = taxClassRepository;
        _countryRepository = countryRepository;
        _stateOrProvinceRepository = stateOrProvinceRepository;
        _workContext = workContext;

    }
    [HttpGet]
    public IActionResult Index()
    {
        var taxRates = _taxRateRepository
          .Query()
          .Select(x => new TaxRateVm
          {
              TaxRateId = x.Id,
              TaxClassName = x.TaxClass.Name,
              CountryName = x.Country.Name,
              StateOrProvinceName = x.StateOrProvince.Name,
              ZipCode = x.ZipCode,
              Rate = x.Rate
          }).ToList();

        return View(taxRates);
    }
    [HttpGet]
    public IActionResult Create()
    {
        var model = new TaxRateVm();
        PopulateTaxRate(model);
        return View(model);
    }
    [HttpPost]
    public IActionResult Create(TaxRateVm model)
    {
        if (ModelState.IsValid)
        {
            var TaxRate = new TaxRate()
            {
                TaxClassId = model.TaxClassId,
                CountryId = model.CountryId,
                StateOrProvinceId = model.StateOrProvinceId,
                ZipCode = model.ZipCode,
                Rate = model.Rate,
            };
            _taxRateRepository.Add(TaxRate);
            _taxRateRepository.SaveChanges();
            return Accepted();
        }
        //PopulateTaxRate(model);
        return BadRequest(ModelState);

    }
    [HttpGet("{Id}")]
    public IActionResult Edit(long Id)
    {
        var query = _taxRateRepository.Query().FirstOrDefault(x => x.Id == Id);
        var model = new TaxRateVm()
        {
            TaxClassId = query.TaxClassId,
            CountryId = query.CountryId,
            StateOrProvinceId = query.StateOrProvinceId,
            ZipCode = query.ZipCode,
            Rate = query.Rate,
        };
        PopulateTaxRate(model);
        return View(model);
    }
    [HttpPost("{Id}")]
    public IActionResult Edit(long Id, TaxRateVm model)
    {
        if (ModelState.IsValid)
        {
            var TaxRate = _taxRateRepository.Query().FirstOrDefault(x => x.Id == Id);
            if (TaxRate == null)
            {
                return NotFound();
            }

            TaxRate.TaxClassId = model.TaxClassId;
            TaxRate.CountryId = model.CountryId;
            TaxRate.StateOrProvinceId = model.StateOrProvinceId;
            TaxRate.ZipCode = model.ZipCode;
            TaxRate.Rate = model.Rate;

            _taxRateRepository.SaveChanges();
            return Accepted();
        }
     /*   PopulateTaxRate(model)*/;
        return BadRequest(ModelState);

    }
    [HttpPost]
    public IActionResult Delete(long Id)
    {

        var taxRate = _taxRateRepository.Query().FirstOrDefault(x => x.Id == Id);
        if (taxRate == null)
        {
            return NotFound();
        }
        _taxRateRepository.Remove(taxRate);
        _taxRateRepository.SaveChanges();
        return Accepted();
    }
    private void PopulateTaxRate(TaxRateVm model)
    {
        var Countries = _countryRepository.Query()
            .OrderBy(x => x.Name);
        var TaxClass = _taxClassRepository.Query().OrderBy(x => x.Name);
        model.TaxClass = TaxClass.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()

        }).ToList();
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
    }

    [HttpPost("{Id}")]
    public IActionResult demo(long Id)
    {
        var model = _taxRateRepository.Query().FirstOrDefault(x => x.Id == Id);
        return Json(model);
    }
}

