namespace Model.ViewModel;
public class CheckoutPaymentForm
{
    public IList<PaymentProviderVm> PaymentProviders { get; set; } = new List<PaymentProviderVm>();
}

