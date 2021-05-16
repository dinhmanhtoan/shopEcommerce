using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class Cart
    {
        public long Id { get; set; }
        public long CustomerId { get;set;}
        public User Customer { get; set; }
        public long CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public IList<CartItem> cartItems { get; set; } = new List<CartItem>();
    }
}
