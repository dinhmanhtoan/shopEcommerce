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
        public const string WISHKEY = "wishlist";
        public CartController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, shopContext context//, IHttpContextAccessor httpContextAccessor
            )
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _context = context;
          //  _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
       public List<CartItem> GetListProduct()
        {
            //read cookie from IHttpContextAccessor  
            // var cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[CARTKEY];
            //read cookie from Request object
            var cookieValueFromReq = Get(CARTKEY);
            if (cookieValueFromReq != null)
            {
               
                var jsonConvert = JsonConvert.DeserializeObject<List<Cart>>(cookieValueFromReq);
               
                //var ListId = jsonConvert.Select(x => x.Id);
                //var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
                //   .Where(p => ListId.Contains(p.Id)).ToList();
                var CartItem = new List<CartItem>();
                foreach (var item in jsonConvert)
                {
                    var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
                     .Where(p => p.Id == item.Id).FirstOrDefault();
                    CartItem.Add(new CartItem { Id = item.Id, quantity = item.quantity, product = product });
                };
                return CartItem;
            }
         
            return new List<CartItem>();
        }
        [HttpGet]
        public List<Product> GetWishList()
        {
            //read cookie from IHttpContextAccessor  
            // var cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[CARTKEY];
            //read cookie from Request object
            var cookieValueFromReq = Get(WISHKEY);
            if (cookieValueFromReq != null)
            {

                var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);

                //var ListId = jsonConvert.Select(x => x.Id);
                //var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
                //   .Where(p => ListId.Contains(p.Id)).ToList();
                var ListWishList = new List<Product>();
                foreach (var item in jsonConvert)
                {
                    var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
                     .Where(p => p.Id == item).FirstOrDefault();
                    ListWishList.Add(product);
                };
                return ListWishList;
            }

            return new List<Product>();
        }
        List<long> GetWishListItems()
        {
            //read cookie from IHttpContextAccessor  
            // var cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[CARTKEY];
            //read cookie from Request object
            var cookieValueFromReq = Get(WISHKEY);
            if (cookieValueFromReq != null)
            {
                var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);

                return jsonConvert;
            }
            return new List<long>();
        }
        List<Cart> GetCartItems()
        {
            //read cookie from IHttpContextAccessor  
            // var cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[CARTKEY];
            //read cookie from Request object
            var cookieValueFromReq = Get(CARTKEY);
            if (cookieValueFromReq != null)
            {
                var jsonConvert = JsonConvert.DeserializeObject<List<Cart>>(cookieValueFromReq);
         
                return jsonConvert;
            }
            return new List<Cart>();
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
        public IActionResult AddToCart(long productid, long quantity )
        {
            var product = GetListProduct();
            if (product == null)
            {
                return NotFound("Không có sản phẩm");
            }
           
            var cart = GetCartItems();
            var cartItem = cart.Find(p => p.Id == productid);
            if (cartItem != null)
            {

                if (quantity != 0)
                {
                    cartItem.quantity = quantity;
                }
                else
                {
                    cartItem.quantity++;
                }
          
            }

            else
            {
                if (quantity == 0)
                {
                    cart.Add(new CartItem() { quantity = 1, Id = productid });
                }
                else
                {
                    cart.Add(new CartItem() { quantity = quantity, Id = productid });
                }
                //  Thêm mới
               
            }
             // convert sang json
             var jsonCart =   JsonConvert.SerializeObject(cart, new JsonSerializerSettings()
             {
                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
             });
            // luu vao cookies
            SaveCart(CARTKEY, jsonCart, 30);

            return Ok();
        }
      [HttpPost]
        public IActionResult AddtoWishList(long productid)
        {
            var product = GetListProduct();
            if (product == null)
            {
                return NotFound("Không có sản phẩm yêu thích");
            }
            var count = 0;
            var wishList = GetWishListItems();
            foreach (var item in wishList)
            {
                if (item == productid)
                {
                    count++;
                }

            }
            if (count > 0)
            {
                return Ok();
            }

            else
            {
                wishList.Add(productid);
            }

            // convert sang json
            var jsonCart =   JsonConvert.SerializeObject(wishList, new JsonSerializerSettings()
             {
                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
             });
            // luu vao cookies
            SaveCart(WISHKEY, jsonCart, 30);

            return Ok();
        }
        [HttpPost]
        public IActionResult RemoveWishList(long productid)
        {
            var count = 0;
            var wishList = GetWishListItems();
            foreach (var item in wishList)
            {
                if (item == productid)
                {
                    count++;
                }

            }
            if (count > 0)
            {
                // Đã tồn tại, tăng thêm 1
                wishList.Remove(productid);
            }
            var jsonCart = JsonConvert.SerializeObject(wishList);
            // luu vao cookies
            SaveCart(WISHKEY, jsonCart, 30);
            return RedirectToAction("Cart", "Cart");
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
            return View(GetListProduct());
        }
        [HttpGet]
        [Route("/wishlist")]
        public IActionResult WishList()
        {
            return View(GetWishList());
        }
        //[Route("/checkout")]
        //public IActionResult CheckOut()
        //{

        //    // Xử lý khi đặt hàng
        //    var cart = GetCartItems();
        //    ViewData["email"] = email;
        //    ViewData["address"] = address;
        //    ViewData["cart"] = cart;

        //    if (!string.IsNullOrEmpty(email))
        //    {
        //        // hãy tạo cấu trúc db lưu lại đơn hàng và xóa cart khỏi session
        //        ClearCart();
        //        RedirectToAction("Index","Home");
        //    }
        //    return View();
        //}
        [HttpGet]
        [Route("/checkout")]
        public IActionResult CheckOut()
        {
            // Xử lý khi đặt hàng
            var cart = GetListProduct();
            var CheckoutViewModel = new CheckoutViewModel();
            CheckoutViewModel.CartItems = cart;
          //  ViewData["cart"] = cart;
            return View(CheckoutViewModel);
        }

        [HttpPost]
        [Route("/checkout")]
        public IActionResult CheckOut(CheckoutViewModel model)
        {
            var cart = GetListProduct();
            if (ModelState.IsValid)
            {
       
                var Order = new Order()
                {
                    CreateOn = DateTime.Now,
                    FullName = model.CheckoutModel.FirstName + model.CheckoutModel.LastName,
                    Email = model.CheckoutModel.Email,
                    PhoneNumber = model.CheckoutModel.PhoneNumber,
                    AddressLine1 = model.CheckoutModel.AddressLine1,
                    AddressLine2 = model.CheckoutModel.AddressLine2,
                    Node = model.CheckoutModel.Note,
                    Status = 1,
                };
                var OrderDetails = new List<OrderDetail>();
                foreach (var item in cart)
                {
                    var OrderDetail = new OrderDetail()
                    {
                        ProductId = item.product.Id,
                        Quantity = item.quantity,
                        Price = item.product.Price
                    };

                    OrderDetails.Add(OrderDetail);
                }
                Order.OrderDetails = OrderDetails;
                _context.Orders.Add(Order);
                _context.SaveChanges();
                ClearCart();
                return RedirectToAction("Index", "Product");
            }
            model.CartItems = cart;
            return View(model);

        }
    }
}
