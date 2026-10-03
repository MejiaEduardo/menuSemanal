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

    public async Task<MenuGlobal> AddAsync(MenuGlobal menu)
    {
        await _context.MenusGlobales.AddAsync(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<IEnumerable<MenuGlobal>> GetAllAsync()
    {
        return await _context.MenusGlobales.ToListAsync();
    }
}