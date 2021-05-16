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
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

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
            var query = _context.Product.Where(x => x.IsFuture == true).Include(x => x.Thumbnail).Include(x => x.OptionValues).ThenInclude(x => x.Option).ToList();
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
                    Price = item.Price,
                    Sale = item.Sale,
                    ThumbnailImageUrl = item.Thumbnail.FileName,
                };
                productvm.Options = item.OptionValues.OrderBy(x => x.SortIndex).Select(x => new ProductOptionVm()
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
