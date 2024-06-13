using gRPC.Server.Data;
using gRPC.Server.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Connect to database
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GRPC;Trusted_Connection=True;TrustServerCertificate=True;");
});

builder.Services.AddScoped<CustomerServiceImplementation>();

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();

app.MapGrpcReflectionService();

// Configure the HTTP request pipeline.
app.MapGrpcService<CustomerServiceImplementation>();
app.MapGrpcService<GreeterServiceImplementation>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

ApplyMigration();

app.Run();

void ApplyMigration()
{
    using var scope = app.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    if (_db.Database.GetPendingMigrations().Count() > 0)
    {
        _db.Database.Migrate();
    }
}