using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MenuSemanal.Application.Interfaces;
using MenuSemanal.Application.Features.MenuPlatillos.DTOs;
using MenuSemanal.Domain.Entities;

namespace MenuSemanal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuPlatilloController : ControllerBase
{
    private readonly IMenuPlatilloRepository _repository;
    private readonly IMenuRepository _menuRepository;
    private readonly IPlatilloRepository _platilloRepository;
    private readonly IValidator<CreateMenuPlatilloRequestDto> _createValidator;
    private readonly IValidator<UpdateMenuPlatilloRequestDto> _updateValidator;

    public MenuPlatilloController(
        IMenuPlatilloRepository repository,
        IMenuRepository menuRepository,
        IPlatilloRepository platilloRepository,
        IValidator<CreateMenuPlatilloRequestDto> createValidator,
        IValidator<UpdateMenuPlatilloRequestDto> updateValidator)
    {
        _repository = repository;
        _menuRepository = menuRepository;
        _platilloRepository = platilloRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    // GET api/menuplatillo            -> todos los renglones
    // GET api/menuplatillo?menuId=1   -> solo los renglones del menú 1 (el menú semanal armado)
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? menuId)
    {
        var items = await _repository.GetAllAsync(menuId);
        return Ok(items.Select(ToResponse));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuPlatillo(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(ToResponse(item));
    }

    [HttpPost]
    public async Task<IActionResult> CrearMenuPlatillo([FromBody] CreateMenuPlatilloRequestDto request)
    {
        var resultado = await _createValidator.ValidateAsync(request);
        if (!resultado.IsValid)
        {
            return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));
        }

        if (await _menuRepository.GetByIdAsync(request.MenuGlobalId) is null)
            return BadRequest("El menú indicado no existe.");

        if (await _platilloRepository.GetByIdAsync(request.PlatilloId) is null)
            return BadRequest("El platillo indicado no existe.");

        var item = new MenuPlatillo
        {
            MenuGlobalId = request.MenuGlobalId,
            PlatilloId = request.PlatilloId,
            DiaSemana = request.DiaSemana,
            TipoComida = request.TipoComida
        };

        var nuevoItem = await _repository.AddAsync(item);

        return CreatedAtAction(nameof(GetMenuPlatillo), new { id = nuevoItem.Id }, ToResponse(nuevoItem));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarMenuPlatillo(int id, [FromBody] UpdateMenuPlatilloRequestDto request)
    {
        var resultado = await _updateValidator.ValidateAsync(request);
        if (!resultado.IsValid)
        {
            return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));
        }

        var item = await _repository.GetByIdAsync(id);
        if (item is null) return NotFound();

        if (await _menuRepository.GetByIdAsync(request.MenuGlobalId) is null)
            return BadRequest("El menú indicado no existe.");

        if (await _platilloRepository.GetByIdAsync(request.PlatilloId) is null)
            return BadRequest("El platillo indicado no existe.");

        item.MenuGlobalId = request.MenuGlobalId;
        item.PlatilloId = request.PlatilloId;
        item.DiaSemana = request.DiaSemana;
        item.TipoComida = request.TipoComida;

        var itemActualizado = await _repository.UpdateAsync(item);

        return Ok(ToResponse(itemActualizado));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarMenuPlatillo(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item is null) return NotFound();

        await _repository.DeleteAsync(item);

        return NoContent();
    }

    private static MenuPlatilloResponseDto ToResponse(MenuPlatillo item) => new()
    {
        Id = item.Id,
        MenuGlobalId = item.MenuGlobalId,
        PlatilloId = item.PlatilloId,
        NombrePlatillo = item.Platillo?.Nombre ?? string.Empty,
        DiaSemana = item.DiaSemana,
        TipoComida = item.TipoComida
    };
}
