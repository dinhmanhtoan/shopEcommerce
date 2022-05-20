namespace Model.ViewModel.Widget1;
public class ProductCountDownWidgetComponentVm
{
    public long Id { get; set; }
    public string WidgetName { get; set; }
    public int DataInterval { get; set; } = 6000;
    public ProductThumbnail Products { get; set; }
}
