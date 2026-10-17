using Microsoft.EntityFrameworkCore;
using MenuSemanal.Domain.Entities;

namespace MenuSemanal.Infrastructure.Data;

public class MenuSemanalDbContext : DbContext
{
    public MenuSemanalDbContext(DbContextOptions<MenuSemanalDbContext> options) : base(options) { }

    public DbSet<MenuGlobal> MenusGlobales { get; set; }
    public DbSet<Platillo> Platillos { get; set; }
    public DbSet<MenuPlatillo> MenuPlatillos { get; set; }

    // Fluent API: aquí se define cómo se ve cada tabla en PostgreSQL
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuGlobal>(entity =>
        {
            entity.Property(m => m.Nombre).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Platillo>(entity =>
        {
            entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Descripcion).HasMaxLength(500);
        });

        modelBuilder.Entity<MenuPlatillo>(entity =>
        {
           
            // Si se borra el menú, se borran sus renglones
            entity.HasOne(mp => mp.MenuGlobal)
                .WithMany(m => m.MenuPlatillos)
                .HasForeignKey(mp => mp.MenuGlobalId)
                .OnDelete(DeleteBehavior.Cascade);

            // No se puede borrar un platillo que ya está usado en algún menú
            entity.HasOne(mp => mp.Platillo)
                .WithMany(p => p.MenuPlatillos)
                .HasForeignKey(mp => mp.PlatilloId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}