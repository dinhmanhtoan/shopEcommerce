using Microsoft.AspNetCore.Mvc.Rendering;
namespace Shop.Controllers;

[Authorize]
[Route("checkout")]
public class CheckoutController : Controller
{
    public readonly IRepository<UserAddress> _userAddressRepository;
    public readonly IRepositoryWithTypedId<PaymentProvider, string> _paymentProviderRepository;
    public readonly IRepositoryWithTypedId<Country , string> _countryRepository;
    public readonly IRepository<StateOrProvince> _stateOrProvinceRepository;
    public readonly IRepository<District> _districtRepository;
    public readonly IRepository<Cart> _cartRepository;
    public readonly IWorkContext _workContext;
    public readonly ICartService _cartService;
    public readonly IOrderService _orderService;
    public CheckoutController(IWorkContext workContext, ICartService cartService, IOrderService orderService,
            IRepository<UserAddress> userAddressRepository,
            IRepositoryWithTypedId<PaymentProvider, string> paymentProviderRepository,
            IRepositoryWithTypedId<Country, string> countryRepository,
            IRepository<StateOrProvince> stateOrProvinceRepository,
            IRepository<District> districtRepository,
            IRepository<Cart> cartRepository
        )
    {
        _userAddressRepository = userAddressRepository;
        _paymentProviderRepository = paymentProviderRepository;
        _countryRepository = countryRepository;
        _stateOrProvinceRepository = stateOrProvinceRepository;
        _districtRepository = districtRepository;
        _cartRepository = cartRepository;
        _workContext = workContext;
        _cartService = cartService;
        _orderService = orderService;
    }
    [HttpGet("shipping")]
    public async Task<IActionResult> Shipping()
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCartDetails(currentUser.Id);
        if (cart == null || !cart.Items.Any())
        {
            return Redirect("~/");
        }
        var model = new DeliveryInformationVm();

        PopulateShippingForm(model, currentUser);
        decimal demo = cart.OrderTotal;
        Console.WriteLine(demo.ToString("C"));
        return View(model);
    }
    [HttpPost("shipping")]
    public async Task<IActionResult> Shipping(DeliveryInformationVm model)
    {
        var currentUser = await _workContext.GetCurrentUser();
        if ((!model.NewAddressForm.IsValid() && model.ShippingAddressId == 0) ||
            (!model.NewBillingAddressForm.IsValid() && !model.UseShippingAddressAsBillingAddress && model.BillingAddressId == 0))
        {
            PopulateShippingForm(model,currentUser);
            return View(model);
        }
        var cart = await _cartService.GetActiveCart(currentUser.Id);

        if (cart == null)
        {
            throw new ApplicationException($"Cart of user {currentUser.Id} cannot be found");
        }

        cart.ShippingData = JsonConvert.SerializeObject(model);
        cart.OrderNote = model.OrderNote;
        await _cartRepository.SaveChangesAsync();
        return RedirectToAction(nameof(Payment));
    }

    [HttpGet("payment")]
    public async Task<IActionResult> Payment()
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCart(currentUser.Id);
        if (cart == null)
        {
            return Redirect("~/");
        }

        cart.LockedOnCheckout = true;
        await _cartRepository.SaveChangesAsync();

        var checkoutPaymentForm = new CheckoutPaymentForm();
        checkoutPaymentForm.PaymentProviders = await _paymentProviderRepository.Query()
            .Where(x => x.IsEnabled)
            .Select(x => new PaymentProviderVm
            {
                Id = x.Id,
                Name = x.Name,
                LandingViewComponentName = x.LandingViewComponentName
            }).ToListAsync();

        return View(checkoutPaymentForm);
    }
    [HttpPost("update-tax-and-shipping-prices")]
    public async Task<IActionResult> UpdateTaxAndShippingPrices([FromBody] TaxAndShippingPriceRequestVm model)
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCart(currentUser.Id);
        var orderTaxAndShippingPrice = await _orderService.UpdateTaxAndShippingPrices(cart.Id, model);

        return Ok(orderTaxAndShippingPrice);
    }

    [HttpGet("success")]
    public IActionResult Success(long orderId)
    {
        return View(orderId);
    }

    [HttpGet("error")]
    public IActionResult Error(long orderId)
    {
        return View(orderId);
    }

    [HttpPost("cancel")]
    public async Task<IActionResult> Cancel()
    {
        var currentUser = await _workContext.GetCurrentUser();
        var cart = await _cartService.GetActiveCart(currentUser.Id);
        if (cart != null && cart.LockedOnCheckout)
        {
            cart.LockedOnCheckout = false;
            await _cartRepository.SaveChangesAsync();
        }

        return Redirect("~/");
    }
    private void PopulateShippingForm(DeliveryInformationVm model,User currentUser)
    {
        model.ExistingShippingAddresses = _userAddressRepository.Query()
            .Where(x => (x.AddressType == AddressType.Shipping) && (x.UserId == currentUser.Id))
            .Select(x=> new ShippingAddressVm{
                UserAddressId =x.Id,
                ContactName = x.Address.ContactName,
                Phone = x.Address.Phone,
                AddressLine1 = x.Address.AddressLine1,
                CityName = x.Address.ContactName,
                ZipCode = x.Address.ZipCode,
                DistrictName = x.Address.District.Name,
                StateOrProvinceName = x.Address.StateOrProvince.Name,
                IsCityEnabled = x.Address.Country.IsCityEnabled,
                IsDistrictEnabled = x.Address.Country.IsDistrictEnabled,
                IsZipCodeEnabled = x.Address.Country.IsZipCodeEnabled
            }).ToList();
        model.ShippingAddressId = currentUser.DefaultShippingAddressId ?? 0;

        model.UseShippingAddressAsBillingAddress = true;

        model.NewAddressForm.ShipableContries = _countryRepository.Query()
            .Where(x=> x.IsShippingEnabled)
            .OrderBy(x=> x.Name)
            .Select(x=> new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        if (model.NewAddressForm.ShipableContries.Count == 1)
        {
            var onlyShipableContryId = model.NewAddressForm.ShipableContries.First().Value;
            model.NewAddressForm.StateOrProvinces = _stateOrProvinceRepository.Query()
                .Where(x => x.CountryId == onlyShipableContryId)
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
        }
    }
}

