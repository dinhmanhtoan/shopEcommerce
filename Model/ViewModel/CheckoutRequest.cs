using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModel
{
    public class CheckoutRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Họ")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Tên")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Email")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Số Điện Thoại")]
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui Lòng Nhập Số Địa chỉ Nhận Hàng")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ApartmentNumber { get; set; }
        public string Note { get; set; }

        public List<OrderDetailVm> OrderDetails { set; get; } = new List<OrderDetailVm>();
    }

}
