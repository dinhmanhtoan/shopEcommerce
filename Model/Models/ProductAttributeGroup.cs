namespace Model.Models;
public class ProductAttributeGroup : EntityBase
{
    [Required(ErrorMessage= "This {0} field is Required.")]
    [MaxLength(450)]
    public string Name { get; set; }
    public IList<ProductAttribute> Attributes { get; set; } = new List<ProductAttribute>();
}

