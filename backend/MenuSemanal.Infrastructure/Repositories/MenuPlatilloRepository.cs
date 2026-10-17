using Microsoft.EntityFrameworkCore;
using MenuSemanal.Application.Interfaces;
using MenuSemanal.Domain.Entities;
using MenuSemanal.Infrastructure.Data;

namespace MenuSemanal.Infrastructure.Repositories;

public class MenuPlatilloRepository : IMenuPlatilloRepository
{
    private readonly MenuSemanalDbContext _context;

    public MenuPlatilloRepository(MenuSemanalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MenuPlatillo>> GetAllAsync(int? menuGlobalId = null)
    {
        var query = _context.MenuPlatillos
            .AsNoTracking()
            .Include(mp => mp.Platillo)
            .AsQueryable();

        if (menuGlobalId.HasValue)
        {
            query = query.Where(mp => mp.MenuGlobalId == menuGlobalId.Value);
        }

        return await query
            .OrderBy(mp => mp.DiaSemana)
            .ThenBy(mp => mp.TipoComida)
            .ToListAsync();
    }

    public async Task<MenuPlatillo?> GetByIdAsync(int id)
    {
        return await _context.MenuPlatillos
            .Include(mp => mp.Platillo)
            .FirstOrDefaultAsync(mp => mp.Id == id);
    }

    public async Task<MenuPlatillo> AddAsync(MenuPlatillo menuPlatillo)
    {
        await _context.MenuPlatillos.AddAsync(menuPlatillo);
        await _context.SaveChangesAsync();
        // Cargamos el platillo para poder devolver su nombre en la respuesta
        menuPlatillo.Platillo = (await _context.Platillos.FindAsync(menuPlatillo.PlatilloId))!;
        return menuPlatillo;
    }

    public async Task<MenuPlatillo> UpdateAsync(MenuPlatillo menuPlatillo)
    {
        _context.MenuPlatillos.Update(menuPlatillo);
        await _context.SaveChangesAsync();
        // Si cambió el platillo, refrescamos la referencia para devolver el nombre correcto
        menuPlatillo.Platillo = (await _context.Platillos.FindAsync(menuPlatillo.PlatilloId))!;
        return menuPlatillo;
    }

    public async Task DeleteAsync(MenuPlatillo menuPlatillo)
    {
        _context.MenuPlatillos.Remove(menuPlatillo);
        await _context.SaveChangesAsync();
    }
}
