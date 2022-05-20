namespace Model.ViewModel.Widget1;

public class ProductWidgetComponentVm
{
    public long Id { get; set; }

    public string WidgetName { get; set; }

    public ProductWidgetSetting Setting { get; set; }

    public IList<ProductThumbnail> Products { get; set; }
}
