using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
   public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; }

        public CheckoutRequest CheckoutModel { get; set; }
    }
}
