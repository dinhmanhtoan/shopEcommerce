using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
    public class CartItemVm
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public ProductVm ProductVm { get; set; }
        public long CartId { get; set; }

        public IList<OptionVariationVm> Values { get; set; } = new List<OptionVariationVm>();

        public int Quantity { get; set; }
    }
}
