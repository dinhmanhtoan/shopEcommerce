namespace Model.Infrastructure;
public interface IEntityWithTypedId<TId>
{
    TId Id { get; }
}

