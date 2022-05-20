namespace Model.Services;
public interface IShipmentService
{
    Task<IList<ShipmentItemVm>> GetShipmentItem(long orderId);

    Task<IList<ShipmentItemVm>> GetItemToShip(long orderId, long warehouseId);

    Task<Result> CreateShipment(Shipment shipment);
}
