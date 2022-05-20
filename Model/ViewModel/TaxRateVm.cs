namespace Model.ViewModel;

public class TaxRateVm
{
    public long TaxRateId { get; set; }
    [Range(1, long.MaxValue, ErrorMessage = "Tax Class is required")]
    public long TaxClassId { get; set; }
    public string TaxClassName { get; set; }
    public string CountryName { get; set; }
    [Required(ErrorMessage = "The {0} field is required.")]
    public string CountryId { get; set; }
    public long? StateOrProvinceId { get; set; }
    public string StateOrProvinceName { get; set; }
    public string ZipCode { get; set; }
    [Required(ErrorMessage = "The {0} field is required.")]
    public decimal Rate { get; set; }
    public IList<SelectListItem> TaxClass { get; set; }
    public IList<SelectListItem> StateOrProvinces { get; set; }
    public IList<SelectListItem> Contries { get; set; }
}

