using System.Reflection;
using MedNet.Domain.Entities;
using MedNet.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedNet.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<AppUser, AppUserRole, int>
{
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<UserTestSession> UserTestsSessions { get; set; }
    public DbSet<UserTestSessionQuestion> UserTestSessionQuestions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
