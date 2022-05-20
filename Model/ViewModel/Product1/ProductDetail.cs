namespace Model.ViewModel.Product1;

public class ProductDetail
{
    public long Id { get; set; }
    public string Slug { get; set; }
    public string Sku { get; set; }
    public string Gtin { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string MetaTitle { get; set; }
    public string MetaKeywords { get; set; }
    public string MetaDescription { get; set; }
    public CalculatedProductPrice CalculatedProductPrice { get; set; }
    public string NormalizedName { get; set; }
    public string Description { get; set; }
    public string Specification { get; set; }
    public string Present { get; set; }
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal? SpecialPrice { get; set; }
    public DateTimeOffset? SpecialPriceStart { get; set; }
    public DateTimeOffset? SpecialPriceEnd { get; set; }
    public Brand Brand { get; set; }
    public long? BrandId { get; set; }
    public DateTime? CreatedOn { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? EditOn { get; set; }
    public long? EditBy { get; set; }
    public bool IsFuture { get; set; }
    public bool IsHot { get; set; }
    public bool IsCallForPricing { get; set; }
    public bool IsAllowToOrder { get; set; }
    public bool StockTrackingIsEnabled { get; set; }
    public int StockQuantity { get; set; }
    public int ReviewsCount { get; set; }
    public double? RatingAverage { get; set; }
    public bool HasVariation =>  Variations.Any();
    public IList<ProductDetailOption> AvailableOptions
    {
        get
        {
            var options = from opt in Variations.SelectMany(x => x.Options)
                            group opt by new
                            {
                                opt.OptionId,
                                opt.OptionName
                            }
                into g
                            select new ProductDetailOption
                            {
                                OptionId = g.Key.OptionId,
                                OptionName = g.Key.OptionName,
                                Values = g.Select(x => x.Value).Distinct().ToList()
                            };

            return options.ToList();
        }
    }


    public IDictionary<string, ProductOptionDisplay> OptionDisplayValues { get; set; } = new Dictionary<string, ProductOptionDisplay>();
    public IList<MediaViewModel> Images { get; set; } = new List<MediaViewModel>();

    public IList<ProductDetailVariation> Variations { get; set; } = new List<ProductDetailVariation>();
    public IList<ProductOptionForm> Options { get; set; } = new List<ProductOptionForm>();
    public IList<ProductDetailAttribute> Attributes { get; set; } = new List<ProductDetailAttribute>();

    public IList<ProductDetailCategory> Categories { get; set; } = new List<ProductDetailCategory>();
    public IList<ProductThumbnail> RelatedProducts { get; set; } = new List<ProductThumbnail>();

    public IList<ProductThumbnail> CrossSellProducts { get; set; } = new List<ProductThumbnail>();


}

