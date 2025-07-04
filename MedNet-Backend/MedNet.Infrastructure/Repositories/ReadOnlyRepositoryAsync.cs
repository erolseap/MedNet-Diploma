using MedNet.Domain.Interfaces;
using MedNet.Domain.Models;
using MedNet.Domain.Repositories;
using MedNet.Domain.Specifications;
using MedNet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MedNet.Infrastructure.Repositories;

public class ReadOnlyRepositoryAsync<TEntity> : IReadOnlyRepositoryAsync<TEntity>
    where TEntity : BaseEntity
{
    private readonly AppDbContext _context;

    protected virtual DbSet<TEntity> DbEntitySet => _context.Set<TEntity>();
    protected virtual IQueryable<TEntity> DbSetQueryable => DbEntitySet.AsNoTracking();

    public virtual bool IsTrackingCapable => false;

    public ReadOnlyRepositoryAsync(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity>? spec = null,
        CancellationToken cancellationToken = default)
    {
        return await (spec == null ? DbSetQueryable : ApplySpecification(spec)).ToListAsync(cancellationToken);
    }

    public Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
    {
        return ApplySpecification(spec).SingleOrDefaultAsync(cancellationToken);
    }

    public Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
    {
        return ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<int> CountAsync(ISpecification<TEntity>? spec = null,
        CancellationToken cancellationToken = default)
    {
        return (spec == null ? DbSetQueryable : ApplySpecification(spec)).CountAsync(cancellationToken);
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return DbSetQueryable.EvaluateSpecification(spec);
    }
}
