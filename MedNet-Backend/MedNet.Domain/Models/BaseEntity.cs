using MedNet.Domain.Interfaces;

namespace MedNet.Domain.Models;

public abstract class BaseEntity : IBaseEntity
{
}

public abstract class BaseKeyedEntity : BaseKeyedEntity<int>
{
}

public abstract class BaseKeyedEntity<TKey> : BaseEntity, IBaseKeyedEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}
