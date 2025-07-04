namespace MedNet.Domain.Exceptions;

public class DbUniqueConstraintViolationException : Exception
{
    public object Entity { get; }
    public string ColumnName { get; }

    public DbUniqueConstraintViolationException(object entity, string columnName, Exception? innerException) : this("Unique constraint violation occured", entity, columnName, innerException)
    {
    }

    public DbUniqueConstraintViolationException(string message, object entity, string columnName, Exception? innerException) : base(message, innerException)
    {
        Entity = entity;
        ColumnName = columnName;
    }
}
