namespace Model.ViewModel;
public class CartInfoForCoupon
{
    public IList<CartItemForCoupon> Items { get; set; } = new List<CartItemForCoupon>();
}
