using MedNet.Application.Interfaces;
using MedNet.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace MedNet.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exc) when (exc.InnerException is PostgresException pgresException &&
                                            pgresException.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            throw new DbUniqueConstraintViolationException(pgresException.Message, exc.Entries[0].Entity, pgresException.ColumnName ?? string.Empty, exc);
        }
        catch (DbUpdateException exc) when (exc.InnerException is PostgresException pgresException &&
                                            pgresException.SqlState == PostgresErrorCodes.ForeignKeyViolation)
        {
            throw new DbForeignKeyConstraintViolationException(pgresException.Message, exc.Entries[0].Entity, pgresException.ColumnName ?? string.Empty, exc);
        }
        catch (DbUpdateException exc) when (exc.InnerException is PostgresException pgresException &&
                                            pgresException.SqlState == PostgresErrorCodes.NotNullViolation)
        {
            throw new DbNotNullablePropertyViolationException(pgresException.Message, exc.Entries[0].Entity, pgresException.ColumnName ?? string.Empty, exc);
        }
    }
}
