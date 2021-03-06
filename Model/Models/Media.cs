namespace Model.Models;
public class Media : EntityBase
{
    [StringLength(450)]
    public string Caption { get; set; }

    public int FileSize { get; set; }

    [StringLength(450)]
    public string FileName { get; set; }

    public MediaType MediaType { get; set; }
    public IList<ProductMedia> Products { get; set; } = new List<ProductMedia>();
}
