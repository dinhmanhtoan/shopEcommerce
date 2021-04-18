using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class User : IdentityUser<long>
    {
        public User()
        {
            CreatedOn = DateTime.Now;
        }
        public Guid Guid { get; set; }
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string FullName { get; set; }
        public IList<UserRole> Roles { get; set; } = new List<UserRole>();
        public DateTime CreatedOn { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
 
    }
}
