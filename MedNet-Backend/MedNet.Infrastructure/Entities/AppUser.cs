using MedNet.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MedNet.Infrastructure.Entities;

public class AppUser : IdentityUser<int>, IAppUser
{
    public AppUser() : base()
    {
        
    }

    public AppUser(string userName) : base(userName)
    {
    }
}
