namespace Model.Services;

public interface IBrandService
{
    public Task<List<Brand>> GetAll();
    public void AddBrand(Brand entity);
    public void UpdateBrand(Brand entity);
    public void Delete(long Id);
    public Brand getById(long Id);

}
public class BrandService : IBrandService
{
    private const string BrandEntityTypeId = "Brand";
    private readonly IEntityService _entityService;
    private IRepository<Brand> _brandRepository;

    public BrandService(IRepository<Brand> brandRepository, IEntityService entityService)
    {
        _brandRepository = brandRepository;
        _entityService = entityService;
    }
    public async Task<List<Brand>> GetAll()
    {
        var res = await _brandRepository.Query().Where(x=> !x.IsDeleted).ToListAsync();
        return res;
    }

    public void AddBrand(Brand brand)
    {
        brand.Slug = _entityService.ToSafeSlug(brand.Slug, brand.Id, BrandEntityTypeId);
        _brandRepository.Add(brand);
        _brandRepository.SaveChanges();
        _entityService.Add(brand.Name, brand.Slug, brand.Id, BrandEntityTypeId);
        _brandRepository.SaveChanges();
    }

    public void UpdateBrand(Brand brand)
    {
         brand.Slug = _entityService.ToSafeSlug(brand.Slug, brand.Id, BrandEntityTypeId);
        _entityService.Update(brand.Name, brand.Slug, brand.Id, BrandEntityTypeId);
        _brandRepository.SaveChanges();
    }

    public void Delete(long Id)
    {
        var Brand = _brandRepository.Query().FirstOrDefault(x => x.Id == Id);
        if (Brand != null)
        {
            Brand.IsDeleted = true;
                _entityService.Remove(Id, BrandEntityTypeId);
            _brandRepository.SaveChanges();
        }
    }
    public Brand getById(long Id)
    {
        var res = _brandRepository.Query().FirstOrDefault(x => x.Id == Id);
        return res;
    }
}


