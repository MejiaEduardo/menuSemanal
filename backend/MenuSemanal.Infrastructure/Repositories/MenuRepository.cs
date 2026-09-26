using Microsoft.EntityFrameworkCore;
using MenuSemanal.Application.Interfaces;
using MenuSemanal.Domain.Entities;
using MenuSemanal.Infrastructure.Data;


namespace MenuSemanal.Infrastructure.Repositories;

public class MenuRepository : IMenuRepository {
    private readonly MenuSemanalDbContext _context;

    public MenuRepository(MenuSemanalDbContext context)
    {
        _context = context;   
    }

    // el constructor recibe el contexto de EF core
    public async Task<IEnumerable<menuGlobal>> GetAllAsync()
    {
        return await _context.MenuGlobal.ToListAsync();
    }
}