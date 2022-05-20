namespace Shop.Controllers;
public class NewsItemController : Controller
{
    private readonly IRepository<NewsItem> _newsItemRepository;
    private readonly IRepository<NewsCategory> _newsCategoryRepository;
    private readonly IMediaService _mediaService;
    private int _pageSize = 24;
    public NewsItemController(IRepository<NewsItem> newsItemRepository,
         IRepository<NewsCategory> newsCategoryRepository,
        IMediaService mediaService,
        IConfiguration config)
    {
        _newsItemRepository = newsItemRepository;
        _newsCategoryRepository = newsCategoryRepository;
        _mediaService = mediaService;
    }
    [HttpGet("News")]
    public IActionResult NewsHome(int page)
    {
        var newsCategoryList = _newsCategoryRepository.Query()
                .Include(x => x.NewsItems)
                .Where(x => !x.IsDeleted)
                .Select(x => new NewsCategoryVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug
                })
                .ToList();
        var model = new NewsVm()
        {
            NewsCategory = newsCategoryList
        };
        var query = _newsItemRepository.Query()
            .Where(x => !x.IsDeleted && x.IsPublished)
            .OrderByDescending(x => x.CreatedOn);

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
            ImageUrl = _mediaService.GetMediaUrl(x.ThumbnailImage),
            PublishedOn = x.CreatedOn,
            Slug = x.Slug
        })
            .Skip(offset)
            .Take(_pageSize)
            .ToList();

        model.PageSize = _pageSize;
        model.Page = currentPageNum;
        return View(model);
    }
    public IActionResult NewsItemDetail(long id)
    {
        var newsItem = _newsItemRepository.Query()
            .Include(x => x.ThumbnailImage)
            .FirstOrDefault(x => x.Id == id && x.IsPublished && !x.IsDeleted);

        if (newsItem == null)
        {
            return NotFound();
        }

        var model = new NewsItemVm()
        {
            Name = newsItem.Name,
            FullContent = newsItem.FullContent,
            ThumbnailImageUrl = _mediaService.GetThumbnailUrl(newsItem.ThumbnailImage)
        };

        return View(model);
    }
}
