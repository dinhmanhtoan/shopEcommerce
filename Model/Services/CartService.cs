using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.ViewModel;
using Newtonsoft.Json;
using Model.Models;

namespace Model.Services
{
    public class CartService : ICartService
    {
        private readonly shopContext _context;
        public CartService(shopContext context)
        {
            _context = context;
        }
        public async Task<bool> AddToCart(long customerId, long productId, int quantity, string values)
        {
            return await AddToCart(customerId, customerId, productId, quantity, values);
        }

        public async Task<bool> AddToCart(long customerId, long createdById, long productId , int quantity, string values)
        {
            if (quantity == 0)
            {
                quantity = 1;
            }
            var cart = await GetActiveCart(customerId, createdById);
            if (cart == null)
            {
                cart = new Models.Cart
                {
                    CustomerId = customerId,
                    CreatedById = createdById,
                };

                _context.Cart.Add(cart);
            }
            else
            {
                cart = await _context.Cart.Include(x=> x.cartItems).ThenInclude(x => x.Product).ThenInclude(x => x.OptionValues).ThenInclude(x => x.Option).FirstOrDefaultAsync(x => x.Id == cart.Id);
            }

            var cartItems = cart.cartItems.Where(x => x.ProductId == productId).ToList();
           
            if (cartItems == null)
            {
            
             var cartItem = new Models.CartItem
                {
                    Cart = cart,
                    ProductId = productId,
                    Values = values,
                    Quantity = quantity
                };
                var query = _context.Product.Include(x => x.OptionValues).ThenInclude(x => x.Option).FirstOrDefault(x => x.Id == cartItem.ProductId);
                if (values == "[]")
                {
                    if (query.OptionValues.Count > 0)
                    {
                        var value = query.OptionValues.Select(x => new OptionVariationVm
                        {
                            OptionId = x.OptionId,
                            OptionValues = JsonConvert.DeserializeObject<List<ProductOptionValueVm>>(x.Value).Select(x => x.Key).FirstOrDefault()
                        }).ToList();
                        var valuedefault = JsonConvert.SerializeObject(value);
                        cartItem.Values = valuedefault;
                    }
                }
                else
                {
                    var optionvalues = JsonConvert.DeserializeObject<List<OptionVariationVm>>(values);
                  
                        if (query.OptionValues.Count > optionvalues.Count)
                        {
                            var value = query.OptionValues.Select(x => new OptionVariationVm
                            {
                                OptionId = x.OptionId,
                                OptionValues = JsonConvert.DeserializeObject<List<ProductOptionValueVm>>(x.Value).Select(x => x.Key).FirstOrDefault()
                            }).ToList();
                            foreach (var item in value)
                            {
                                foreach (var item2 in optionvalues)
                                {
                                    if (item.OptionId != item2.OptionId)
                                    {
                                    var OptionVariationVm = new OptionVariationVm()
                                    {
                                        OptionId = item.OptionId,
                                        OptionValues = item.OptionValues
                                    };
                                    optionvalues.Add(OptionVariationVm);
                                    }
                             
                                }
                            }
                        var valuedefault = JsonConvert.SerializeObject(optionvalues);
                            cartItem.Values = valuedefault;
                        }
                }
                cart.cartItems.Add(cartItem);
            }
            else
            {
                var cartItem = cartItems.Find(x => x.Values == values);
                if (cartItem == null)
                {
                    cartItem = new Models.CartItem
                    {
                        Cart = cart,
                        ProductId = productId,
                        Values = values,
                        Quantity = quantity
                    };
                    var query = _context.Product.Include(x => x.OptionValues).ThenInclude(x => x.Option).FirstOrDefault(x => x.Id == cartItem.ProductId);
                    if (values == "[]")
                    {
                        if (query.OptionValues.Count > 0)
                        {
                            var value = query.OptionValues.Select(x => new OptionVariationVm
                            {
                                OptionId = x.OptionId,
                                OptionName = null,
                                OptionValues = JsonConvert.DeserializeObject<List<ProductOptionValueVm>>(x.Value).Select(x => x.Key).FirstOrDefault()
                            }).ToList();
                            var valuedefault = JsonConvert.SerializeObject(value);
                     
                                if (cartItem.Values != valuedefault)
                                {
                                     cartItem.Values = valuedefault;
                                }
                                else
                                {
                            
                                    cartItem.Quantity = cartItem.Quantity + quantity;
                                }
                            var cartItem2 = cartItems.Find(x => x.Values == cartItem.Values);
                            if (cartItem2 != null)
                            {
                                cartItem.Quantity++;
                            }

                        }
                    }
                    else
                    {
                        var optionvalues = JsonConvert.DeserializeObject<List<OptionVariationVm>>(values);
                     
                          
                        
                        if (query.OptionValues.Count > optionvalues.Count)
                        {
                            var value = query.OptionValues.Select(x => new OptionVariationVm
                            {
                                OptionId = x.OptionId,
                                OptionValues = JsonConvert.DeserializeObject<List<ProductOptionValueVm>>(x.Value).Select(x => x.Key).FirstOrDefault()
                            }).ToList();
                            foreach (var item in value)
                            {
                                foreach (var item2 in optionvalues)
                                {
                                    if (item.OptionId != item2.OptionId)
                                    {
                                        var OptionVariationVm = new OptionVariationVm()
                                        {
                                            OptionId = item.OptionId,
                                            OptionValues = item.OptionValues
                                        };
                                        optionvalues.Add(OptionVariationVm);
                                    }

                                }
                            }
                            var valuedefault = JsonConvert.SerializeObject(optionvalues);
                            cartItem.Values = valuedefault;
                         }

                    }
                    cart.cartItems.Add(cartItem);
                }
                else
                {
                   cartItem.Quantity = cartItem.Quantity + quantity;
                }
            }
          await  _context.SaveChangesAsync();
            return true;
        }

        public Task<Models.Cart> GetActiveCart(long customerId)
        {
            return GetActiveCart(customerId, customerId);
        }

        public async Task<Models.Cart> GetActiveCart(long customerId, long createdById)
        {
            return await _context.Cart
                .Include(x => x.cartItems) 
                .Where(x => x.CustomerId == customerId && x.CreatedById == createdById).FirstOrDefaultAsync();
        }
    }
}
