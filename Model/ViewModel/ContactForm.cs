using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModel
{
    public class ContactForm
    {
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Email")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Số Điện Thoại")]
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Ghi Chu")]
        public string Note { get; set; }
    }
}
