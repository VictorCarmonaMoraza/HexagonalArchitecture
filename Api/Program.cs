using Dominio.Ports.Primary;
using Dominio.Ports.Secundary;
using Dominio.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "product.json");


//Inyeccion de dependencias
builder.Services.AddTransient<IRepository>(provider => new JsonRepository.ProductRepository(path));
builder.Services.AddTransient<IService, ProductService>();

//Agregar codigo para swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", (IService service) =>
{
    return service.GetAll();
}).WithName("GetProducts");

app.MapPost("/products", (string name, decimal price, IService service) =>
{
    service.Register(name, price);
    return Results.Created();
}).WithName("AddProduct");
/*
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/

app.Run();