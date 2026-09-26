using MenuSemanal.Application.Interfaces;
using MenuSemanal.Infrastructure.Data;
using MenuSemanal.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MenuSemanal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // configuracion al acceso a Postgres
        services.AddDbContext<MenuSemanalDbContext>(options =>
            options.UseNpgsql(connectionString));

        // 2. LA INYECCIÓN CLAVE (El contrato firmado por el Gerente):
        // "Cada vez que un Chef (Controlador) pida la Receta (IHabitoRepository), 
        // entrégale los datos de la Finca PostgreSQL (HabitoRepository)"
        services.AddScoped<IMenuRepository, MenuRepository>();
        
        return services;
    }
}