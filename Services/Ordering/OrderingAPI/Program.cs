using OrderingAPI;
using OrderingApplication;
using OrderingInfrastructure;
using OrderingInfrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Ordering API",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });

    await app.InitializeDatabaseAsync();
}

app.Run();
