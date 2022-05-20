namespace Model.ViewModel;
public class StockHistoryVm
{
    public long Id { get; set; }
    public string ProductName { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string UserFullName { get; set; }
    public long AdjustedQuantity { get; set;}
    public string Note { get; set; }
}

