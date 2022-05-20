namespace Model.Services;
public interface IEntityService
{
    string ToSafeSlug(string slug, long entityId, string entityTypeId);
    Entity Get(long entityId, string entityTypeId);
    void Add(string name, string slug, long entityId, string entityTypeId);

    void Update(string newName, string newSlug, long entityId, string entityTypeId);

    void Remove(long entityId, string entityTypeId);
}
