namespace Model.Services;

public interface IProductService
{

    public Task<List<Product>> GetAll();
    public Task<List<Product>> GetByCategory(long categoryId);
    public void AddProduct(Product entity);
    public Task UpdateProduct(Product entity);
    public Task Delete(long Id);
    public Task<Product> getById(long Id);

}
public class ProductService : IProductService
{
    private const string ProductEntityTypeId = "Product";
    private readonly IEntityService _entityService;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ProductCategory> _productCategoryRepository;

    public ProductService(IRepository<Product> productRepository, IRepository<ProductCategory> productCategoryRepository, IEntityService entityService)
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
        _entityService = entityService;
    }
    public  async Task<List<Product>> GetAll()
    {
        var res = await _productRepository.Query().Where(x => !x.IsDeleted).ToListAsync();
        return res;
    }
    public async Task<List<Product>> GetByCategory(long categoryId)
    {
        var producCategory = await _productCategoryRepository.Query().Include(x=> x.Product).Include(x => x.Category).Where(x=> x.CategoryId == categoryId).ToListAsync();
        var products = new List<Product>();
        foreach (var item in producCategory)
        {
            var product = new Product()
            {
                //Id = item.Product.Id,
                Sku = item.Product.Sku,
                Name = item.Product.Name,
                NormalizedName = item.Product.NormalizedName,
                ShortDescription = item.Product.ShortDescription,
                Slug = item.Product.Slug,
                BrandId = item.Product.BrandId,
                Price = item.Product.Price,
                Thumbnail = item.Product.Thumbnail,
            };
            products.Add(product);
        }
        return products;
    }

    public void  AddProduct(Product product)
    {
        product.Slug = _entityService.ToSafeSlug(product.Slug, product.Id, ProductEntityTypeId);
        _productRepository.Add(product);
         _productRepository.SaveChanges();
        _entityService.Add(product.Name, product.Slug, product.Id, ProductEntityTypeId);
         _productRepository.SaveChanges();
    }

    public async Task UpdateProduct(Product product)
    {
        var slug = _entityService.Get(product.Id, ProductEntityTypeId);
        if (product.IsVisibleIndividually)
        {
            product.Slug = _entityService.ToSafeSlug(product.Slug, product.Id, ProductEntityTypeId);
            if (slug != null)
            {
                _entityService.Update(product.Name, product.Slug, product.Id, ProductEntityTypeId);
            }
            else
            {
                _entityService.Add(product.Name, product.Slug, product.Id, ProductEntityTypeId);
            }
        }
        else
        {
            if (slug != null)
            {
                _entityService.Remove(product.Id, ProductEntityTypeId);
            }
        }
           
       await _productRepository.SaveChangesAsync();
    }

    public async Task Delete(long Id)
    {
            var product = await _productRepository.Query().Include(x=>x.ProductLinks).FirstOrDefaultAsync(x => x.Id == Id);
            if (product != null)
            {
                product.IsDeleted = true;
                    _entityService.Remove(Id, ProductEntityTypeId);
                await _productRepository.SaveChangesAsync();
            }
    }
    public Task<Product> getById(long Id)
    {
        var res = _productRepository.Query().Include(x=>x.Thumbnail).FirstOrDefaultAsync(x => x.Id == Id);
        return res;
    }
}

