using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MenuSemanal.Application.Interfaces;
using MenuSemanal.Application.Features.Menus.DTOs;// el namespace real de tu DTO
using MenuSemanal.Domain.Entities;

namespace MenuSemanal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuRepository _repository;
    private readonly IValidator<CreateMenuRequestDto> _validator;

    public MenuController(IMenuRepository repository, IValidator<CreateMenuRequestDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var menus = await _repository.GetAllAsync();
        return Ok(menus);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenu(int id)
    {
        var menus = await _repository.GetAllAsync();
        var menu = menus.FirstOrDefault(m => m.Id == id);
        return menu is null ? NotFound() : Ok(menu);
    }

    [HttpPost]
    public async Task<IActionResult> CrearMenu([FromBody] CreateMenuRequestDto request)
    {
        var resultado = await _validator.ValidateAsync(request);
        if (!resultado.IsValid)
        {
            return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));
        }

        var menu = new MenuGlobal { Nombre = request.Nombre ?? string.Empty };

        var nuevoMenu = await _repository.AddAsync(menu);

        return CreatedAtAction(nameof(GetMenu), new { id = nuevoMenu.Id }, nuevoMenu);
    }
}