using System.Text.Json.Serialization;
using ECommerce.Services.Orders;
using ECommerce.Shared.Libs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigDbContext<OrderDbContext>(builder.Configuration.GetConnectionString("DefaultConnectionString")!);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IProductRestClient>(serviceProvider =>
{
    return new ProductRestClient("http://localhost:5096/");
});

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
