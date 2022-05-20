namespace Model.Services;

public interface INewsItemService
{
    void Create(NewsItem newsItem);

    void Update(NewsItem newsItem);

    void Delete(long id);

    void Delete(NewsItem newsItem);
}

