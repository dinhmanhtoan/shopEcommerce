using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Shop.Extensions;

public class ShopUserStore : UserStore<User, Role, shopContext, long, IdentityUserClaim<long>, UserRole,
    IdentityUserLogin<long>,IdentityUserToken<long>, IdentityRoleClaim<long>>
{
    public ShopUserStore(shopContext context, IdentityErrorDescriber describer) : base(context, describer)
    {
    }
}

