namespace Model.Services;

public interface ITaxService
{
    public Task<decimal> GetTaxPercent(long? taxClassId, string countryId, long? stateOrProvinceId, string zipCode);
}
