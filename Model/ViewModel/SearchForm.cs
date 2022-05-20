namespace Model.ViewModel;
public class SearchForm
{
    public string Query { get; set; }

    public string Category { get; set; }

    public IList<SelectListItem> AvailableCategories { get; set; } = new List<SelectListItem>();
}
