namespace Model.ViewModel.New;

public class NewsItemThumbnail
{
    public long Id { get; set; }

    public string ShortContent { get; set; }
        
    public string ImageUrl { get; set; }

    public DateTimeOffset? PublishedOn { get; set; }

    public string Slug { get; set; }
}

