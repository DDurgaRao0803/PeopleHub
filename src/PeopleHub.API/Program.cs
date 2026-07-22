using PeopleHub.Infrastructure.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PeopleHub.Infrastructure.Persistence.Context;
using PeopleHub.Infrastructure.Persistence.Seeders;
using PeopleHub.API.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using PeopleHub.Application;
using PeopleHub.Application.Users.Validators;

var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PeopleHub API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});
if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddInfrastructure(
        builder.Configuration,
        options => { });
}
else
{
    builder.Services.AddInfrastructure(builder.Configuration);
}
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!app.Environment.IsEnvironment("Testing"))
    {
        await db.Database.MigrateAsync();
        await ServiceCategorySeeder.SeedAsync(db);
        await AdminSeeder.SeedAsync(db);
    }
}

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}