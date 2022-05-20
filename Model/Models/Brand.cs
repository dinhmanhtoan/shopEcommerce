namespace Model.Models;
public class Brand :EntityBase
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public IList<Product> Products { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsPublished { get; set; }
}

