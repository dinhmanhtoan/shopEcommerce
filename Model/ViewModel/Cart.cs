using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
    public class Cart 
    {
        public long Id { get; set; }
        public long quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
    }
}
