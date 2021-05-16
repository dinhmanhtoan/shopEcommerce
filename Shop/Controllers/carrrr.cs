//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Model.Models;
//using Model.Services;
//using Model.ViewModel;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Shop.Controllers
//{
//    public class CartController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;
//        private readonly IProductService _productService;
//        private readonly ICategoryService _categoryService;
//        private readonly shopContext _context;
//      //  private readonly IHttpContextAccessor _httpContextAccessor;
//        public const string CARTKEY = "cart";
//        public const string WISHKEY = "wishlist";
//        public const string RECENTLYVIEWED = "recentlyviewed";
//        public CartController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, shopContext context//, IHttpContextAccessor httpContextAccessor
//            )
//        {
//            _logger = logger;
//            _productService = productService;
//            _categoryService = categoryService;
//            _context = context;
//          //  _httpContextAccessor = httpContextAccessor;
//        }
//        [HttpGet]
//       public List<CartItem> GetListProduct()
//        {
//            //read cookie from IHttpContextAccessor  
//            // var cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[CARTKEY];
//            //read cookie from Request object
//            var cookieValueFromReq = Get(CARTKEY);
//            if (cookieValueFromReq != null)
//            {
               
//                var jsonConvert = JsonConvert.DeserializeObject<List<Cart>>(cookieValueFromReq);
               
//                //var ListId = jsonConvert.Select(x => x.Id);
//                //var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
//                //   .Where(p => ListId.Contains(p.Id)).ToList();
//                var CartItem = new List<CartItem>();
//                foreach (var item in jsonConvert)
//                {
//                    var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
//                        .Include(x => x.OptionValues).ThenInclude(x => x.Option)
//                     .Where(p => p.Id == item.Id).FirstOrDefault();
//                    if (item.Size.Count() > 0)
//                    {
//                        product.OptionValues.Where(x => x.Value.Contains(item.Size));
//                    }
//                    if (item.Color.Count() > 0)
//                    {
//                        product.OptionValues.Where(x => x.Value.Contains(item.Color));
//                    }
//                    var productvm = new ProductVm()
//                    {
//                        Id = product.Id,
//                        Code = product.Code,
//                        Title = product.Title,
//                        Slug = product.Slug,
//                        Description = product.Description,
//                        Detail = product.Detail,
//                        BrandId = product.BrandId,
//                        CategoryId = product.CategoryId,
//                        Price = product.Price,
//                        Sale = product.Sale,
//                        ThumbnailImageUrl = product.Thumbnail.FileName
//                    };


//                    productvm.Options = product.OptionValues.OrderBy(x => x.SortIndex).Select(x => new ProductOptionVm()
//                    {
//                        Id = x.OptionId,
//                        Name = x.Option.Name,
//                        DisplayType = x.DisplayType,
//                        Values = JsonConvert.DeserializeObject<IList<ProductOptionValueVm>>(x.Value)
//                    }).ToList();



//                    CartItem.Add(new CartItem { Id = item.Id, quantity = item.quantity, product = productvm , Size = item.Size, Color = item.Color });
//                };
//                return CartItem;
//            }
         
//            return new List<CartItem>();
//        }
//        [HttpGet]
//        public List<Product> GetWishList()
//        {
//            //read cookie from IHttpContextAccessor  
//            // var cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[CARTKEY];
//            //read cookie from Request object
//            var cookieValueFromReq = Get(WISHKEY);
//            if (cookieValueFromReq != null)
//            {

//                var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);

//                //var ListId = jsonConvert.Select(x => x.Id);
//                //var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
//                //   .Where(p => ListId.Contains(p.Id)).ToList();
//                var ListWishList = new List<Product>();
//                foreach (var item in jsonConvert)
//                {
//                    var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
//                     .Where(p => p.Id == item).FirstOrDefault();
//                    ListWishList.Add(product);
//                };
//                return ListWishList;
//            }

//            return new List<Product>();
//        }
//        [HttpGet]
//        public List<Product> GetViewed()
//        {
//            //read cookie from IHttpContextAccessor  
//            // var cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[CARTKEY];
//            //read cookie from Request object
//            var cookieValueFromReq = Get(RECENTLYVIEWED);
//            if (cookieValueFromReq != null)
//            {

//                var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);

//                //var ListId = jsonConvert.Select(x => x.Id);
//                //var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
//                //   .Where(p => ListId.Contains(p.Id)).ToList();
//                var ListWishList = new List<Product>();
//                foreach (var item in jsonConvert)
//                {
//                    var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail)
//                     .Where(p => p.Id == item).FirstOrDefault();
//                    ListWishList.Add(product);
//                };
//                return ListWishList;
//            }

//            return new List<Product>();
//        }
//        List<long> GetViewedItems()
//        {
//            //read cookie from IHttpContextAccessor  
//            // var cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[CARTKEY];
//            //read cookie from Request object
//            var cookieValueFromReq = Get(RECENTLYVIEWED);
//            if (cookieValueFromReq != null)
//            {
//                var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);

//                return jsonConvert;
//            }
//            return new List<long>();
//        }
//        List<long> GetWishListItems()
//        {
//            //read cookie from IHttpContextAccessor  
//            // var cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[CARTKEY];
//            //read cookie from Request object
//            var cookieValueFromReq = Get(WISHKEY);
//            if (cookieValueFromReq != null)
//            {
//                var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);

//                return jsonConvert;
//            }
//            return new List<long>();
//        }
//        List<Cart> GetCartItems()
//        {
//            var cookieValueFromReq = Get(CARTKEY);
//            if (cookieValueFromReq != null)
//            {
//                var jsonConvert = JsonConvert.DeserializeObject<List<Cart>>(cookieValueFromReq);
         
//                return jsonConvert;
//            }
//            return new List<Cart>();
//        }
//        public void ClearCart()
//        {
//            Response.Cookies.Delete(CARTKEY);
//        }
//        public void SaveCart(string key, string value, int? expireTime)
//        {
//            CookieOptions option = new CookieOptions();
//            if (expireTime.HasValue)
//                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
//            else
//                option.Expires = DateTime.Now.AddMinutes(10);
//            Response.Cookies.Append(key, value, option);
//        }
//        public string Get(string key)
//        {
//            return Request.Cookies[key];
//        }
//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }
//        [HttpPost]
//        public IActionResult AddToCart(long productid, long quantity ,string Size = "" ,string Color = "")
//        {
//            var product = GetListProduct();
//            if (product == null)
//            {
//                return NotFound("Không có sản phẩm");
//            }
//          var cart = GetCartItems();
//            var ListColor = new List<string>();
//            var ListSize = new List<string>();
//            foreach (var item in product)
//            {
//                if (item.product.Options != null)
//                {
//                    foreach (var item2 in item.product.Options)
//                    {

//                        if (item2.Name == "Color")
//                        {
//                            ListColor = item2.Values.Select(x => x.Key).ToList();
//                        }
//                        else
//                        {
//                            ListSize = item2.Values.Select(x => x.Key).ToList();
//                        }
//                    }
//                }
           
//            }
//            var sizeItem = "";
//            var colorItem = "";
//            if (ListColor != null)
//            {
//                if (Size != "")
//                {
//                    sizeItem += ListSize.Find(x => x == Size);
//                }
//                else
//                {
//                    sizeItem += ListSize.FirstOrDefault();
//                }
//            }
//            if (ListColor != null)
//            {
//                if (Color != "")
//                {

//                    colorItem += ListColor.Find(x => x == Color);
//                }
//                else
//                {
//                    colorItem += ListColor.FirstOrDefault();
//                }
//            }
//             var cartItem = cart.Find(p => p.Id == productid);
//            if (cartItem != null)
//            {
//                if (cartItem.Size == sizeItem && cartItem.Color == colorItem)
//                {
//                    if (quantity != 0)
//                    {
//                        cartItem.quantity = quantity;
//                    }
//                    else
//                    {
//                        cartItem.quantity++;
//                    }
//                }
//                else
//                {
//                    if (quantity != 0)
//                    {
//                        cartItem.quantity = quantity;
//                    }
//                    else
//                    {
//                        cartItem.quantity++;
//                    }
//                }
//            }
//            else
//            {
//                if (quantity == 0)
//                {
//                    cart.Add(new CartItem() { quantity = 1, Id = productid , Size = sizeItem, Color = colorItem});
//                }
//                else
//                {
//                    cart.Add(new CartItem() { quantity = quantity, Id = productid, Size = sizeItem, Color = colorItem });
//                }
//                //  Thêm mới
               
//            }
//             // convert sang json
//             var jsonCart = JsonConvert.SerializeObject(cart, new JsonSerializerSettings()
//             {
//                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//             });
//            // luu vao cookies
//            SaveCart(CARTKEY, jsonCart, 30);

//            return Ok();
//        }
//      [HttpPost]
//        public IActionResult AddtoWishList(long productid)
//        {
//            var product = GetListProduct();
//            if (product == null)
//            {
//                return NotFound("Không có sản phẩm yêu thích");
//            }
//            var count = 0;
//            var wishList = GetWishListItems();
//            foreach (var item in wishList)
//            {
//                if (item == productid)
//                {
//                    count++;
//                }

//            }
//            if (count > 0)
//            {
//                return Ok();
//            }

//            else
//            {
//                wishList.Add(productid);
//            }

//            // convert sang json
//            var jsonCart =   JsonConvert.SerializeObject(wishList, new JsonSerializerSettings()
//             {
//                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//             });
//            // luu vao cookies
//            SaveCart(WISHKEY, jsonCart, 30);

//            return Ok();
//        }
//        [HttpPost]
//        public IActionResult RecentlyViewed(long productid)
//        {
//            var product = GetViewed();
//            if (product == null)
//            {
//                return NotFound("Không có sản phẩm yêu thích");
//            }
//            var count = 0;
//            var Viewed = GetViewedItems();
//            foreach (var item in Viewed)
//            {
//                if (item == productid)
//                {
//                    count++;
//                }

//            }
//            if (count > 0)
//            {
//                return Ok();
//            }

//            else
//            {
//                Viewed.Add(productid);
//            }
//            // convert sang json
//            var jsonCart = JsonConvert.SerializeObject(Viewed, new JsonSerializerSettings()
//            {
//                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//            });
//            // luu vao cookies
//            SaveCart(RECENTLYVIEWED, jsonCart, 30);

//            return Ok();
//        }
//        [HttpPost]
//        public IActionResult RemoveWishList(long productid)
//        {
//            var count = 0;
//            var wishList = GetWishListItems();
//            foreach (var item in wishList)
//            {
//                if (item == productid)
//                {
//                    count++;
//                }

//            }
//            if (count > 0)
//            {
//                // Đã tồn tại, tăng thêm 1
//                wishList.Remove(productid);
//            }
//            var jsonCart = JsonConvert.SerializeObject(wishList);
//            // luu vao cookies
//            SaveCart(WISHKEY, jsonCart, 30);
//            return RedirectToAction("Cart", "Cart");
//        }

//        [HttpPost]
//        public IActionResult RemoveCart(long productid)
//        {
//            var cart = GetCartItems();
//            var cartItem = cart.Find(p => p.Id == productid);
//            if (cartItem != null)
//            {
//                // Đã tồn tại, tăng thêm 1
//                cart.Remove(cartItem);
//            }
//            var jsonCart = JsonConvert.SerializeObject(cart);
//            // luu vao cookies
//            SaveCart(CARTKEY, jsonCart, 30);
//            return RedirectToAction("Cart", "Cart");
//        }
//        [HttpPost]
//        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity, string Size = "", string Color = "")
//        {
//            // Cập nhật Cart thay đổi số lượng quantity ...
//            var cart = GetCartItems();
//            var cartItem = cart.Find(p => p.Id == productid);
//            if (cartItem != null)
//            {
//                if (Size != "")
//                {
//                    if (cartItem.Size != Size)
//                    {
//                        cartItem.Size = Size;
//                    }
//                }
//                else
//                {
//                    // Đã tồn tại, tăng thêm 1
//                    cartItem.quantity = quantity;
//                }

              
//            }
//            var jsonCart = JsonConvert.SerializeObject(cart);
//            // luu vao cookies
//            SaveCart(CARTKEY, jsonCart, 30);
//            return Ok();
//        }
//        [HttpGet]
//        public IActionResult Cart()
//        {
//            var cart = GetListProduct();
//            var Viewed = GetViewed();
//            var CheckoutViewModel = new CheckoutViewModel();
//            CheckoutViewModel.CartItems = cart;
//            CheckoutViewModel.Viewed = Viewed;
//            return View(CheckoutViewModel);
//        }
//        [HttpGet]
//        [Route("/wishlist")]
//        public IActionResult WishList()
//        {
//            return View(GetWishList());
//        }
//        //[Route("/checkout")]
//        //public IActionResult CheckOut()
//        //{

//        //    // Xử lý khi đặt hàng
//        //    var cart = GetCartItems();
//        //    ViewData["email"] = email;
//        //    ViewData["address"] = address;
//        //    ViewData["cart"] = cart;

//        //    if (!string.IsNullOrEmpty(email))
//        //    {
//        //        // hãy tạo cấu trúc db lưu lại đơn hàng và xóa cart khỏi session
//        //        ClearCart();
//        //        RedirectToAction("Index","Home");
//        //    }
//        //    return View();
//        //}
//        [HttpGet]
//        [Route("/checkout")]
//        public IActionResult CheckOut()
//        {
//            // Xử lý khi đặt hàng
//            var cart = GetListProduct();
//            var Viewed = GetViewed();
//            var CheckoutViewModel = new CheckoutViewModel();
//            CheckoutViewModel.CartItems = cart;
//            CheckoutViewModel.Viewed = Viewed;
//          //  ViewData["cart"] = cart;
//            return View(CheckoutViewModel);
//        }

//        [HttpPost]
//        [Route("/checkout")]
//        public IActionResult CheckOut(CheckoutViewModel model)
//        {
//            var cart = GetListProduct();
//            var Viewed = GetViewed();
//            if (ModelState.IsValid)
//            {
//                var Order = new Order()
//                {
//                    CreateOn = DateTime.Now,
//                    FullName = model.CheckoutModel.FirstName + model.CheckoutModel.LastName,
//                    Email = model.CheckoutModel.Email,
//                    PhoneNumber = model.CheckoutModel.PhoneNumber,
//                    AddressLine1 = model.CheckoutModel.AddressLine1,
//                    AddressLine2 = model.CheckoutModel.AddressLine2,
//                    Node = model.CheckoutModel.Note,
//                    Status = 1,
//                };
//                var OrderDetails = new List<OrderDetail>();
//                foreach (var item in cart)
//                {
//                    var OrderDetail = new OrderDetail()
//                    {
//                        ProductId = item.product.Id,
//                        Quantity = item.quantity,
//                        Price = item.product.Price
//                    };

//                    OrderDetails.Add(OrderDetail);
//                }
//                Order.OrderDetails = OrderDetails;
//                _context.Orders.Add(Order);
//                _context.SaveChanges();
//                ClearCart();
//                return RedirectToAction("checkoutSuccess");
//            }
//            model.CartItems = cart;
//            model.Viewed = Viewed;
//            return View(model);
//        }
//        public IActionResult checkoutSuccess()
//        {
//            return View();
//        }
//    }
//}
