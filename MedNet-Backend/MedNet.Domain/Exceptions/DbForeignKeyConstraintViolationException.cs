namespace MedNet.Domain.Exceptions;

public class DbForeignKeyConstraintViolationException : Exception
{
    public object Entity { get; }
    public string ColumnName { get; }

    public DbForeignKeyConstraintViolationException(object entity, string columnName, Exception? innerException) : this("Foreign key constraint violation occured", entity, columnName, innerException)
    {
    }

    public DbForeignKeyConstraintViolationException(string message, object entity, string columnName, Exception? innerException) : base(message, innerException)
    {
        Entity = entity;
        ColumnName = columnName;
    }
}
