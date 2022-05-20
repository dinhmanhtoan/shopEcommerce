namespace Model.ViewModel.Attribute;

public class ProductAttributeFormVm
{
    [Required(ErrorMessage = "The {0} field is required.")]
    public string Name { get; set; }
    public long GroupId { get; set; }
    public IList<SelectListItem> Groups { get; set; } = new List<SelectListItem>();
}

