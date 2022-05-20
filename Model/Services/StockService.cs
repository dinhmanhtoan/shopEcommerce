namespace Model.Services;

public class StockService :IStockService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Stock> _stockRepository;
    private readonly IRepository<StockHistory> _stockHistoryRepository;

    public StockService(IRepository<Product> productRepository, IRepository<Stock> stockRepository , IRepository<StockHistory> stockHistoryRepository)
    {
        _productRepository = productRepository;
        _stockRepository = stockRepository;
        _stockHistoryRepository = stockHistoryRepository;
    }
    public async Task AddAllProduct(Warehouse warehouse)
    {
        // && x.VendorId == warehouse.VendorId
        var products = await _productRepository.Query().Where(x => !x.HasOptions && !x.IsDeleted)
             .GroupJoin(_stockRepository.Query().Where(x => x.WarehouseId == warehouse.Id),
                         product => product.Id, stock => stock.ProductId,
                         (product, stock) => new { ProductId = product.Id, stock })
             .SelectMany(x => x.stock.DefaultIfEmpty(), (x, stock) => new
             {
                 x.ProductId,
                 stock
             }).Where(x => x.stock == null).ToArrayAsync();
        var stock = products.Select(x => new Stock { ProductId = x.ProductId, WarehouseId = warehouse.Id, Quantity = 0 });
          _stockRepository.AddRange(stock);
          await _stockRepository.SaveChangesAsync();
    
    }

    public async Task UpdateStock(StockUpdateRequest stockUpdateRequest)
    {
        var product = await _productRepository.Query().FirstOrDefaultAsync(x => x.Id == stockUpdateRequest.ProductId);
        var stock = await _stockRepository.Query().FirstOrDefaultAsync(x => x.ProductId == stockUpdateRequest.ProductId && x.WarehouseId == stockUpdateRequest.WarehouseId);

        stock.Quantity = stock.Quantity + stockUpdateRequest.AdjustedQuantity;
        product.StockQuantity = product.StockQuantity + stockUpdateRequest.AdjustedQuantity;
        var stockHistory = new StockHistory
        {
            ProductId = stockUpdateRequest.ProductId,
            WarehouseId = stockUpdateRequest.WarehouseId,
            AdjustedQuantity = stockUpdateRequest.AdjustedQuantity,
            Note = stockUpdateRequest.Note,
            CreatedById = stockUpdateRequest.UserId,
            CreatedOn = DateTimeOffset.Now,
        };
        _stockHistoryRepository.Add(stockHistory);
        await _stockHistoryRepository.SaveChangesAsync();

    }
}

