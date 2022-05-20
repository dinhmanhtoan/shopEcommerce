namespace Model.ViewModel;
public class CommentItem
{
    public long Id { get; set; }

    public string CommentText { get; set; }

    public string CommenterName { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public string CreatedOnRelative
    {
        get
        {
            return CreatedOn.Humanize();
        }
    }

    public string Status { get; set; }

    public IEnumerable<CommentItem> Replies { get; set; } = new List<CommentItem>();
}

