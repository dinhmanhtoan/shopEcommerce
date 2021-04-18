using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CategoryVm
    {
        public string ThumbnailImageUrl { get; set; }
        public long Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditOn { get; set; }
        public long? EditBy { get; set; }
        public IList<ProductMediaVm> ProductImages { get; set; } = new List<ProductMediaVm>();
    }
}
