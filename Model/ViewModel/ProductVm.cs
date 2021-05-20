using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ProductVm
    {
        public long Id { get; set; }
        public string Code { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Tiêu Đề")]
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public List<Category> categories { get; set; } = new List<Category>();
        public Category Category { get; set; }
        public List<Brand> brands { get; set; } = new List<Brand>();
        public Brand Brand { get; set; }

        public long? BrandId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Giá")]
        public decimal? Price { get; set; }
        public decimal? Sale { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditOn { get; set; }
        public long? EditBy { get; set; }

        public long? CategoryId { get; set; }
        public bool IsFuture { get; set; }
        public bool IsHot { get; set; }
        public IList<Rating> Rating { get; set; } = new List<Rating>();

        public string ThumbnailImageUrl { get; set; }
        public IList<ProductMediaVm> ProductImages { get; set; } = new List<ProductMediaVm>();
        public IList<ProductOptionVm> ProductOptionVm { get; set; } = new List<ProductOptionVm>();
        public IList<ProductOptionVm> Options { get; set; } = new List<ProductOptionVm>();
    }
}
