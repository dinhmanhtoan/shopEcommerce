using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
   public class OrderDetail
    {
        public long Quantity { set; get; }
        public decimal Price { set; get; }
        public Order Order { get; set; }
        public long OrderId { get; set; }
        public Product Product { get; set; }
        public long ProductId { set; get; }
    }
}
