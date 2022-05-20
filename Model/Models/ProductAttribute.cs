namespace Model.Models;
public class ProductAttribute: EntityBase
{
    [Required(ErrorMessage = "This {0} field is Required.")]
    [MaxLength(450)]
    public string Name { get; set; }
    public long GroupId { get; set; }

    public ProductAttributeGroup Group { get; set; }

    public IList<ProductTemplateProductAttribute> ProductTemplates { get; protected set; } = new List<ProductTemplateProductAttribute>();
}

