namespace Model.Infrastructure;
public class Repository<T> : RepositoryWithTypedId<T, long>, IRepository<T> where T : class, IEntityWithTypedId<long>
{
    public Repository(shopContext context) : base(context)
    {
    }
}

