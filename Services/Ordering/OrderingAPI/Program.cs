using OrderingAPI;
using OrderingApplication;
using OrderingInfrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddServices();

var app = builder.Build();

app.UseServices();

app.Run();
