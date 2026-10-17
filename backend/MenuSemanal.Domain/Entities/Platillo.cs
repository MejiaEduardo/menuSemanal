namespace MenuSemanal.Domain.Entities;

public class Platillo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }

    public ICollection<MenuPlatillo> MenuPlatillos { get; set; } = new List<MenuPlatillo>();
}