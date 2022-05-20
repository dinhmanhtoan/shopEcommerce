namespace Model.Models;
public class RecentlyViewedProduct : EntityBase
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public DateTimeOffset LatestViewedOn { get; set; }
}

