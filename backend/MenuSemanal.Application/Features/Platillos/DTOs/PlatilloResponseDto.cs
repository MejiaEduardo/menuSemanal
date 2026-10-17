namespace MenuSemanal.Application.Features.Platillos.DTOs;

public class PlatilloResponseDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
}