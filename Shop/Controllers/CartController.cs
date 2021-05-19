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
        private readonly ICartService _cartService;
        private readonly IWorkContext _workContext;
        private readonly IMediaService _mediaService;
        private readonly shopContext _context;
        //  private readonly IHttpContextAccessor _httpContextAccessor;
        public const string CARTKEY = "cart";
        public const string WISHKEY = "wishlist";
        public const string RECENTLYVIEWED = "recentlyviewed";
        public CartController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, shopContext context//, IHttpContextAccessor httpContextAccessor
            , IWorkContext workContext,
            ICartService cartService,
            IMediaService mediaService
            )
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _workContext = workContext;
            _context = context;
            _cartService = cartService;
            _mediaService = mediaService;
            //  _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(long productid, int quantity, string values)
        {
            var currentUser = await _workContext.GetCurrentUser();
            var result = await _cartService.AddToCart(currentUser.Id, productid, quantity, values);
            return Ok(result);
            //if (result)
            //{
            //    return RedirectToAction("AddToCartResult", new { productId = productid });
            //}
            //else
            //{
            //    return Ok(new { Error = true, Message = result });
            //}
        }
        [HttpGet]
        public async Task<CheckoutViewModel> GetCart()
        {
            var currentUser = await _workContext.GetCurrentUser();
            var query = await _cartService.GetActiveCart(currentUser.Id);
            if (query == null)
            {
                var CheckoutViewModel = new CheckoutViewModel();
                return CheckoutViewModel;
            }
            else
            {
                var CartitemVm = query.cartItems.Select(x => new CartItemVm
                {
                    CartId = x.CartId,
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Values = JsonConvert.DeserializeObject<List<OptionVariationVm>>(x.Values)
                }).ToList();

                foreach (var item in CartitemVm)
                {

                    var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail).Include(x => x.Brand).Include(x => x.Category)
                       .Include(x => x.OptionValues).ThenInclude(x => x.Option).Include(x => x.Rating)
                      .Where(p => p.Id == item.ProductId).FirstOrDefault();
                    item.ProductVm = new ProductVm()
                    {
                        Id = product.Id,
                        Code = product.Code,
                        Title = product.Title,
                        Slug = product.Slug,
                        Description = product.Description,
                        Detail = product.Detail,
                        Brand = product.Brand,
                        BrandId = product.BrandId,
                        Category = product.Category,
                        CategoryId = product.CategoryId,
                        Price = product.Price,
                        Sale = product.Sale,
                        ThumbnailImageUrl = product.Thumbnail.FileName,
                        Rating = product.Rating
                    };
                    var ListImage = product.Images;
                    foreach (var productMedia in product.Images.Where(x => x.Media.MediaType == MediaType.Image))
                    {
                        item.ProductVm.ProductImages.Add(new ProductMediaVm
                        {
                            Id = productMedia.Id,
                            MediaUrl = _mediaService.GetThumbnailUrl(productMedia.Media),
                            Media = productMedia.Media.FileName
                        });
                    }
                    item.ProductVm.ProductOptionVm = product.OptionValues.OrderBy(x => x.SortIndex).Select(x =>
                       new ProductOptionVm
                       {
                           Id = x.OptionId,
                           Name = x.Option.Name,
                           DisplayType = x.DisplayType,
                           Values = JsonConvert.DeserializeObject<IList<ProductOptionValueVm>>(x.Value)
                       }).ToList();
                    // neu co gia tri
                    foreach (var item2 in item.Values)
                    {
                        var OptionName = _context.ProductOption.Where(x => x.Id == item2.optionId).Select(x => x.Name).FirstOrDefault();
                        item2.optionName = OptionName;
                    };
                    // kiem tra xem neu khong co gia tri
                    if (item.Values.Count == 0)
                    {
                        if (product.OptionValues.Count > 0)
                        {
                            var value = product.OptionValues.Select(x => new OptionVariationVm
                            {
                                optionId = x.OptionId,
                                optionValues = JsonConvert.DeserializeObject<List<ProductOptionValueVm>>(x.Value).Select(x => x.Key).FirstOrDefault()
                            }).FirstOrDefault();
                            item.Values.Add(value);

                        }
                    }
                }

                var CheckoutViewModel = new CheckoutViewModel();
                CheckoutViewModel.CartItemVms = CartitemVm;
                return CheckoutViewModel;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var CheckoutViewModel = new CheckoutViewModel();
            CheckoutViewModel = await GetCart();
            var Viewed = GetViewed();
            CheckoutViewModel.Viewed = Viewed;
            return View(CheckoutViewModel);
            
        }

        [HttpPost]
        public async Task<IActionResult> Cart(long CartId)
        {
            var currentUser = await _workContext.GetCurrentUser();
            var query = _cartService.GetActiveCart(currentUser.Id);
            if (query == null)
            {
                return NotFound();
            }
            else
            {
                var CartitemVm = query.Result.cartItems.Select(x => new CartItemVm
                {
                    CartId = x.CartId,
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Values = JsonConvert.DeserializeObject<List<OptionVariationVm>>(x.Values)
                }).FirstOrDefault(x => x.Id == CartId);

                    var product = _context.Product.Include(x => x.Images).ThenInclude(x => x.Media).Include(x => x.Thumbnail).Include(x => x.Brand).Include(x => x.Category)
                       .Include(x => x.OptionValues).ThenInclude(x => x.Option).Include(x => x.Rating)
                      .Where(p => p.Id == CartitemVm.ProductId).FirstOrDefault();
                    CartitemVm.ProductVm = new ProductVm()
                    {
                        Id = product.Id,
                        Code = product.Code,
                        Title = product.Title,
                        Slug = product.Slug,
                        Description = product.Description,
                        Detail = product.Detail,
                        Brand = product.Brand,
                        BrandId = product.BrandId,
                        Category = product.Category,
                        CategoryId = product.CategoryId,
                        Price = product.Price,
                        Sale = product.Sale,
                        ThumbnailImageUrl = product.Thumbnail.FileName,
                        Rating = product.Rating
                    };
                    var ListImage = product.Images;
                    foreach (var productMedia in product.Images.Where(x => x.Media.MediaType == MediaType.Image))
                    {
                    CartitemVm.ProductVm.ProductImages.Add(new ProductMediaVm
                        {
                            Id = productMedia.Id,
                            MediaUrl = _mediaService.GetThumbnailUrl(productMedia.Media),
                            Media = productMedia.Media.FileName
                        });
                    }
                    CartitemVm.ProductVm.ProductOptionVm = product.OptionValues.OrderBy(x => x.SortIndex).Select(x =>
                       new ProductOptionVm
                       {
                           Id = x.OptionId,
                           Name = x.Option.Name,
                           DisplayType = x.DisplayType,
                           Values = JsonConvert.DeserializeObject<IList<ProductOptionValueVm>>(x.Value)
                       }).ToList();

                return Ok(CartitemVm);
            }
        }
        [HttpPost]
        public async Task<IActionResult> RemoveCart(long CartId)
        {
            var currentUser = await _workContext.GetCurrentUser();
            var cart = _context.Cart.Include(x => x.cartItems).FirstOrDefault(x => x.CustomerId == currentUser.Id);
            var cartitems = cart.cartItems.Where(x => x.Id == CartId).FirstOrDefault();
                cart.cartItems.Remove(cartitems);
                _context.SaveChanges();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart(long CartId, int quantity, string values)
        {
            var currentUser = await _workContext.GetCurrentUser();
            var cart = _context.Cart.Include(x => x.cartItems).FirstOrDefault(x => x.CustomerId == currentUser.Id);
            var cartitem = cart.cartItems.Where(x => x.Id == CartId).FirstOrDefault();
            var cartitems = cart.cartItems.Where(x => x.ProductId == cartitem.ProductId).ToList();
            if (values == null)
            {
                cartitem.Quantity = quantity;
            }
            else
            {
               var item =  cartitems.Find(x => x.Values == values);
                if (item != null)
                {
                    item.Quantity += quantity;
                    cart.cartItems.Remove(cartitem);  
                }
                else
                {
                    cartitem.Values = values;
                }     
            }
            _context.SaveChanges();
            //   var optionvalue = JsonConvert.DeserializeObject<List<OptionVariationVm>>(values);
            return Ok();
        }
        [HttpGet]
        public List<Product> GetViewed()
        {
            var cookieValueFromReq = Get(RECENTLYVIEWED);
            if (cookieValueFromReq != null)
            {
                var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);
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
        public string Get(string key)
        {
            return Request.Cookies[key];
        }

        List<long> GetViewedItems()
        {
            var cookieValueFromReq = Get(RECENTLYVIEWED);
            if (cookieValueFromReq != null)
            {
                var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);

                return jsonConvert;
            }
            return new List<long>();
        }
        [HttpPost]
        public IActionResult RecentlyViewed(long productid)
        {
            var product = GetViewed();
            if (product == null)
            {
                return NotFound("Không có sản phẩm yêu thích");
            }
            var count = 0;
            var Viewed = GetViewedItems();
            foreach (var item in Viewed)
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
                Viewed.Add(productid);
            }
            // convert sang json
            var jsonCart = JsonConvert.SerializeObject(Viewed, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        // luu vao cookies
        SaveCookies(RECENTLYVIEWED, jsonCart, 30);

            return Ok();
        }
        public void ClearCookies(string Key)
        {
            Response.Cookies.Delete(Key);
        }
        public void SaveCookies(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append(key, value, option);
        }
        [HttpGet]
        [Route("/wishlist")]
         public IActionResult WishList()
            {
                return View(GetWishList());
            }
        [HttpGet]
        public List<Product> GetWishList()
          {
              var cookieValueFromReq = Get(WISHKEY);
              if (cookieValueFromReq != null)
              {

                  var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);
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
               var cookieValueFromReq = Get(WISHKEY);
               if (cookieValueFromReq != null)
               {
                   var jsonConvert = JsonConvert.DeserializeObject<List<long>>(cookieValueFromReq);

                   return jsonConvert;
               }
               return new List<long>();
           }
        [HttpPost]
        public IActionResult AddtoWishList(long productid)
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
        SaveCookies(WISHKEY, jsonCart, 30);

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
            SaveCookies(WISHKEY, jsonCart, 30);
              return RedirectToAction("Cart", "Cart");
          }
        [HttpGet]
        [Route("/checkout")]
       public async Task<IActionResult> CheckOut()
       {
            // Xử lý khi đặt hàng
           var CheckoutViewModel = await GetCart();
           var Viewed = GetViewed();
           CheckoutViewModel.Viewed = Viewed;
         //  ViewData["cart"] = cart;
           return View(CheckoutViewModel);
       }

        [HttpPost]
        [Route("/checkout")]
        public async Task<IActionResult> CheckOut(CheckoutViewModel model)
        {
            var CheckoutViewModel = await GetCart();
            var Viewed = GetViewed();
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
                foreach (var item in CheckoutViewModel.CartItemVms)
                {
                    var OrderDetail = new OrderDetail()
                    {
                        ProductId = item.ProductVm.Id,
                        Quantity = item.Quantity,
                        Price = item.ProductVm.Price,
                        Values = JsonConvert.SerializeObject(item.Values)
                    };

                    OrderDetails.Add(OrderDetail);
                }
                Order.OrderDetails = OrderDetails;
                _context.Orders.Add(Order);
                var CartItems = new List<Model.Models.CartItem>();
                foreach (var item in CheckoutViewModel.CartItemVms)
                {
                    var CartItem = _context.CartItem.FirstOrDefault(x => x.Id == item.Id);
                 
                    CartItems.Add(CartItem);
                }
               
                _context.CartItem.RemoveRange(CartItems);
                _context.SaveChanges();
                return RedirectToAction("checkoutSuccess");
            }
            model.Viewed = Viewed;
            model.CartItemVms = CheckoutViewModel.CartItemVms;
            return View(model);
        }
        public IActionResult checkoutSuccess()
        {
            return View();
        }
    }


}
