namespace Model.Services;

public class EntityService : IEntityService
{
    public readonly IRepository<Entity> _entityRepository;
    public EntityService(IRepository<Entity> entityRepository)
    {
        _entityRepository = entityRepository;
    }
    public string ToSafeSlug(string slug, long entityId, string entityTypeId)
    {
        var i = 2;
        while (true)
        {
            var entity = _entityRepository.Query().FirstOrDefault(x => x.Slug == slug);
            if (entity != null && !(entity.EntityId == entityId && entity.EntityTypeId == entityTypeId))
            {
                slug = string.Format("{0}-{1}", slug, i);
                i++;
            }
            else
            {
                break;
            }
        }
        return slug;
    }
    public Entity Get(long entityId, string entityTypeId)
    {
        return _entityRepository.Query().FirstOrDefault(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId);
    }

    public void Add(string name, string slug, long entityId, string entityTypeId)
    {
        var entity = new Entity
        {
            Name = name,
            Slug = slug,
            EntityId = entityId,
            EntityTypeId = entityTypeId
        };

        _entityRepository.Add(entity);
    }

    public void Update(string newName, string newSlug, long entityId, string entityTypeId)
    {
        var entity = _entityRepository.Query().First(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId);
        entity.Name = newName;
        entity.Slug = newSlug;
    }

    public void Remove(long entityId, string entityTypeId)
    {
        var entity = _entityRepository.Query().FirstOrDefault(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId);

        if (entity != null)
        {
            _entityRepository.Remove(entity);
        };
    }

 
}

