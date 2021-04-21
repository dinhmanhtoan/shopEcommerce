using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Models;
using Model.Services;
using Model.ViewModel;
using Model.ViewModel.Search;
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
        private readonly shopContext _context;
        public ProductController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, shopContext context)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _context = context;
        }
        //[HttpPost]
        //public async Task<IActionResult> Index(Search search)
        //{
        //    var query = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media)
        //                                       .Include(x => x.Thumbnail).AsQueryable();
        //    if (search.CategoryId != 0 )
        //    {
        //        long categoryId = search.CategoryId;
        //        query = query.Where(x => x.CategoryId == categoryId);
        //    }

        //    //if (search.BranId != null)
        //    //{
        //    //    long BranId = search.BranId;
        //    //    query = query.Where(x => x.BranId == BranId);
        //    //}
        //    if (search.Slug != null)
        //    {
        //        string Slug = search.Slug;
        //        query = query.Where(x => x.Slug.Contains(Slug));
        //    }
        //    if (search.Sort != 0)
        //    {
        //        int Sort = search.Sort;
        //        if (Sort !=  -1)
        //        {
        //            query = query.Take(Sort);
        //        }
        //    }
        //    if (search.PriceStart != 0)
        //    {
        //        decimal PriceStart = search.PriceStart;
        //        query = query.Where(x => x.Price >= PriceStart);
        //    }
        //    if (search.PriceEnd != 0)
        //    {
        //        decimal PriceEnd = search.PriceEnd;
        //        query = query.Where(x => x.Price <= PriceEnd);
        //    }
        //    var category = await _categoryService.GetAll();
        //    var ProductList = new ProductList();
        //    foreach (var item in query)
        //    {
        //        var ProductThumbnail = new ProductThumbnail()
        //        {
        //            Id = item.Id,
        //            Code = item.Code,
        //            Title = item.Title,
        //            Description = item.Description,
        //            ThumbnailImage = _context.Medias.Where(x => x.Id == item.ThumbnailId).FirstOrDefault(),
        //            Detail = item.Detail,
        //            Price = item.Price,
        //            Sale = item.Sale,
        //        };
        //        ProductList.products.Add(ProductThumbnail);
        //    }
        //    ProductList.categories = category;
        
        //    return View(ProductList);
        //}
        [HttpGet]
        public async Task<IActionResult> Index(Search search)
        {
            var query = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media)
                                        .Include(x => x.Thumbnail).AsQueryable();
            if(search.Category != null)
            {
                query = query.Where(x => x.CategoryId == search.Category);
            }
            if(!string.IsNullOrEmpty(search.Sort))
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

            query = query.Skip(search.PageNumber - 1).Take(search.PageSize);
            var category = await _categoryService.GetAll();
            var ProductList = new ProductList();
            foreach (var item in query)
            {
                var ProductThumbnail = new ProductThumbnail()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Title = item.Title,
                    Description = item.Description,
                    ThumbnailImage = _context.Medias.Where(x => x.Id == item.ThumbnailId).FirstOrDefault(),
                    Detail = item.Detail,
                    Price = item.Price,
                    Sale = item.Sale,
                };
                ProductList.products.Add(ProductThumbnail);
            }
            ProductList.categories = category;

            return View(ProductList);
        }
    }
}
