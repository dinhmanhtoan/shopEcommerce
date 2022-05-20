namespace Model.ViewModel.Product1;

public class ProductsByCategory
{
    public long CategoryId { get; set; }

    public long? ParentCategorId { get; set; }

    public string CategoryName { get; set; }

    public string CategorySlug { get; set; }

    public string CategoryMetaTitle { get; set; }

    public string CategoryMetaKeywords { get; set; }

    public string CategoryMetaDescription { get; set; }

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

