namespace Model.ViewModel.Product1;

public class ProductPTV
{
    public long Id { get; set; }
    public string Title { get; set; }
    public IList<ProductLinkVm> RelatedProducts { get; set; } = new List<ProductLinkVm>();
    public IList<ProductLinkVm> CrossSellProducts { get; set; } = new List<ProductLinkVm>();
    public IList<CartRuleProductVm> CartRuleProducts { get; set; } = new List<CartRuleProductVm>();
    public IList<ProductLinkVm> Products { get; set; } = new List<ProductLinkVm>();

}

