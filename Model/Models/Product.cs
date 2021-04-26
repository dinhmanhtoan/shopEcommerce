using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Product
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public long? ThumbnailId { get; set; }
        public Media Thumbnail { get; set; }
        public IList<ProductMedia> Images { get; set; } = new List<ProductMedia>();
        public decimal? Price { get; set; }
        public decimal? Sale { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditOn { get; set; }
        public long? EditBy { get; set; }
        public long? CategoryId { get; set; }
        public Category Category { get; set; }
        public IList<Rating> Rating { get; set; }
        public IList<OrderDetail> OrderDetails { get; set; }
        public Brand Brand { get; set; }
        public long? BrandId { get; set; }
        public IList<ProductOptionValue> OptionValues { get; set; }


        public void AddMedia(ProductMedia media)
        {
            media.Product = this;
            Images.Add(media);
        }

    }
}
