namespace Model.ViewModel.Widget1;
public class WidgetFormBase
{
    public long Id { get; set; }

    [Required(ErrorMessage = "The {0} field is required.")]
    public string Name { get; set; }

    public long WidgetZoneId { get; set; }
    public IList<SelectListItem> WidgetZoneItems { get; set; } = new List<SelectListItem>();

    public DateTimeOffset? PublishStart { get; set; }

    public DateTimeOffset? PublishEnd { get; set; }

    public int DisplayOrder { get; set; }
    public int numberOfWidgets { get; set; }
}
