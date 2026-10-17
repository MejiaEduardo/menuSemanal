using MenuSemanal.Domain.Enums;

namespace MenuSemanal.Domain.Entities;

// Tabla puente: dice QUÉ platillo se come, en QUÉ menú, QUÉ día y en QUÉ comida.
public class MenuPlatillo
{
    public int Id { get; set; }

    public int MenuGlobalId { get; set; }
    public MenuGlobal MenuGlobal { get; set; } = null!;

    public int PlatilloId { get; set; }
    public Platillo Platillo { get; set; } = null!;

    public DiaSemana DiaSemana { get; set; }
    public TipoComida TipoComida { get; set; }
}