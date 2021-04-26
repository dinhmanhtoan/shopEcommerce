using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Services;
using Shop.Models;
using Model.ViewModel;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Model.Models;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly shopContext _context;
        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, shopContext context)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _context = context;
        }

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

            return View(ProductList);
        }
        //[HttpGet("{Id}")]
        //public async Task<IActionResult> Details(long Id)
        //{

        //    return View();
        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
