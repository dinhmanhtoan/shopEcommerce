namespace Model.Models;
public class ProductTemplateProductAttribute : EntityBase
{
    public long ProductTemplateId { get; set; }
    public ProductTemplate ProductTemplate { get; set; }
    public long ProductAttributeId { get; set; }
    public ProductAttribute ProductAttribute { get; set; }
}

