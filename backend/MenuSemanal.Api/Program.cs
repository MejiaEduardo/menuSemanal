using System.Text.Json.Serialization;
using DotNetEnv;
using MenuSemanal.Application;
using MenuSemanal.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// TraversePath() busca el .env también en carpetas superiores
Env.TraversePath().Load();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresConnection")
                       ?? throw new InvalidOperationException("Falta la cadena de conexión en el archivo .env.");

var corsAllowedOrigins = Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS")
                         ?? throw new InvalidOperationException("Falta la variable CORS_ALLOWED_ORIGINS en el archivo .env.");

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddApplication();

// Los enums (DiaSemana, TipoComida) se leen y se muestran como texto
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();

const string CorsPolicy = "PoliticaCorsMenuSemanal";

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy, policy =>
    {
        var origenes = corsAllowedOrigins.Split(
            ',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        policy.WithOrigins(origenes)
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    // En desarrollo solo escuchas en http, así que se evita el warning
    app.UseHttpsRedirection();
}

app.UseCors(CorsPolicy);

app.MapControllers();
app.Run();