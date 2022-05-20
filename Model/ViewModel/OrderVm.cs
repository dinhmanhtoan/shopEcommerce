namespace Model.ViewModel;

public  class OrderVm
{
    private readonly ICurrencyService _currencyService;
    public OrderVm(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }
    public long OrderId { get; set; }
    public string CustomerName { get; set; }
    public decimal OrderTotal { get; set; }
    public string OrderTotalString { get { return _currencyService.FormatCurrency(OrderTotal); } } 
    public string OrderStatus { get; set; }
    public DateTimeOffset CreatedOn { get; set; }     
}

