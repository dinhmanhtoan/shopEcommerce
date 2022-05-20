namespace Model.ViewModel;
public class ProductOptionForm
{
    [Required(ErrorMessage = "The {0} field is required.")]
    public string Name { get; set; }
}
