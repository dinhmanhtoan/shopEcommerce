namespace Model.ViewModel;

public class SearchResult
{
    public long BrandId { get; set; }

    public string BrandName { get; set; }

    public string BrandSlug { get; set; }

    public int TotalProduct { get; set; }

    public IList<ProductThumbnail> Products { get; set; } = new List<ProductThumbnail>();

    public FilterOption FilterOption { get; set; }

    public SearchOption CurrentSearchOption { get; set; }

    public IList<SelectListItem> AvailableSortOptions => new List<SelectListItem>
    {
        new SelectListItem { Text = "Giá: Thấp đến cao", Value = "price-asc" },
        new SelectListItem { Text = "Giá: Cao đến thấp", Value = "price-desc" }
    };
}

