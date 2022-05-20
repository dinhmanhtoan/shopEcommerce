namespace Model.ViewModel;

 public class PageVm
{
    public string text { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }

    public string Body { get; set; }

    public string MetaTitle { get; set; }

    public string MetaKeywords { get; set; }

    public string MetaDescription { get; set; }
    public string href { get; set; }
    public string shortContent
    {
        get
        {
            if (text.Length > 21)
            {
                text = text.Remove(19);
                text += "..";
            }
            return text;
        }
        set
        {
            text = value;
        }
    }
}

