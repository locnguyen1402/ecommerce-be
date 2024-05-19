using System.Reflection;
using Microsoft.EntityFrameworkCore;

using ECommerce.Inventory.Data;
using ECommerce.Inventory.Data.Repositories;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Extensions;

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AutoMapEndpoints(typeof(Program).Assembly);

app.Run();
