using Microsoft.EntityFrameworkCore;
using MenuSemanal.Domain.Entities;

namespace MenuSemanal.Infrastructure.Data;
    
public class MenuSemanalDbContext : DbContext
{
    public MenuSemanalDbContext(DbContextOptions<MenuSemanalDbContext> options) : base(options) { }

    public DbSet<MenuGlobal> MenusGlobales { get; set; }
}   