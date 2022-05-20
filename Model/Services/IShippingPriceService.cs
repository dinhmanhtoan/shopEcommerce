namespace Model.Services;

public interface IShippingPriceService
{
    Task<IList<ShippingPrice>> GetApplicableShippingPrices(GetShippingPriceRequest request);
}

