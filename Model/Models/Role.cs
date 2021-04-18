using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
   public class Role : IdentityRole<long>
    {
        public IList<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
