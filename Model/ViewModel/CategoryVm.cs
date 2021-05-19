using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CategoryVm
    {
        public string ThumbnailImageUrl { get; set; }
        public long Id { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Tiêu Đề")]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Miêu tả")]
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditOn { get; set; }
        public long? EditBy { get; set; }

        public ProductMediaVm ProductImages { get; set; } 
    }
}
