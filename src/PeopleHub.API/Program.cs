var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();