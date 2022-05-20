namespace Model.ViewModel;

public class ShipmentForm
{
    public long OrderId { get; set; }

    public long WarehouseId { get; set; }

    public string TrackingNumber { get; set; }

    public IList<ShipmentItemVm> Items { get; set; } = new List<ShipmentItemVm>();

    public IList<SelectListItem> Warehouse { get; set; } = new List<SelectListItem>();
}

