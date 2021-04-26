using Microsoft.AspNetCore.Http;
using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model.ViewModel
{ 
    public class FormProduct
    {
        public ProductVm Products { get; set; } = new ProductVm();
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập chọn Ảnh")]
        public IFormFile ThumbnailImage { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập chọn Ảnh")]
        public IList<IFormFile> ProductImages { get; set; } = new List<IFormFile>();
        public string ListDelete { get; set; }
        public ProductMedia productMedia { get; set; }

    }
}
