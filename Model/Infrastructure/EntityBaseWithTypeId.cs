namespace Model.Infrastructure;
public abstract class EntityBaseWithTypeId<TId> : IEntityWithTypedId<TId>
{
    public virtual TId Id { get; protected set; }
}

