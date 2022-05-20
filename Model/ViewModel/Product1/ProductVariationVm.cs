namespace Model.ViewModel.Product1;
public class ProductVariationVm
{
    public long Id { get; set; }
    public string Sku { get; set; }
    public string Gtin { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public IFormFile ThumbnailImage { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public IList<long> DeleteNewImages { get; set; } = new List<long>();
    public IList<IFormFile> NewImages { get; set; } = new List<IFormFile>();
    public IList<string> ImageUrls { get; set; } = new List<string>();
    public IList<ProductMediaVm> ImageVariation { get; set; } = new List<ProductMediaVm>();
    public IList<ProductOptionCombinationVm> OptionCombinations { get; set; } =
        new List<ProductOptionCombinationVm>();
}

