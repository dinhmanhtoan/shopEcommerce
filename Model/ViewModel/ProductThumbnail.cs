using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
   public  class ProductThumbnail
    {

        public long Id { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public decimal? Price { get; set; }
        public decimal? Sale { get; set; }
        public long? CategoryId { get; set; }
        public Media ThumbnailImage { get; set; }
        public static ProductThumbnail FromProduct(Product product)
        {
            var productThumbnail = new ProductThumbnail
            {
                Id = product.Id,
                Code = product.Code,
                Title = product.Title,
                Description = product.Description,
                Detail = product.Detail,
                Price = product.Price,
                Sale = product.Sale,
                ThumbnailImage = product.Thumbnail,
            };

            return productThumbnail;
        }
    }
}
