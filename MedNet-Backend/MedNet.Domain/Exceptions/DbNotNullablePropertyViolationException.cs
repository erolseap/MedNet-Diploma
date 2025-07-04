namespace MedNet.Domain.Exceptions;

public class DbNotNullablePropertyViolationException : Exception
{
    public object Entity { get; }
    public string ColumnName { get; }

    public DbNotNullablePropertyViolationException(object entity, string columnName, Exception? innerException)
        : this("Unique constraint violation occured", entity, columnName, innerException)
    {
    }

    public DbNotNullablePropertyViolationException(string message, object entity, string columnName,
        Exception? innerException) : base(message, innerException)
    {
        Entity = entity;
        ColumnName = columnName;
    }
}
