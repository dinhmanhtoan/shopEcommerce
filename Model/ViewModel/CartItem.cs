using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
   public class CartItem:  Cart
    {
        public ProductVm product { set; get; }
        public IList<Product> Viewed { get; set; }

    }
}
