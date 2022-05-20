namespace Model.ViewModel.Widget1;
public class CarouselWidgetItemForm
{
    public string Image { get; set; }
    public string ImageUrl { get; set; }
    [Newtonsoft.Json.JsonIgnore]
    public IFormFile UploadImage { get; set; }
    public string Caption { get; set; }
    public string SubCaption { get; set; }
    public string LinkText { get; set; }
    public string TargetUrl { get; set; }
}
