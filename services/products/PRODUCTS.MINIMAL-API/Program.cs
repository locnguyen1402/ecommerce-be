using Products.MinimalApis.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.AutoMapEndpoints();

app.Run();
