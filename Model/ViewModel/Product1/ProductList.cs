using Model.ViewModel.Web;
namespace Model.ViewModel.Product1;

public class ProductList
{
    public IList<ProductThumbnail> products { get; set; } = new List<ProductThumbnail>();
    public IList<Category> categories { get; set; } = new List<Category>();
    public IList<Brand> Brand { get; set; } = new List<Brand>();
    public string Images { get; set; }
    public Search Search { get; set; }
}

