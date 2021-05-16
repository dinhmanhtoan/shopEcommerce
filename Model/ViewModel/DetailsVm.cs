using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
    public class DetailsVm
    {
        public Product product { get; set; }
        public List<Product> products { get; set; }
        public IList<ProductOptionVm> ProductOptionVm { get; set; } = new List<ProductOptionVm>();
    }
}
