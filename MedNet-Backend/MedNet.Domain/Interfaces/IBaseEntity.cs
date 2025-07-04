namespace MedNet.Domain.Interfaces;

/// <summary>
/// Base interface for entities which can be used for entities not having primary keys, or having composite primary keys
/// </summary>
public interface IBaseEntity
{
}

/// <summary>
/// Base interface for entities with a single integer primary key
/// </summary>
public interface IBaseKeyedEntity : IBaseKeyedEntity<int>
{
}

/// <summary>
/// Base interface for entities with a single primary key
/// </summary>
public interface IBaseKeyedEntity<TKey> : IBaseEntity
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
}
