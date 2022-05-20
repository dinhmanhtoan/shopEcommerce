namespace Model.ViewModel.Widget1;
public class CategoryWidgetSettings
{
    public long CategoryId { get; set; }
    public IList<SelectListItem> CategoryListItems { get; set; } = new List<SelectListItem>();
}
