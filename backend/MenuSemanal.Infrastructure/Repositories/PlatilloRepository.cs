using Microsoft.EntityFrameworkCore;
using MenuSemanal.Application.Interfaces;
using MenuSemanal.Domain.Entities;
using MenuSemanal.Infrastructure.Data;

namespace MenuSemanal.Infrastructure.Repositories;

public class PlatilloRepository : IPlatilloRepository
{
    private readonly MenuSemanalDbContext _context;

    public PlatilloRepository(MenuSemanalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Platillo>> GetAllAsync()
    {
        return await _context.Platillos.AsNoTracking().ToListAsync();
    }

    public async Task<Platillo?> GetByIdAsync(int id)
    {
        return await _context.Platillos.FindAsync(id);
    }

    public async Task<Platillo> AddAsync(Platillo platillo)
    {
        await _context.Platillos.AddAsync(platillo);
        await _context.SaveChangesAsync();
        return platillo;
    }

    public async Task<Platillo> UpdateAsync(Platillo platillo)
    {
        _context.Platillos.Update(platillo);
        await _context.SaveChangesAsync();
        return platillo;
    }

    public async Task DeleteAsync(Platillo platillo)
    {
        _context.Platillos.Remove(platillo);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EstaEnUsoAsync(int id)
    {
        return await _context.MenuPlatillos.AnyAsync(mp => mp.PlatilloId == id);
    }
}
