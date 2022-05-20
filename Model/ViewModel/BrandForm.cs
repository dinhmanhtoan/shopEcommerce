namespace Model.ViewModel;
public class BrandForm
{
    public long Id { get; set; }
    [Required(ErrorMessage = "the {0} field is required"), MaxLength(250)]
    public string Name { get; set; }
    [Required(ErrorMessage = "the {0} field is required")]
    public string Slug { get; set; }
    public IList<Product> Products { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsPublished { get; set; }
}
