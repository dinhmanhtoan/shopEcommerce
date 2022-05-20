using Newtonsoft.Json.Converters;
namespace Model.ViewModel.Widget1;
public class ProductWidgetSetting
{
    public int NumberOfProducts { get; set; }

    public long? CategoryId { get; set; }

    public IList<SelectListItem> CategoryListItems = new List<SelectListItem>();

    [System.Text.Json.Serialization.JsonConverter(typeof(StringEnumConverter))]
    public ProductWidgetOrderBy OrderBy { get; set; }

    public IList<SelectListItem> OrderByListItems = new List<SelectListItem>();
    public bool FeaturedOnly { get; set; }

    public long? ProductId { get; set; }
}

