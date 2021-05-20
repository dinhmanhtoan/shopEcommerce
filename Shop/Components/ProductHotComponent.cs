using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Services;
using Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//future
namespace Shop.Components
{
    public class ProductHotViewComponent : ViewComponent
    {

        private readonly shopContext _shopContext;
        private readonly IMediaService _mediaService;
        public ProductHotViewComponent(shopContext shopContext, IMediaService mediaService)
        {
            _shopContext = shopContext;
            _mediaService = mediaService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = _shopContext.Product.Where(x => x.IsHot == true).Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Brand).Include(x => x.Category)
                  .Include(x => x.Thumbnail).Include(x => x.OptionValues).ThenInclude(x => x.Option).Include(x => x.Rating).ToList().Take(5);
            var productvms = new List<ProductVm>();

            foreach (var item in query)
            {
                var productvm = new ProductVm()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Title = item.Title,
                    Slug = item.Slug,
                    Description = item.Description,
                    Detail = item.Detail,
                    BrandId = item.BrandId,
                    Brand = item.Brand,
                    Category = item.Category,
                    CategoryId = item.CategoryId,
                    Price = item.Price,
                    Sale = item.Sale,
                    ThumbnailImageUrl = item.Thumbnail.FileName,
                    Rating = item.Rating
                };
                foreach (var productMedia in item.Images.Where(x => x.Media.MediaType == MediaType.Image))
                {
                    productvm.ProductImages.Add(new ProductMediaVm
                    {
                        Id = productMedia.Id,
                        MediaUrl = _mediaService.GetThumbnailUrl(productMedia.Media),
                        Media = productMedia.Media.FileName
                    });
                }
                productvm.ProductOptionVm = item.OptionValues.OrderBy(x => x.SortIndex).Select(x => new ProductOptionVm()
                {
                    Id = x.OptionId,
                    Name = x.Option.Name,   
                    DisplayType = x.DisplayType,
                    Values = JsonConvert.DeserializeObject<IList<ProductOptionValueVm>>(x.Value)
                }).ToList();

                productvms.Add(productvm);

            }
            return View(productvms);
        }
    }

}
