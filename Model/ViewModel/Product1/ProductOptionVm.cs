namespace Model.ViewModel.Product1;

public class ProductOptionVm
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string DisplayType { get; set; }

    public IList<ProductOptionValueVm> Values { get; set; } = new List<ProductOptionValueVm>();
}
