using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
   public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public List<CartItemVm> CartItemVms { get; set; }
        public CheckoutRequest CheckoutModel { get; set; }
        public IList<Product> Viewed { get; set; }
    }
}
