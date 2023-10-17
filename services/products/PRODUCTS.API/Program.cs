var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!;
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));

builder.Services.ConfigDbContext<ProductDbContext>
(
    configuration!.GetConnectionString("DefaultConnectionString")!,
    typeof(Program).Assembly.ToString()
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigController();

builder.Services
    .AddAutoMapper()
    .AddValidation()
    .RegisterMediatR();

builder.Services.RegisterOLRestClient(appSettings.Integration.OpenLibrary.RestClients.BaseUrl);
// builder.Services.RegisterOLRestClient("https://openlibrary.org");

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
