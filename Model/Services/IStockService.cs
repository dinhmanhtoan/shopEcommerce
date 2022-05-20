namespace Model.Services;

public interface IStockService
{
    public Task AddAllProduct(Warehouse warehouse);
    public Task UpdateStock(StockUpdateRequest stockUpdateRequest);
}
