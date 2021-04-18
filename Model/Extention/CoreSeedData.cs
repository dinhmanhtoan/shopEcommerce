using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Model.Extention
{
    public class CoreSeedData
    {
        public static void SeedData(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
            new Role { Id = 1L, ConcurrencyStamp = "4776a1b2-dbe4-4056-82ec-8bed211d1454", Name = "admin", NormalizedName = "ADMIN" },
            new Role { Id = 2L, ConcurrencyStamp = "00d172be-03a0-4856-8b12-26d63fcf4374", Name = "customer", NormalizedName = "CUSTOMER" },
            new Role { Id = 3L, ConcurrencyStamp = "d4754388-8355-4018-b728-218018836817", Name = "guest", NormalizedName = "GUEST" },
            new Role { Id = 4L, ConcurrencyStamp = "71f10604-8c4d-4a7d-ac4a-ffefb11cefeb", Name = "vendor", NormalizedName = "VENDOR" }
            );
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 10L,
                    AccessFailedCount = 0,
                    ConcurrencyStamp = "101cd6ae-a8ef-4a37-97fd-04ac2dd630e4",
                    CreatedOn = DateTime.Now,
                    Email = "system@simplcommerce.com",
                    EmailConfirmed = false,
                    FullName = "System User",
                    LockoutEnabled = false,
                    NormalizedEmail = "SYSTEM@SIMPLCOMMERCE.COM",
                    NormalizedUserName = "SYSTEM@SIMPLCOMMERCE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEJ90QpvSu/TBqD8+UzFWfTTU0zWPnkqNoU5qhCoPVI9/rJt1Lnbjy+lyRWQz/wYzwg==",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    UserName = "system@simplcommerce.com",
                    SecurityStamp = "a9565acb-cee6-425f-9833-419a793f5fba",
                    Guid = new Guid("5f72f83b-7436-4221-869c-1b69b2e23aae")
                });
            builder.Entity<UserRole>().HasData(
                new UserRole { UserId = 10, RoleId = 1 }
            );
        }

    }
}
