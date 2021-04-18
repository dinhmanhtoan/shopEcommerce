using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
    public class ProductList
    {
        public List<ProductThumbnail> products { get; set; }
        public List<Category> categories { get; set; }
        public string Images { get; set; }
    }
}
