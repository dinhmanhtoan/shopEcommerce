namespace Shop.Controllers;
public class NewsCategoryController : Controller
{
    private int _pageSize = 24;
    private readonly IRepository<NewsItem> _newsItemRepository;
    private readonly IMediaService _mediaService;
    private readonly IRepository<NewsCategory> _newsCategoryRepository;
    public NewsCategoryController(IRepository<NewsItem> newsItemRepository, IMediaService mediaService, IRepository<NewsCategory> newsCategoryRepository)
    {
        _newsItemRepository = newsItemRepository;
        _mediaService = mediaService;
        _newsCategoryRepository = newsCategoryRepository;
    }
    public IActionResult NewsCategoryDetail(long id ,int page)
    {
        var newsCategoryList = _newsCategoryRepository.Query()
                            .Include(x => x.NewsItems).Where(x => !x.IsDeleted)
                            .Select(x => new NewsCategoryVm()
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Slug = x.Slug
                            }).ToList();
        if(newsCategoryList == null)
        {
            return NotFound();
        }
        var currentCategory = newsCategoryList.Select(x => new NewsCategoryVm()
        {
            Id = x.Id,
            Name = x.Name,
            Slug = x.Slug
        }).FirstOrDefault(x => x.Id == id);

        var model = new NewsVm()
        {
            CurrentNewsCategory = currentCategory,
            NewsCategory = newsCategoryList

        };
        var query = _newsItemRepository.Query()
            .Where(x => x.Categories.Any(c => c.CategoryId == currentCategory.Id) && !x.IsDeleted && x.IsPublished)
            .OrderBy(x => x.CreatedOn);
        model.TotalItem = query.Count();
        var currentPageNum = page <= 0 ? 1 : page;
        var offset = (_pageSize * currentPageNum) - _pageSize;
        while (currentPageNum > 1 && offset >= model.TotalItem)
        {
            currentPageNum--;
            offset = (_pageSize * currentPageNum) - _pageSize;
        }
        model.NewsItem = query.Include(x => x.ThumbnailImage).Select(x => new NewsItemThumbnail()
        {
            Id = x.Id,
            ShortContent = x.ShortContent,
            ImageUrl =  _mediaService.GetThumbnailUrl(x.ThumbnailImage),
            PublishedOn = x.PublishedOn,
            Slug = x.Slug
        }).Skip(offset)
          .Take(_pageSize)
          .ToList();
        return View(model);
    }
}
