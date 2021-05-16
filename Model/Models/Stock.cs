using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class Stock
    {
        public long Id { get; set; }
        public long ProductId { get; set; }

        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }
    }
}
