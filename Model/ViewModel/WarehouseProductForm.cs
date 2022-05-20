namespace Model.ViewModel;

public class WarehouseProductForm
{
    public long WarehouseId { get; set; }
    public IList<SelectListItem> Warehouse { get; set; }
    public IList<MangeWarehouseProductItemVm> MangeWarehouseProductItemVm { get; set; }
}

