using MenuSemanal.Domain.Entities;

namespace MenuSemanal.Application.Interfaces;

public interface IPlatilloRepository
{
    Task<IEnumerable<Platillo>> GetAllAsync();
    Task<Platillo?> GetByIdAsync(int id);
    Task<Platillo> AddAsync(Platillo platillo);
    Task<Platillo> UpdateAsync(Platillo platillo);
    Task DeleteAsync(Platillo platillo);
    Task<bool> EstaEnUsoAsync(int id); // true si algún menú ya usa este platillo
}