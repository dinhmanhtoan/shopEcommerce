using Model.ViewModel.NganLuong;
namespace Shop.Components;

public class NganLuongLandingViewComponent : ViewComponent
{
    private readonly IRepositoryWithTypedId<PaymentProvider,string> _paymentProviderRepository;
    public NganLuongLandingViewComponent(IRepositoryWithTypedId<PaymentProvider, string> paymentProviderRepository)
    {
        _paymentProviderRepository = paymentProviderRepository;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var nganLuongProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == Model.ViewModel.NganLuong.PaymentProviderHelper.NganLuongPaymentProviderId);
        var nganLuongSetting = JsonConvert.DeserializeObject<NganLuongConfigForm>(nganLuongProvider.AdditionalSettings);
        return View();
    }
}

