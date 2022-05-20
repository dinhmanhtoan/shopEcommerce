
namespace Model.Models;
public class Warehouse : EntityBase
{
    public Warehouse()
    {
    
    }
    public Warehouse(long id)
    {
        Id = id;
    }

    public string Name { get; set; }
    public IList<Stock> Stocks { get; set; }
    public long? VendorId { get; set; }
    public Vender Vendor { get; set; }
    public long AddressId { get; set; }
    public Address Address { get; set; }
}

