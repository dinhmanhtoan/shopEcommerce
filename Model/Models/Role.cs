namespace Model.Models;
public class Role : IdentityRole<long>, IEntityWithTypedId<long>
{
    public IList<UserRole> Users { get; set; } = new List<UserRole>();
}

