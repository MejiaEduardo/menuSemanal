namespace MenuSemanal.Application.Features.Platillos.DTOs;

public class CreatePlatilloRequestDto
{
    public string? Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
}