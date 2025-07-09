using MedNet.Domain.Models;
using MedNet.Domain.Specifications;

namespace MedNet.Application.Specifications.Shared;

public class GetEntitiesByIdExceptSpecification<TEntity> : GetEntitiesByIdExceptSpecification<TEntity, int>
    where TEntity : BaseKeyedEntity
{
    public GetEntitiesByIdExceptSpecification(IEnumerable<int> excludeIds) : base(excludeIds)
    {
    }
}

public class GetEntitiesByIdExceptSpecification<TEntity, TEntityKey> : BaseSpecification<TEntity>
    where TEntity : BaseKeyedEntity<TEntityKey>
    where TEntityKey : IEquatable<TEntityKey>
{
    public GetEntitiesByIdExceptSpecification(IEnumerable<TEntityKey> excludeIds) : base(e => !excludeIds.Contains(e.Id))
    {
    }
}
