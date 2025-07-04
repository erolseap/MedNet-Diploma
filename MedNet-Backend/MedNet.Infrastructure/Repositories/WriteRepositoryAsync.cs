using MedNet.Domain.Interfaces;
using MedNet.Domain.Models;
using MedNet.Domain.Repositories;
using MedNet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MedNet.Infrastructure.Repositories;

public class WriteRepositoryAsync<TEntity> : ReadOnlyRepositoryAsync<TEntity>, IWriteRepositoryAsync<TEntity>
    where TEntity : BaseEntity
{
    protected override IQueryable<TEntity> DbSetQueryable => DbEntitySet.AsTracking();

    public override bool IsTrackingCapable => true;

    public WriteRepositoryAsync(AppDbContext context) : base(context)
    {
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbEntitySet.AddAsync(entity,  cancellationToken);
        return entity;
    }

    public Task AddRangeAsync(params TEntity[] entities)
    {
        return DbEntitySet.AddRangeAsync(entities);
    }

    public void Update(TEntity entity)
    {
        DbEntitySet.Update(entity);
    }

    public void UpdateRange(params TEntity[] entities)
    {
        DbEntitySet.UpdateRange(entities);
    }

    public void Remove(TEntity entity)
    {
        DbEntitySet.Remove(entity);
    }

    public void RemoveRange(params TEntity[] entities)
    {
        DbEntitySet.RemoveRange(entities);
    }
}
