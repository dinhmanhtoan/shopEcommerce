namespace Model.ViewModel.Attribute;

public class ProductTemplateFrom
{
    public long? Id { get; set; }
    [Required(ErrorMessage = "The {0} field is required.")]
    public string Name { get; set; }
    public IList<ProductAttributeVm> Attributes { get; set; } = new List<ProductAttributeVm>();

    public IList<SelectListItem> AttributeList { get; set; } = new List<SelectListItem>();
}

