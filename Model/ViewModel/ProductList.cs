﻿using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
    public class ProductList
    {
        public IList<ProductThumbnail> products { get; set; } = new List<ProductThumbnail>();
        public IList<Category> categories { get; set; } = new List<Category>();
        public string Images { get; set; }
    }
}
