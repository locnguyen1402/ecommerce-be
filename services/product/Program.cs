using System.Text.Json.Serialization;
using ECommerce.Services.Product;
using ECommerce.Shared.Libs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigDbContext<ProductDbContext>(builder.Configuration.GetConnectionString("DefaultConnectionString")!);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddAutoMapper();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
