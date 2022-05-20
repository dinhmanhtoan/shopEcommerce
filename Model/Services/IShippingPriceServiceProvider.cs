namespace Model.Services;

public interface IShippingPriceServiceProvider
{
    Task<GetShippingPriceResponse> GetShippingPrices(GetShippingPriceRequest request, ShippingProvider provider);
}

