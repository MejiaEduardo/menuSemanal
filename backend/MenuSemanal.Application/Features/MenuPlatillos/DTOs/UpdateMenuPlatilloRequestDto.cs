using MenuSemanal.Domain.Enums;

namespace MenuSemanal.Application.Features.MenuPlatillos.DTOs;

public class UpdateMenuPlatilloRequestDto
{
    public int MenuGlobalId { get; set; }
    public int PlatilloId { get; set; }
    public DiaSemana DiaSemana { get; set; }
    public TipoComida TipoComida { get; set; }
}