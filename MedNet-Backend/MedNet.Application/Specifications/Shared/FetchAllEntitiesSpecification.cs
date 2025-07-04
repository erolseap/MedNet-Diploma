using MedNet.Domain.Models;
using MedNet.Domain.Specifications;

namespace MedNet.Application.Specifications.Shared;

public class FetchAllEntitiesSpecification<TEntity> : BaseSpecification<TEntity> where TEntity : BaseEntity
{
    public FetchAllEntitiesSpecification() : base(e => true)
    {
    }
}
