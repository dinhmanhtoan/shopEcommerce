using PaymentProviderHelperBraintree = Model.ViewModel.Braintree.PaymentProviderHelper;
using PaymentProviderHelperCashfree = Model.ViewModel.CashFree.PaymentProviderHelper;
using PaymentProviderHelperMomo = Model.ViewModel.Momo.PaymentProviderHelper;
using PaymentProviderHelperNganLuong = Model.ViewModel.NganLuong.PaymentProviderHelper;
using PaymentProviderHelperCoD = Model.ViewModel.CoD.PaymentProviderHelper;
using PaymentProviderHelperPaypalExpress = Model.ViewModel.PaypalExpress.PaymentProviderHelper;
using PaymentProviderHelperStripe = Model.ViewModel.Stripe.PaymentProviderHelper;



namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class PaymentProvidersController : Controller
{
    private readonly IRepositoryWithTypedId<PaymentProvider, string> _paymentProviderRepository;
    private readonly IWorkContext _workContext;
    public PaymentProvidersController(IRepositoryWithTypedId<PaymentProvider, string> paymentProviderRepository, IWorkContext workContext)
    {
        _paymentProviderRepository = paymentProviderRepository;
        _workContext = workContext;

    }
    [HttpGet]
    public IActionResult Index()
    {
        var paymentProvider = _paymentProviderRepository.Query().ToList();
        return View(paymentProvider);
    }
    [HttpPost("{id}")]
    public IActionResult Stop(string id)
    {
        var model = _paymentProviderRepository.Query().FirstOrDefault(x => x.Id == id);
        if (model == null) return BadRequest();
        model.IsEnabled = !model.IsEnabled;
        _paymentProviderRepository.SaveChanges();
        return Accepted();
    }

    [HttpGet]
    public async Task<IActionResult> BraintreeConfig()
    {
        var BraintreeProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperBraintree.BraintreeProviderId);
        var model = JsonConvert.DeserializeObject<BraintreeConfigForm>(BraintreeProvider.AdditionalSettings);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> BraintreeConfig(BraintreeConfigForm model)
    {
        if (ModelState.IsValid)
        {
            var BraintreeProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperBraintree.BraintreeProviderId);
            BraintreeProvider.AdditionalSettings = JsonConvert.SerializeObject(model);
            await _paymentProviderRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> CoDConfig()
    {

        var CoDProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperCoD.CODProviderId);
        var model = JsonConvert.DeserializeObject<CoDSetting>(CoDProvider.AdditionalSettings);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> CoDConfig(CoDSetting model)
    {
        if (ModelState.IsValid)
        {
            var CoDProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperCoD.CODProviderId);
            CoDProvider.AdditionalSettings = JsonConvert.SerializeObject(model);
            await _paymentProviderRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> MomoConfig()
    {
        var momoProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperMomo.MomoPaymentProviderId);
        var model = JsonConvert.DeserializeObject<MomoPaymentConfigForm>(momoProvider.AdditionalSettings);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> MomoConfig(MomoPaymentConfigForm model)
    {
        if (ModelState.IsValid)
        {
            var momoProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperMomo.MomoPaymentProviderId);
            momoProvider.AdditionalSettings = JsonConvert.SerializeObject(model);
            await _paymentProviderRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> NganLuongConfig()
    {
        var NganLuongProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperNganLuong.NganLuongPaymentProviderId);
        var model = JsonConvert.DeserializeObject<NganLuongConfigForm>(NganLuongProvider.AdditionalSettings);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> NganLuongConfig(NganLuongConfigForm model)
    {
        if (ModelState.IsValid)
        {
            var NganLuongProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperNganLuong.NganLuongPaymentProviderId);
            NganLuongProvider.AdditionalSettings = JsonConvert.SerializeObject(model);
            await _paymentProviderRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> PaypalExpressConfig()
    {
        var PaypalExpressProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperPaypalExpress.PaypalExpressProviderId);
        var model = JsonConvert.DeserializeObject<PaypalExpressConfigForm>(PaypalExpressProvider.AdditionalSettings);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> PaypalExpressConfig(PaypalExpressConfigForm model)
    {
        if (ModelState.IsValid)
        {
            var PaypalExpressProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperPaypalExpress.PaypalExpressProviderId);
            PaypalExpressProvider.AdditionalSettings = JsonConvert.SerializeObject(model);
            await _paymentProviderRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> StripeConfig()
    {
        var StripeProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperStripe.StripeProviderId);
        var model = JsonConvert.DeserializeObject<StripeConfigForm>(StripeProvider.AdditionalSettings);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> StripeConfig(StripeConfigForm model)
    {
        if (ModelState.IsValid)
        {
            var StripeProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelperStripe.StripeProviderId);
            StripeProvider.AdditionalSettings = JsonConvert.SerializeObject(model);
            await _paymentProviderRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}

