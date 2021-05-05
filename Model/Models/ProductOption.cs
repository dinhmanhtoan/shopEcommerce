﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
   public class ProductOption
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public IList<ProductOptionValue> OptionValue { get; set; }
    }
}
