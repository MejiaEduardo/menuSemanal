
using MenuSemanal.Domain.Entities;

namespace MenuSemanal.Application.Interfaces;

public interface IMenuRepository
{
    Task<IEnumerable<MenuGlobal>> GetAllAsync();
	Task<MenuGlobal> AddAsync(MenuGlobal menu); // agregamos el contrato para el post>
}