using PeopleHub.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();