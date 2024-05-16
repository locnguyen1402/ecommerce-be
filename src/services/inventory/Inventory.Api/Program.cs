using System.Reflection;
using Microsoft.EntityFrameworkCore;

using ECommerce.Inventory.Data;
using ECommerce.Inventory.Data.Repositories;
using ECommerce.Inventory.Domain.AggregatesModel;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Db context
var connectionString = Configuration.GetConnectionString("DefaultConnection")!;
builder.Services
    .AddDbContext<InventoryDbContext>(options =>
    {
        options.UseNpgsql(connectionString);
    });

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Common dependencies
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
