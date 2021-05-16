using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel.Web
{
    public class Search
    {
      
        public long? Category { get; set; }
        public long? Brand { get; set; }
        public string Sort { get; set; }
        public string query { get; set; }
        public decimal? PriceStart { get; set; }
        public decimal? PriceEnd { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1;
        public int TotalRecords { get; set; }
        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }

    }
}
