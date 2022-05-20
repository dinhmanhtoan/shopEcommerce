namespace Model.ViewModel.Widget1;
public class CarouselWidgetForm : WidgetFormBase
{
    public IList<CarouselWidgetItemForm> Items { get; set; } = new List<CarouselWidgetItemForm>();
}
