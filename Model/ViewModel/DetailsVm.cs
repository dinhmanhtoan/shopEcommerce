namespace Model.ViewModel;
public class DetailsVm
{
    public Product product { get; set; }
    public List<Product> products { get; set; }
    public IList<ProductOptionForm> ProductOptionVm { get; set; } = new List<ProductOptionForm>();
}

