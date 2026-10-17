using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MenuSemanal.Application.Interfaces;
using MenuSemanal.Application.Features.Platillos.DTOs;
using MenuSemanal.Domain.Entities;

namespace MenuSemanal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatilloController : ControllerBase
{
    private readonly IPlatilloRepository _repository;
    private readonly IValidator<CreatePlatilloRequestDto> _createValidator;
    private readonly IValidator<UpdatePlatilloRequestDto> _updateValidator;

    public PlatilloController(
        IPlatilloRepository repository,
        IValidator<CreatePlatilloRequestDto> createValidator,
        IValidator<UpdatePlatilloRequestDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var platillos = await _repository.GetAllAsync();
        return Ok(platillos.Select(ToResponse));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlatillo(int id)
    {
        var platillo = await _repository.GetByIdAsync(id);
        return platillo is null ? NotFound() : Ok(ToResponse(platillo));
    }

    [HttpPost]
    public async Task<IActionResult> CrearPlatillo([FromBody] CreatePlatilloRequestDto request)
    {
        var resultado = await _createValidator.ValidateAsync(request);
        if (!resultado.IsValid)
        {
            return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));
        }

        var platillo = new Platillo
        {
            Nombre = request.Nombre ?? string.Empty,
            Descripcion = request.Descripcion
        };

        var nuevoPlatillo = await _repository.AddAsync(platillo);

        return CreatedAtAction(nameof(GetPlatillo), new { id = nuevoPlatillo.Id }, ToResponse(nuevoPlatillo));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarPlatillo(int id, [FromBody] UpdatePlatilloRequestDto request)
    {
        var resultado = await _updateValidator.ValidateAsync(request);
        if (!resultado.IsValid)
        {
            return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));
        }

        var platillo = await _repository.GetByIdAsync(id);
        if (platillo is null) return NotFound();

        platillo.Nombre = request.Nombre ?? string.Empty;
        platillo.Descripcion = request.Descripcion;

        var platilloActualizado = await _repository.UpdateAsync(platillo);

        return Ok(ToResponse(platilloActualizado));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarPlatillo(int id)
    {
        var platillo = await _repository.GetByIdAsync(id);
        if (platillo is null) return NotFound();

        if (await _repository.EstaEnUsoAsync(id))
        {
            return Conflict("No se puede eliminar el platillo porque ya está usado en uno o más menús.");
        }

        await _repository.DeleteAsync(platillo);

        return NoContent();
    }

    private static PlatilloResponseDto ToResponse(Platillo platillo) => new()
    {
        Id = platillo.Id,
        Nombre = platillo.Nombre,
        Descripcion = platillo.Descripcion
    };
}
