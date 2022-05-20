using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Shop.Extensions;

public class ShopRoleStore: RoleStore<Role, shopContext, long, UserRole, IdentityRoleClaim<long>>
{
    public ShopRoleStore(shopContext context) : base(context)
    {
    }
}
