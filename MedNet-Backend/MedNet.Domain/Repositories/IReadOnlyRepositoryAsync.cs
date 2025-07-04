using MedNet.Domain.Models;
using MedNet.Domain.Specifications;

namespace MedNet.Domain.Repositories;

/// <summary>
/// This is a read-only type of asynchronous repository, which does not track any entity
/// If you want to track entities and save them later, see IWriteRepositoryAsync
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
public interface IReadOnlyRepositoryAsync<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Whether the repository is tracking-capable (write repository, for example) or not
    /// </summary>
    bool IsTrackingCapable { get; }

    /// <summary>
    /// Get a list of stored entities with specified conditions
    /// </summary>
    /// <returns>List of entities complied with conditions</returns>
    Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity>? spec = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get a single stored entity or default value if not found. Throws exception if more than one entity found with the same requirements applied
    /// </summary>
    /// <throws></throws>
    /// <returns>Stored entity or default value if not found</returns>
    Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get a first stored entity or default value if not found
    /// </summary>
    /// <returns>Stored entity or default value if not found</returns>
    Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get a count of stored entities with specified conditions
    /// </summary>
    /// <returns>Count of stored entities with specified conditions</returns>
    Task<int> CountAsync(ISpecification<TEntity>? spec = null, CancellationToken cancellationToken = default);
}
