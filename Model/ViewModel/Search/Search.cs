using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel.Search
{
    public class Search
    {
      
        public long? Category { get; set; }
        public long? Brand { get; set; }
        public string Sort { get; set; }
        public decimal? PriceStart { get; set; }
        public decimal? PriceEnd { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }
}
