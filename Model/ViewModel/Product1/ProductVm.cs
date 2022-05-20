namespace Model.ViewModel.Product1;

public class ProductVm
{
    public long Id { get; set; }
    public string Sku { get; set; }
    public string Gtin { get; set; }
    [Required(ErrorMessage = "This {0} is not Required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "This {0} is not Required")]
    public string Slug { get; set; }
    public string MetaTitle { get; set; }
    public string MetaKeywords { get; set; }
    public string MetaDescription { get; set; }
    public string NormalizedName { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public string Specification { get; set; }
    public string Present { get; set; }
    [Required(ErrorMessage = "This {0} is not Required")]
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal? SpecialPrice { get; set; }
    public DateTimeOffset? SpecialPriceStart { get; set; }
    public DateTimeOffset? SpecialPriceEnd { get; set; }
    public List<Category> categories { get; set; } = new List<Category>();
    public IList<long> CategoryIds { get; set; } = new List<long>();
    public List<Brand> brands { get; set; } = new List<Brand>();
    public Brand Brand { get; set; }
    public long? BrandId { get; set; }
    public DateTime? CreatedOn { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? EditOn { get; set; }
    public long? EditBy { get; set; }
    public bool IsFuture { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsCallForPricing { get; set; }
    public bool IsAllowToOrder { get; set; }
    public bool StockTrackingIsEnabled { get; set; }
    public int StockQuantity { get; set; }
    public bool IsHot { get; set; }
    public bool IsPublished { get; set; }
    public long? TaxClassId { get; set; }
    public long? VendorId { get; set; }
    public int DisplayOrder { get; set; }
    public TaxClass TaxClass { get; set; }
    public int ReviewsCount { get; set; }
    public double? RatingAverage { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public IList<long> DeletedMediaIds { get; set; } = new List<long>();
    public IList<ProductMediaVm> ProductImages { get; set; } = new List<ProductMediaVm>();
    public IList<MediaViewModel> Images { get; set; } = new List<MediaViewModel>();
    public IList<ProductOptionVm> Options { get; set; } = new List<ProductOptionVm>();
    public IList<ProductOptionVm> ProductOption { get; set; } = new List<ProductOptionVm>();
    public IList<ProductVariationVm> Variations { get; set; } = new List<ProductVariationVm>();
    public IList<ProductAttributeVm> Attributes { get; set; } = new List<ProductAttributeVm>();
    public IList<ProductLinkVm> RelatedProducts { get; set; } = new List<ProductLinkVm>();
    public IList<ProductLinkVm> CrossSellProducts { get; set; } = new List<ProductLinkVm>();
    public IList<CategoryListItem> CategoryListItem { get; set; } = new List<CategoryListItem>();
    public IList<SelectListItem> TaxClassListItem { get; set; } = new List<SelectListItem>();
    public IList<SelectListItem> AttributesListItem { get; set; } = new List<SelectListItem>();
    public IList<SelectListItem> TemplateListItem { get; set; } = new List<SelectListItem>();

}
