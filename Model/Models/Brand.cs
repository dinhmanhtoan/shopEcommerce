using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Brand
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Vui Lòng Nhập Tên"), MaxLength(250)]
        public string Name { get; set; }
        public string Slug { get; set; }
        public IList<Product> Products { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }
    }
}
