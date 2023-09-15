var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.ConfigDbContext<ProductDbContext>(configuration.GetConnectionString("DefaultConnectionString")!);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.ConfigController();

builder.Services
    .AddAutoMapper()
    .AddValidation();

builder.Services.RegisterOLRestClient(configuration.GetSection("Integration:OpenLibrary").Get<Integration>()!.BaseUrl);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
