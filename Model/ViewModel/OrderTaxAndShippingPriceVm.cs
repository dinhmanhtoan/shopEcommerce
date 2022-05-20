namespace Model.ViewModel;

public class OrderTaxAndShippingPriceVm
{
    public bool IsProductPriceIncludedTax { get; set; }

    public IList<ShippingPrice> ShippingPrices { get; set; }

    public string SelectedShippingMethodName { get; set; }

    public CartVm Cart { get; set; }
}

