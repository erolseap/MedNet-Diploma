using MedNet.Domain.Models;
using MedNet.Domain.Specifications;

namespace MedNet.Application.Specifications.Shared;

public class GetEntityByIdSpecification<TEntity> : GetEntityByIdSpecification<TEntity, int>
    where TEntity : BaseKeyedEntity
{
    public GetEntityByIdSpecification(int id) : base(id)
    {
    }
}

public class GetEntityByIdSpecification<TEntity, TEntityKey> : BaseSpecification<TEntity>
    where TEntity : BaseKeyedEntity<TEntityKey>
    where TEntityKey : IEquatable<TEntityKey>
{
    public GetEntityByIdSpecification(TEntityKey id) : base(e => e.Id.Equals(id))
    {
    }
}
