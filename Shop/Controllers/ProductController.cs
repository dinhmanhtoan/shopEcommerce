using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Models;
using Model.Services;
using Model.ViewModel;
using Model.ViewModel.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly shopContext _context;
        public ProductController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, shopContext context, IBrandService brandService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _context = context;
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Search search)
        {
            var query = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media)
                                        .Include(x => x.Thumbnail).AsQueryable();
            if (search.Category != null)
            {
                query = query.Where(x => x.CategoryId == search.Category);
            }
            if (search.Brand != null)
            {
                query = query.Where(x => x.BrandId == search.Brand);
            }
            if (!string.IsNullOrEmpty(search.Sort))
            {
                switch (search.Sort)
                {
                    case "title-asc":
                        query = query.OrderBy(x => x.Title);
                        break;
                    case "title-desc":
                        query = query.OrderByDescending(x => x.Title);
                        break;
                    case "price-asc":
                        query = query.OrderBy(x => x.Price);
                        break;
                    case "price-desc":
                        query = query.OrderByDescending(x => x.Price);
                        break;
                }
            }

            query = query.Skip(search.PageNumber * (search.PageNumber - 1)).Take(search.PageSize);
            var category = await _categoryService.GetAll();
            var brand = await _brandService.GetAll();
            var ProductList = new ProductList();
            foreach (var item in query)
            {
                var ProductThumbnail = new ProductThumbnail()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Title = item.Title,
                    Slug = item.Slug,
                    Description = item.Description,
                    ThumbnailImage = _context.Medias.Where(x => x.Id == item.ThumbnailId).FirstOrDefault(),
                    Detail = item.Detail,
                    Price = item.Price,
                    Sale = item.Sale,
                };
                ProductList.products.Add(ProductThumbnail);
            }
            ProductList.categories = category;
            ProductList.Brand = brand;

            return View(ProductList);
        }

        [HttpGet("chi-tiet/{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            var query = _context.Product.Include(x => x.OptionValues).ThenInclude(x=> x.Option).Where(x => x.Slug == slug)
                .Include(x=> x.Brand)
                .Include(x => x.Category)
                .Include(x => x.Images).ThenInclude(x => x.Media)
                .Include(x => x.Thumbnail)
                .Include(x => x.Rating)
                    
                .First();
            var like = _context.Product.Where(x => x.CategoryId == query.CategoryId).Include(x => x.Thumbnail).ToList();
            var DetailsVm = new DetailsVm();
            DetailsVm.product = query;
            DetailsVm.products = like;
            DetailsVm.ProductOptionVm = query.OptionValues.OrderBy(x => x.SortIndex).Select(x =>
         new ProductOptionVm
         {
             Id = x.OptionId,
             Name = x.Option.Name,
             DisplayType = x.DisplayType,
             Values = JsonConvert.DeserializeObject<IList<ProductOptionValueVm>>(x.Value)
         }).ToList();
            return View(DetailsVm);
        }
       
    }
}
