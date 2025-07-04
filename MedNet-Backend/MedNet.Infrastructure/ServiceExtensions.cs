using System.Reflection;
using MedNet.Application.Interfaces;
using MedNet.Domain.Repositories;
using MedNet.Infrastructure.Data;
using MedNet.Infrastructure.Entities;
using MedNet.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedNet.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts => opts
            .UseNpgsql(configuration.GetConnectionString("default")!, builder => builder.MigrationsAssembly(Assembly.GetExecutingAssembly()))
            .UseSnakeCaseNamingConvention()
        );
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IReadOnlyRepositoryAsync<>), typeof(ReadOnlyRepositoryAsync<>));
        services.AddScoped(typeof(IWriteRepositoryAsync<>), typeof(WriteRepositoryAsync<>));
        // services.AddScoped<IReadOnlyRepositoryAsync<Question>, ReadOnlyRepositoryAsync<Question>>();
    }

    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>();

        using var dbContext = new AppDbContext(dbContextOptions);
        dbContext.Database.Migrate();
    }

    public static async Task CreateRoles(this IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<AppUserRole>>();

        async Task CreateRoleIfNotExists(string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName)) return;
            await roleManager.CreateAsync(new AppUserRole(roleName));
        }

        await CreateRoleIfNotExists("Admin");
    }
}
