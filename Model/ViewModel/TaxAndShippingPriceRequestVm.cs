namespace Model.ViewModel;

public class TaxAndShippingPriceRequestVm
{
    public string SelectedShippingMethodName { get; set; }

    public ShippingAddressVm NewShippingAddress { get; set; }

    public ShippingAddressVm NewBillingAddress { get; set; }

    public long ExistingShippingAddressId { get; set; }
}

