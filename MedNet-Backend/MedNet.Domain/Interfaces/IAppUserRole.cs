namespace MedNet.Domain.Interfaces;

public interface IAppUserRole : IBaseKeyedEntity
{
    /// <summary>Gets or sets the name for this role.</summary>
    public string? Name { get; set; }
}
