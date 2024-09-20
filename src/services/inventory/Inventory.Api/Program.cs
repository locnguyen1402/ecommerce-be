using Microsoft.EntityFrameworkCore;

using ECommerce.Inventory.Data;
using ECommerce.Inventory.Data.Repositories;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Libs.Extensions;
using ECommerce.Inventory.Api.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

// Db context
var connectionString = Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.ConfigDbContext<InventoryDbContext>(connectionString, typeof(Program).Assembly.ToString());

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IShopCollectionRepository, ShopCollectionRepository>();
builder.Services.AddScoped<IProductPromotionRepository, ProductPromotionRepository>();
builder.Services.AddScoped<IOrderPromotionRepository, OrderPromotionRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();

builder.Services.AddScoped<IMerchantService, MerchantService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
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
