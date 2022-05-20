namespace Model.ViewModel.Product1;

public class ProductThumbnail
{
    public long Id { get; set; }
    public string Sku { get; set; }
    public string Gtin { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string NormalizedName { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public string Specification { get; set; }
    public string Present { get; set; }
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal? SpecialPrice { get; set; }
    public DateTimeOffset? SpecialPriceStart { get; set; }
    public DateTimeOffset? SpecialPriceEnd { get; set; }
    public bool IsCallForPricing { get; set; }
    public bool IsAllowToOrder { get; set; }
    public int? StockQuantity { get; set; }
    public Brand Brand { get; set; }
    public long? BrandId { get; set; }
    public List<long> CategoryId { get; set; }
    public Media ThumbnailImage { get; set; }
    public string ThumbnailUrl { get; set; }
    public int ReviewsCount { get; set; }

    public double? RatingAverage { get; set; }
    public CalculatedProductPrice CalculatedProductPrice { get; set; }
    public static ProductThumbnail FromProduct(Product product)
    {
        var productThumbnail = new ProductThumbnail
        {
            Id = product.Id,
            Sku = product.Sku,
            Gtin = product.Gtin,
            Name = product.Name,
            Slug = product.Slug,
            NormalizedName = product.NormalizedName,
            Description = product.Description,
            ShortDescription = product.ShortDescription,
            Specification = product.Specification,
            Price = product.Price,
            OldPrice = product.OldPrice,
            SpecialPrice = product.SpecialPrice,
            SpecialPriceStart = product.SpecialPriceStart,
            SpecialPriceEnd = product.SpecialPriceEnd,
            BrandId = product.BrandId,
            Brand = product.Brand,
            ReviewsCount = product.ReviewsCount,
            RatingAverage = product.RatingAverage,
            StockQuantity = product.StockQuantity,
            IsAllowToOrder = product.IsAllowToOrder,
            IsCallForPricing = product.IsCallForPricing,
            ThumbnailImage = product.Thumbnail
        };
        return productThumbnail;
    }
}

