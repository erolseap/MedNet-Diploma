﻿namespace MedNet.Domain.Interfaces;

public interface IAppUser : IBaseKeyedEntity
{
    /// <summary>Gets or sets the username for this user.</summary>
    public string? UserName { get; set; }

    /// <summary>Gets or sets the email address for this user.</summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if a user has confirmed their email address.
    /// </summary>
    /// <value>True if the email address has been confirmed, otherwise false.</value>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if two-factor authentication is enabled for this user.
    /// </summary>
    /// <value>True if 2fa is enabled, otherwise false.</value>
    public bool TwoFactorEnabled { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time, in UTC, when any user lockout ends.
    /// </summary>
    /// <remarks>A value in the past means the user is not locked out.</remarks>
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if the user could be locked out.
    /// </summary>
    /// <value>True if the user could be locked out, otherwise false.</value>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Gets or sets the number of failed login attempts for the current user.
    /// </summary>
    public int AccessFailedCount { get; set; }
}
