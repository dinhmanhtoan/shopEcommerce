using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Models;
using Model.Services;
using Model.ViewModel;
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Product = await _productService.GetAll();
            Product.Take(5).ToList();
            var category = await _categoryService.GetAll();
            var ProductList = new ProductList();
            foreach (var item in Product)
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
        [HttpGet("{Id}")]
        public async Task<IActionResult> Index(long Id)
        {
            var Product = await _productService.GetByCategory(Id);
            var ProductList = new ProductList();
            foreach (var item in Product)
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
            var category = await _categoryService.GetAll();
            return View(ProductList);
        }
    }
}
