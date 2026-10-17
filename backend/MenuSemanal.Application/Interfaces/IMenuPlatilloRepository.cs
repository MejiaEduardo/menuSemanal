using MenuSemanal.Domain.Entities;

namespace MenuSemanal.Application.Interfaces;

public interface IMenuPlatilloRepository
{
    Task<IEnumerable<MenuPlatillo>> GetAllAsync(int? menuGlobalId = null);
    Task<MenuPlatillo?> GetByIdAsync(int id);
    Task<MenuPlatillo> AddAsync(MenuPlatillo menuPlatillo);
    Task<MenuPlatillo> UpdateAsync(MenuPlatillo menuPlatillo);
    Task DeleteAsync(MenuPlatillo menuPlatillo);
}
