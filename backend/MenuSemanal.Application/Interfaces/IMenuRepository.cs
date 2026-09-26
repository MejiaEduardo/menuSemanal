
using MenuSemanal.Domain.Entities;
namespace MenuSemanal.Application.Interfaces;

public interface IMenuRepository
{
    Task<IEnumerable<menuGlobal>> GetAllAsync();
}