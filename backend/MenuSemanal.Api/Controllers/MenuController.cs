using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MenuSemanal.Application.Interfaces;
using MenuSemanal.Application.Features.Menus.DTOs;
using MenuSemanal.Domain.Entities;

namespace MenuSemanal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuRepository _repository;
    private readonly IValidator<CreateMenuRequestDto> _createValidator;
    private readonly IValidator<UpdateMenuRequestDto> _updateValidator;

    public MenuController(
        IMenuRepository repository,
        IValidator<CreateMenuRequestDto> createValidator,
        IValidator<UpdateMenuRequestDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var menus = await _repository.GetAllAsync();
        return Ok(menus.Select(ToResponse));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenu(int id)
    {
        var menu = await _repository.GetByIdAsync(id);
        return menu is null ? NotFound() : Ok(ToResponse(menu));
    }

    [HttpPost]
    public async Task<IActionResult> CrearMenu([FromBody] CreateMenuRequestDto request)
    {
        var resultado = await _createValidator.ValidateAsync(request);
        if (!resultado.IsValid)
        {
            return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));
        }

        var menu = new MenuGlobal { Nombre = request.Nombre ?? string.Empty };
        var nuevoMenu = await _repository.AddAsync(menu);

        return CreatedAtAction(nameof(GetMenu), new { id = nuevoMenu.Id }, ToResponse(nuevoMenu));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarMenu(int id, [FromBody] UpdateMenuRequestDto request)
    {
        var resultado = await _updateValidator.ValidateAsync(request);
        if (!resultado.IsValid)
        {
            return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));
        }

        var menu = await _repository.GetByIdAsync(id);
        if (menu is null) return NotFound();

        menu.Nombre = request.Nombre ?? string.Empty;
        var menuActualizado = await _repository.UpdateAsync(menu);

        return Ok(ToResponse(menuActualizado));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarMenu(int id)
    {
        var menu = await _repository.GetByIdAsync(id);
        if (menu is null) return NotFound();

        await _repository.DeleteAsync(menu);
        return NoContent();
    }

    private static MenuResponseDto ToResponse(MenuGlobal menu) => new()
    {
        Id = menu.Id,
        Nombre = menu.Nombre
    };
}