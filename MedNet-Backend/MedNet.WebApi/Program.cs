using System.Text.Json;
using System.Text.Json.Serialization;
using MedNet.Application;
using MedNet.Infrastructure;
using MedNet.Infrastructure.Data;
using MedNet.Infrastructure.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplication();
builder.Services.ConfigureInfrastructure(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.KebabCaseLower));
    });

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddRoles<AppUserRole>()
    .AddEntityFrameworkStores<AppDbContext>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenApi();
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter your valid token.\nExample: `eyJhbGciOij2fC...`"
        });
        
        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        if (!builder.Environment.IsDevelopment())
        {
            policy.WithOrigins("https://mednet.com")
                .AllowAnyHeader()
                .AllowCredentials()
                .WithMethods("GET", "POST", "DELETE", "PATCH")
                .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
        }
        else
        {
            policy.SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyMethod();
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.MigrateDatabase();
}

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.CreateRoles();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGroup("account")
    .MapIdentityApi<AppUser>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.Run();
