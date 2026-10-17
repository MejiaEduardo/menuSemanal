
using MenuSemanal.Domain.Entities;

namespace MenuSemanal.Application.Interfaces;

public interface IMenuRepository
{
    Task<IEnumerable<MenuGlobal>> GetAllAsync();
	Task<MenuGlobal> AddAsync(MenuGlobal menu); // agregamos el contrato para el post>
	Task<MenuGlobal?> GetByIdAsync(int id);
	Task<MenuGlobal> UpdateAsync(MenuGlobal menu);
	Task  DeleteAsync(MenuGlobal menu);
}