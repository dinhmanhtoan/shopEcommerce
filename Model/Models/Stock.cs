namespace Model.Models;
public class Stock : EntityBase
{
    public Product Product { get; set; }
    public long ProductId { get; set; }
    public Warehouse Warehouse { get; set; }
    public long WarehouseId { get; set; }
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
}

