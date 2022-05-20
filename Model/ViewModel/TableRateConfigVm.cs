namespace Model.ViewModel;
public class TableRateConfigVm
{
    public long? StateOrProvinceId { get; set; }
    public long? DistrictId { get; set; }
    public string CountryId { get; set; }

    public IList<PriceAndDestinationForm> PriceAndDestinationForm = new List<PriceAndDestinationForm>();
    public IList<SelectListItem> StateOrProvinces { get; set; }

    public IList<SelectListItem> Districts { get; set; }

    public IList<SelectListItem> Countries { get; set; }
}
