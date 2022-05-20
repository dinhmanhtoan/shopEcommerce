namespace Model.Services;

public class NewsItemService : INewsItemService
{
    private const string NewsItemEntityTypeId = "NewsItem";
    private readonly IRepository<NewsItem> _newsItemRepository;
    private readonly IEntityService _entityService;

    public NewsItemService(IRepository<NewsItem> newsItemRepository, IEntityService entityService)
    {
        _newsItemRepository = newsItemRepository;
        _entityService = entityService;
    }
    public void Create(NewsItem newsItem)
    {
        if (newsItem != null)
        {
            using (var transaction = _newsItemRepository.BeginTransaction())
            {
                newsItem.Slug = _entityService.ToSafeSlug(newsItem.Slug, newsItem.Id, NewsItemEntityTypeId);
                _newsItemRepository.Add(newsItem);
                _newsItemRepository.SaveChanges();

                _entityService.Add(newsItem.Name, newsItem.Slug, newsItem.Id, NewsItemEntityTypeId);
                _newsItemRepository.SaveChanges();

                transaction.Commit();
            }
        }
    }
    public void Update(NewsItem newsItem)
    {
        if (newsItem != null)
        {
            newsItem.Slug = _entityService.ToSafeSlug(newsItem.Slug, newsItem.Id, NewsItemEntityTypeId);
            _entityService.Update(newsItem.Name, newsItem.Slug, newsItem.Id, NewsItemEntityTypeId);
            _newsItemRepository.SaveChanges();
        }
    }
    public  void Delete(long id)
    {
        var newsItem = _newsItemRepository.Query().First(x => x.Id == id);
         Delete(newsItem);
    }

    public void Delete(NewsItem newsItem)
    {
        if (newsItem != null)
        {
            newsItem.IsDeleted = true;
             _entityService.Remove(newsItem.Id, NewsItemEntityTypeId);
            _newsItemRepository.SaveChanges();
        }
    }

 
}

