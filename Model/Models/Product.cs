namespace Model.Models;
public class Product : EntityBase
{
    public Product()
    {
        CreatedOn = DateTime.Now;
        EditOn = DateTime.Now;
    }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string NormalizedName { get; set; }

    [StringLength(450)]
    public string Sku { get; set; }

    [StringLength(450)]
    public string Gtin { get; set; }
    [StringLength(450)]
    public string MetaTitle { get; set; }
    [StringLength(450)]
    public string MetaKeywords { get; set; }
    public string MetaDescription { get; set; }

    [StringLength(450)]
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public string Specification { get; set; }
    public string Present { get; set; }
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal? SpecialPrice { get; set; }
    public DateTimeOffset? SpecialPriceStart { get; set; }

    public DateTimeOffset? SpecialPriceEnd { get; set; }

    public long? ThumbnailId { get; set; }
    public Media Thumbnail { get; set; }
    public IList<ProductMedia> Images { get; set; } = new List<ProductMedia>();
    public bool HasOptions { get; set; }
    public bool IsVisibleIndividually { get; set; }
    // goi de danh gia
    public bool IsCallForPricing { get; set; }
    // cho phep dat hang
    public bool IsAllowToOrder { get; set; }
    // bat theo doi hang ton kho
    public bool StockTrackingIsEnabled { get; set; }
    public int StockQuantity { get; set; }
    public bool IsPublished { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? PublishedOn { get; set; }
    public DateTime? CreatedOn { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? EditOn { get; set; }
    public long? EditBy { get; set; }
    public bool IsFuture { get; set; }
    public bool IsHot { get; set; }
    public IList<ProductCategory> Categories { get; set; } = new List<ProductCategory>();
    public Brand Brand { get; set; }
    public long? BrandId { get; set; }
    public long? TaxClassId { get; set; }
    public TaxClass TaxClass { get; set; }
    public int ReviewsCount { get; set; }
    public double? RatingAverage { get; set; }
    public int DisplayOrder { get; set; }
    public long? VendorId { get; set; }
    public IList<ProductOptionValue> OptionValues { get; set; } = new List<ProductOptionValue>();
    public IList<ProductLink> ProductLinks { get; protected set; } = new List<ProductLink>();
    public IList<ProductLink> LinkedProductLinks { get; protected set; } = new List<ProductLink>();
    public IList<ProductAttributeValue> AttributeValues { get; protected set; } = new List<ProductAttributeValue>();
    public IList<CartItem> cartItems { get; set; } = new List<CartItem>();

    public IList<ShipmentItem> ShipmentItem { get; set; } = new List<ShipmentItem>();
    public IList<ProductPriceHistory> PriceHistories { get; protected set; } = new List<ProductPriceHistory>();

    public void AddMedia(ProductMedia media)
    {
        media.Product = this;
        Images.Add(media);
    }
    public void AddAttributeValue(ProductAttributeValue attributeValue)
    {
        attributeValue.Product = this;
        AttributeValues.Add(attributeValue);
    }
    public void AddOptionValue(ProductOptionValue optionValue)
    {
        optionValue.Product = this;
        OptionValues.Add(optionValue);
    }
    public IList<ProductOptionCombination> OptionCombinations { get; protected set; } = new List<ProductOptionCombination>();

    public void AddOptionCombination(ProductOptionCombination combination)
    {
        combination.Product = this;
        OptionCombinations.Add(combination);
    }
    public void AddProductLinks(ProductLink productLink)
    {
        productLink.Product = this;
        ProductLinks.Add(productLink);
    }
    public void AddCategory(ProductCategory category)
    {
        category.Product = this;
        Categories.Add(category);
    }
    public Product Clone()
    {
        var product = new Product();
        product.Name = Name;
        product.Slug = Slug;
        product.Sku = Sku;
        product.Gtin = Gtin;
        product.MetaTitle = MetaTitle;
        product.MetaKeywords = MetaKeywords;
        product.MetaDescription = MetaDescription;
        product.NormalizedName = NormalizedName;
        product.Description = Description;
        product.ShortDescription = ShortDescription;
        product.Price = Price;
        product.OldPrice = OldPrice;
        product.Present = Present;
        product.SpecialPrice = SpecialPrice;
        product.SpecialPriceStart = SpecialPriceStart;
        product.SpecialPriceEnd = SpecialPriceEnd;
        product.ThumbnailId = ThumbnailId;
        product.BrandId = BrandId;
        product.TaxClassId = TaxClassId;
        product.VendorId = VendorId;
        product.HasOptions = HasOptions;
        product.IsVisibleIndividually = IsVisibleIndividually;
        product.IsAllowToOrder = IsAllowToOrder;
        product.IsCallForPricing = IsCallForPricing;
        product.StockQuantity = StockQuantity;
        product.StockTrackingIsEnabled = StockTrackingIsEnabled;
        product.IsDeleted = false;
        product.DisplayOrder = DisplayOrder;
        product.IsPublished = IsPublished;

        foreach (var category in Categories)
        {
            product.AddCategory(new ProductCategory
            {
                CategoryId = category.CategoryId
            });
        }
        return product;
    }
}
