namespace Model.ViewModel;
public class OrderHistoryListItem
{

    public long Id { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public decimal SubTotal { get; set; }
    public string SubTotalString { get { return SubTotal.ToString(); } }

    public OrderStatus OrderStatus { get; set; }

    public IList<OrderHistoryProductVm> OrderItems { get; set; } = new List<OrderHistoryProductVm>();

}

