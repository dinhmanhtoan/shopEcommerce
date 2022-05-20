namespace Model.ViewModel.Widget1;
public class ProductCarouselWidgetComponentVm
{
    public long Id { get; set; }
    public string WidgetName { get; set; }
    public int DataInterval { get; set; } = 6000;
    public ProductWidgetSetting Setting { get; set; }
    public IList<ProductThumbnail> Products { get; set; }
    public List<Brand> Brands { get; set; }
}
