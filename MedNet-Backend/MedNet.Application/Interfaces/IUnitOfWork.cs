using MedNet.Domain.Exceptions;

namespace MedNet.Application.Interfaces;

public interface IUnitOfWork
{
    /// <summary>Commit all pending changes to the database</summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="DbForeignKeyConstraintViolationException">Thrown when foreign key constraint is violated</exception>
    /// <exception cref="DbUniqueConstraintViolationException">Thrown when unique key constraint is violated</exception>
    /// <exception cref="DbNotNullablePropertyViolationException">Thrown when null value to non-nullable property passed</exception>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database</returns>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
