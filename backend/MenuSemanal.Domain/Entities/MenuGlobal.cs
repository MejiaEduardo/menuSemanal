namespace MenuSemanal.Domain.Entities;

public class MenuGlobal
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public ICollection<MenuPlatillo> MenuPlatillos { get; set; } = new List<MenuPlatillo>();
}