using MedNet.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MedNet.Infrastructure.Entities;

public class AppUserRole : IdentityRole<int>, IAppUserRole
{
    public AppUserRole() : base()
    {
        
    }

    public AppUserRole(string name) : base(name)
    {
    }
}
