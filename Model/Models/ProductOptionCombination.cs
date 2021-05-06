using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class ProductOptionCombination
    {
        public long Id { get; set; }
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public long OptionId { get; set; }

        public ProductOption Option { get; set; }

        [StringLength(450)]
        public string Value { get; set; }

        public int SortIndex { get; set; }
    }
}
