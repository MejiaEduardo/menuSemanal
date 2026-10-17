using System.Text.Json.Serialization;
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
// Los enums (DiaSemana, TipoComida) se leen y se muestran como texto: "Lunes", "Almuerzo"...
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
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