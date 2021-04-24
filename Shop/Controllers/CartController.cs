using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Models;
using Model.Services;
using Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly shopContext _context;
      //  private readonly IHttpContextAccessor _httpContextAccessor;
        public const string CARTKEY = "cart";
        public CartController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, shopContext context//, IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _context = context;
          //  _httpContextAccessor = httpContextAccessor;
        }
        List<Product> GetCartItems()
        {
            //read cookie from IHttpContextAccessor  
            // var cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[CARTKEY];
            //read cookie from Request object
            var cookieValueFromReq = Get(CARTKEY);
            if (cookieValueFromReq != null)
            {
               
                var jsonConvert = JsonConvert.DeserializeObject<List<CartItem>>(cookieValueFromReq);
                var ListId = jsonConvert.Select(x => x.Id);
                var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
                   .Where(p => ListId.Contains(p.Id)).ToList();
                   
                return product;
            }
            return new List<Product>();
        }
       public void ClearCart()
        {
            Response.Cookies.Delete(CARTKEY);
        }
        public void SaveCart(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append(key, value, option);
        }
        public string Get(string key)
        {
            return Request.Cookies[key];
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddToCart(long productid)
        {
            var product = _context.Product.Include(x => x.Images).ThenInclude(x=> x.Media).Include(x => x.Thumbnail)
                     .Where(p => p.Id == productid)
                     .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");
            var cart = GetCartItems();
            var cartItem = cart.Find(p => p.Id == productid);
            if (cartItem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartItem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = 1, Id = product.Id  });
            }
             // convert sang json
             var jsonCart =   JsonConvert.SerializeObject(cart, new JsonSerializerSettings()
             {
                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
             });
            // luu vao cookies
            SaveCart(CARTKEY, jsonCart, 30);

            return Json(jsonCart);
        }
        [HttpPost]
        public IActionResult RemoveCart(long productid)
        {
            var cart = GetCartItems();
            var cartItem = cart.Find(p => p.Id == productid);
            if (cartItem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartItem);
            }
            var jsonCart = JsonConvert.SerializeObject(cart);
            // luu vao cookies
            SaveCart(CARTKEY, jsonCart, 30);
            return RedirectToAction("Cart", "Cart");
        }
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartItem = cart.Find(p => p.Id == productid);
            if (cartItem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartItem.quantity = quantity;
            }
            var jsonCart = JsonConvert.SerializeObject(cart);
            // luu vao cookies
            SaveCart(CARTKEY, jsonCart, 30);
            return Ok();
        }
        [HttpGet]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }
        [Route("/checkout")]
        public IActionResult CheckOut([FromForm] string email, [FromForm] string address)
        {

            // Xử lý khi đặt hàng
            var cart = GetCartItems();
            ViewData["email"] = email;
            ViewData["address"] = address;
            ViewData["cart"] = cart;

            if (!string.IsNullOrEmpty(email))
            {
                // hãy tạo cấu trúc db lưu lại đơn hàng và xóa cart khỏi session

                ClearCart();
                RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}
