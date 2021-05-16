using Model.Models;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cart = Model.Models.Cart;

namespace Model.Services
{
    public interface ICartService
    {
        Task<bool> AddToCart(long customerId, long productId, int quantity,string values);

        Task<bool> AddToCart(long customerId, long createdById, long productId, int quantity,string values);
        Task<Cart> GetActiveCart(long customerId);

        Task<Cart> GetActiveCart(long customerId, long createdById);

        //Task<CartVm> GetActiveCartDetails(long customerId);

        //Task<CartVm> GetActiveCartDetails(long customerId, long createdById);
    }
}
