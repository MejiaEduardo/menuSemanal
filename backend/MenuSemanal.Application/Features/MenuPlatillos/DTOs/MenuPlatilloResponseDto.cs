using MenuSemanal.Domain.Enums;

namespace MenuSemanal.Application.Features.MenuPlatillos.DTOs;

public class MenuPlatilloResponseDto
{
    public int Id { get; set; }
    public int MenuGlobalId { get; set; }
    public int PlatilloId { get; set; }
    public string NombrePlatillo { get; set; } = string.Empty;
    public DiaSemana DiaSemana { get; set; }
    public TipoComida TipoComida { get; set; }
}