using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class CartItem
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long CartId { get; set; }
        public Cart Cart { get; set; }
        // biết được sản phẩm nào
        // string Values kiểu part object
        //biết được Size , Color vd: Color
        //public long OptionId { get; set; }
        // biết được giá trị vd: #fff
        //public string OptionValues { get; set; }
        public string Values { get; set; }

        public int Quantity { get; set; }
    }
}
