namespace Model.Models;
public class shopContext : IdentityDbContext<User, Role, long, IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
{
    public shopContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Type> typeToRegisters = new List<Type>();
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var type = entity.ClrType.Assembly.DefinedTypes.Select(t => t.AsType());
            var types = entity.ClrType.Assembly;
            var typess = entity.ClrType.Assembly.DefinedTypes;

            typeToRegisters.AddRange(type);
        }
     
        RegisterEntities(modelBuilder, typeToRegisters);
        RegisterConvention(modelBuilder);
        base.OnModelCreating(modelBuilder);
        ShopCustomModelBuilder.Build(modelBuilder);
        ShopSeedData.SeedData(modelBuilder);
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                  .SelectMany(t => t.GetProperties())
                  .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }
    }
    private static void RegisterConvention(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var nameParts = entity.ClrType.Name;
            modelBuilder.Entity(entity.Name).ToTable(nameParts);
        }
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    private static void RegisterEntities(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
    {
        var entityTypes = typeToRegisters.Where(x => x.GetTypeInfo().IsSubclassOf(typeof(EntityBase)) && !x.GetTypeInfo().IsAbstract);
        foreach (var type in entityTypes)
        {
            modelBuilder.Entity(type);
        }
    }
}

