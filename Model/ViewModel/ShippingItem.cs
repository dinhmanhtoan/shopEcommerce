namespace Model.ViewModel;

public class ShippingItem
{
    public long ProductId { get; set; }

    public Product Product { get; set; }

    public int Quantity { get; set; }
}

