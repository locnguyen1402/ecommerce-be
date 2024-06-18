using Microsoft.EntityFrameworkCore;

using ECommerce.Inventory.Data;
using ECommerce.Inventory.Data.Repositories;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Libs.Extensions;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Db context
var connectionString = Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.ConfigDbContext<InventoryDbContext>(connectionString, typeof(Program).Assembly.ToString());

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();

// Common dependencies
builder.Services
    .ConfigureJson()
    .AddHttpContextAccessor()
    .AddAutoMapper()
    .AddValidation()
    .RegisterMediatR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Enable CORS
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AutoMapEndpoints(typeof(Program).Assembly);

app.Run();
