using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class ProductLink
    {
        public long Id { get; set; }
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public long LinkedProductId { get; set; }

        public Product LinkedProduct { get; set; }

        public ProductLinkType LinkType { get; set; }
    }
}
