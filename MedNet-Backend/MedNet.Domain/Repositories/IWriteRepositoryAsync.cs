using MedNet.Domain.Models;

namespace MedNet.Domain.Repositories;

/// <summary>
/// This is a write-capable type of asynchronous repository, which is tracking every added/fetched entity
/// When write-capable repository is used, all entities are automatically tracked, so you can save changes later
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
public interface IWriteRepositoryAsync<TEntity> : IReadOnlyRepositoryAsync<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Add a passed entity and start tracking
    /// </summary>
    /// <param name="entity">Entity itself</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A passed entity</returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a range of passed entities and start tracking
    /// </summary>
    /// <param name="entities">Range of entities</param>
    Task AddRangeAsync(params TEntity[] entities);

    /// <summary>
    /// Update a passed entity. Entity should not be tracked
    /// </summary>
    /// <param name="entity">Updating entity</param>
    void Update(TEntity entity);
    
    /// <summary>
    /// Update a range of passed entities. Entities should not be tracked
    /// </summary>
    /// <param name="entities">Range of entities</param>
    void UpdateRange(params TEntity[] entities);

    /// <summary>
    /// Remove a passed entity
    /// </summary>
    /// <param name="entity">Removing entity</param>
    void Remove(TEntity entity);
    
    /// <summary>
    /// Remove a range of passed entities.
    /// </summary>
    /// <param name="entities">Range of entities</param>
    void RemoveRange(params TEntity[] entities);
}
