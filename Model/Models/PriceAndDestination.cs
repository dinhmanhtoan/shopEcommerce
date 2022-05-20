namespace Model.Models;
public class PriceAndDestination : EntityBase
{
    public Country Country { get; set; }

    [StringLength(450)]
    public string CountryId { get; set; }
    public StateOrProvince StateOrProvince { get; set; }
    public long? StateOrProvinceId { get; set; }
    public District District { get; set; }
    public long? DistrictId { get; set; }

    [StringLength(450)]
    public string ZipCode { get; set; }
    public string Note { get; set; }
    public decimal MinOrderSubtotal { get; set; }
    public decimal ShippingPrice { get; set; }
}
