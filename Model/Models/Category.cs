namespace Model.Models;
public partial class Category : EntityBase
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public string MetaTitle { get; set; }
    public string MetaKeywords { get; set; }
    public string MetaDescription { get; set; }
    public long? ThumbnailId { get; set; }
    public Media Thumbnail { get; set; }
    public int DisplayOrder { get; set; }
    public bool IncludeInMenu { get; set; }
    public bool IsPublished { get; set; }
    public bool IsDeleted { get; set; }
    public long? ParentId { get; set; }
    public Category Parent { get; set; }
    public IList<Category> Children { get; protected set; } = new List<Category>();
    public IList<ProductCategory> Products { get; set; } = new List<ProductCategory>();

}

