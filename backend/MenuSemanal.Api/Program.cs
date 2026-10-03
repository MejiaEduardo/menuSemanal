using DotNetEnv;
using MenuSemanal.Infrastructure;
using Scalar.AspNetCore; // 1. Importamos Scalar
using MenuSemanal.Application;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno desde el archivo .env local
Env.Load();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresConnection")
                       ?? throw new InvalidOperationException("Falta la cadena de conexión.");

// Llamamos al método que inyecta los repositorios y la base de datos


builder.Services.AddInfrastructure(connectionString);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // levanta la interfaz grafica moderna consumiendo ese JSON
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();

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



record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}