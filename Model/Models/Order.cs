using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class Order
    {
        public long Id { get; set; }
        public DateTimeOffset? CreateOn { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public string AddressLine1{ set; get; }
        public string AddressLine2 { set; get; }
        public string ApartmentNumber {set; get; }
        public string Node { set; get; }
        public int Status { set; get; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
