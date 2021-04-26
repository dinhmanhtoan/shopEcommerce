using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class ProductOptionValue
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string DisplayType { get; set; }
        public int SortIndex { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public ProductOption Option { get; set; }
        public long OptionId { get; set; }
    }
}
