namespace Model.ViewModel;

public class ShipmentVm
{
    public long Id { get; set; }
    public DateTimeOffset CreateOn { get; set; }
    public string TrakingNumber { get; set; }
    public string WarehouseName { get; set; }
    public long OrderId { get; set; }

    public OrderAddress ShippingAddress { get; set; }

    public List<ShipmentItemVm> ProductVms { get; set; }
}

