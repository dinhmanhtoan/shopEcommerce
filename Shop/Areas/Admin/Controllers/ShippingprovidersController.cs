namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class ShippingprovidersController : Controller
{
    private readonly IRepositoryWithTypedId<ShippingProvider , string> _shippingProviderRepository;
    private readonly IRepository<PriceAndDestination> _priceAndDestinationRepository;
    private readonly IRepositoryWithTypedId<Country, string> _countryRepository;
    private readonly IRepository<StateOrProvince> _stateOrProvinceRepository;
    private readonly IRepository<District> _districtRepository;

    private readonly IWorkContext _workContext;
    public ShippingprovidersController(IRepositoryWithTypedId<ShippingProvider, string> shippingProviderRepository,
        IRepository<PriceAndDestination> priceAndDestinationRepository,
        IRepositoryWithTypedId<Country,string> countryRepository,
        IRepository<StateOrProvince> stateOrProvinceRepository,
        IRepository<District> districtRepository,
        IWorkContext workContext
        )
    {
        _shippingProviderRepository = shippingProviderRepository;
        _priceAndDestinationRepository = priceAndDestinationRepository;
        _countryRepository = countryRepository;
        _stateOrProvinceRepository = stateOrProvinceRepository;
        _districtRepository = districtRepository;
        _workContext = workContext;

    }
    [HttpGet]
    public IActionResult Index()
    {
        var shippingProvider = _shippingProviderRepository.Query().ToList();
        return View(shippingProvider);
    }
    [HttpPost]
    public IActionResult Stop(string Id)
    {
        var shippingProvider = _shippingProviderRepository.Query().FirstOrDefault(x => x.Id == Id);
        if (shippingProvider == null)
        {
            return NotFound();
        }
        shippingProvider.IsEnabled = !shippingProvider.IsEnabled;
        _shippingProviderRepository.SaveChanges();
        return Ok();
    }
    [Route("shipping-table-rate-config")]
    public IActionResult table()
    {
        //var model = new TableRateConfigVm();
        var model = _priceAndDestinationRepository.Query().Select(x =>  new PriceAndDestinationForm
        {
            Id = x.Id,
            CountryId = x.CountryId,
            CountryName = x.Country.Name,
            StateOrProvinceId = x.StateOrProvinceId,
            StateOrProvinceName = x.StateOrProvince.Name,
            DistrictId = x.DistrictId,
            DistrictName = x.District.Name,
            ZipCode = x.ZipCode,
            MinOrderSubtotal = x.MinOrderSubtotal,
            ShippingPrice = x.ShippingPrice,
            Note = x.Note
        }).ToList();
        //PopulateAddressFormData(model);
        return View(model);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var returnModel = await _priceAndDestinationRepository.Query().Where(x => x.Id == id).Select(x => new PriceAndDestinationForm
        {
            Id = x.Id,
            CountryId = x.CountryId,
            CountryName = x.Country.Name,
            StateOrProvinceId = x.StateOrProvinceId,
            StateOrProvinceName = x.StateOrProvince.Name,
            DistrictId = x.DistrictId,
            DistrictName = x.District.Name,
            ZipCode = x.ZipCode,
            MinOrderSubtotal = x.MinOrderSubtotal,
            ShippingPrice = x.ShippingPrice,
            Note = x.Note
        }).FirstOrDefaultAsync();
        return Ok(returnModel);
    }
    [HttpPost]
    public  async Task<IActionResult> add(PriceAndDestinationForm model)
    {
        var PriceAndDestination = new PriceAndDestination();
        PriceAndDestination.StateOrProvinceId = model.StateOrProvinceId;
        PriceAndDestination.CountryId = model.CountryId;
        PriceAndDestination.DistrictId = model.DistrictId;
        PriceAndDestination.ZipCode = model.ZipCode;
        PriceAndDestination.MinOrderSubtotal = model.MinOrderSubtotal;
        PriceAndDestination.ShippingPrice = model.ShippingPrice;
        PriceAndDestination.Note = model.Note;
        _priceAndDestinationRepository.Add(PriceAndDestination);
        _priceAndDestinationRepository.SaveChanges();
        return await Get(PriceAndDestination.Id);
    }
    [HttpPost("{id}")]
    public async Task<IActionResult> edit(long id, PriceAndDestinationForm model)
    {
        var PriceAndDestination = _priceAndDestinationRepository.Query().FirstOrDefault(x => x.Id == id);
        PriceAndDestination.StateOrProvinceId = model.StateOrProvinceId;
        PriceAndDestination.CountryId = model.CountryId;
        PriceAndDestination.DistrictId = model.DistrictId;
        PriceAndDestination.ZipCode = model.ZipCode;
        PriceAndDestination.MinOrderSubtotal = model.MinOrderSubtotal;
        PriceAndDestination.ShippingPrice = model.ShippingPrice;
        PriceAndDestination.Note = model.Note;
        _priceAndDestinationRepository.SaveChanges();

        return await Get(PriceAndDestination.Id);

    }
    [HttpPost("{id}")]
    public IActionResult remove(long id)
    {
        var model = _priceAndDestinationRepository.Query().FirstOrDefault(x => x.Id == id);
        if (model == null)
        {
            return BadRequest();
        }
        _priceAndDestinationRepository.Remove(model);
        _priceAndDestinationRepository.SaveChanges();
        return Accepted();
    }
    private void PopulateAddressFormData(TableRateConfigVm model)
    {
        var shippableCountries = _countryRepository.Query()
            .Where(x => x.IsShippingEnabled)
            .OrderBy(x => x.Name);

        if (!shippableCountries.Any())
        {
            return;
        }

        model.Countries = shippableCountries
            .Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

        var selectedShipableCountryId = !string.IsNullOrEmpty(model.CountryId) ? model.CountryId : model.Countries.First().Value;
        //var selectedCountry = shippableCountries.FirstOrDefault(c => c.Id == selectedShipableCountryId);
        //if (selectedCountry != null)
        //{
        //    model.DisplayCity = selectedCountry.IsCityEnabled;
        //    model.DisplayDistrict = selectedCountry.IsDistrictEnabled;
        //    model.DisplayZipCode = selectedCountry.IsZipCodeEnabled;
        //}

        model.StateOrProvinces = _stateOrProvinceRepository.Query()
            .Where(x => x.CountryId == selectedShipableCountryId)
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

