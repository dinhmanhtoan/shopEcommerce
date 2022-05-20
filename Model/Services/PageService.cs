namespace Model.Services;
public class PageService : IPageService
{
    public const string PageEntityTypeId = "Page";
    private readonly IEntityService _entityService;
    private readonly IRepository<Pages> _pageRepository;

    public PageService(IRepository<Pages> pageRepository, IEntityService entityService)
    {
        _pageRepository = pageRepository;
        _entityService = entityService;
    }
    public async Task Create(Pages page)
    {
        using (var transaction = _pageRepository.BeginTransaction())
        {
            page.Slug = _entityService.ToSafeSlug(page.Slug, page.Id, PageEntityTypeId);
            _pageRepository.Add(page);
            await _pageRepository.SaveChangesAsync();

            _entityService.Add(page.Name, page.Slug, page.Id, PageEntityTypeId);
            await _pageRepository.SaveChangesAsync();

            transaction.Commit();
        }
    }
    public async Task Update(Pages page)
    {
        page.Slug = _entityService.ToSafeSlug(page.Slug, page.Id, PageEntityTypeId);
        _entityService.Update(page.Name, page.Slug, page.Id, PageEntityTypeId);
        await _pageRepository.SaveChangesAsync();
    }
    public async Task Delete(Pages page)
    {
        _pageRepository.Remove(page);
         _entityService.Remove(page.Id, PageEntityTypeId);
        await _pageRepository.SaveChangesAsync();
    }


}
