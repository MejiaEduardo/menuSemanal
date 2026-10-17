using Microsoft.EntityFrameworkCore;
using MenuSemanal.Application.Interfaces;
using MenuSemanal.Domain.Entities;
using MenuSemanal.Infrastructure.Data;

namespace MenuSemanal.Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly MenuSemanalDbContext _context;

    public MenuRepository(MenuSemanalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MenuGlobal>> GetAllAsync()
    {
        return await _context.MenusGlobales.AsNoTracking().ToListAsync();
    }

    public async Task<MenuGlobal?> GetByIdAsync(int id)
    {
        return await _context.MenusGlobales.FirstOrDefaultAsync(m => m.Id == id);
    }
    
    
    public async Task<MenuGlobal> AddAsync(MenuGlobal menu)
    {
        await _context.MenusGlobales.AddAsync(menu);
        await _context.SaveChangesAsync();
        return menu;
    }
    
    public async Task<MenuGlobal> UpdateAsync(MenuGlobal menu)
    {
        _context.MenusGlobales.Update(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task DeleteAsync(MenuGlobal menu)
    {
        _context.MenusGlobales.Remove(menu);
        await _context.SaveChangesAsync();
    }
}
