using FluentValidation;
using MenuSemanal.Application.Features.MenuPlatillos.DTOs;

namespace MenuSemanal.Application.Features.MenuPlatillos.Validators;

public class CreateMenuPlatilloValidators : AbstractValidator<CreateMenuPlatilloRequestDto>
{
    public CreateMenuPlatilloValidators()
    {
        RuleFor(x => x.MenuGlobalId).GreaterThan(0).WithMessage("Debes indicar un menú válido.");
        RuleFor(x => x.PlatilloId).GreaterThan(0).WithMessage("Debes indicar un platillo válido.");
        RuleFor(x => x.DiaSemana).IsInEnum().WithMessage("El día de la semana no es válido.");
        RuleFor(x => x.TipoComida).IsInEnum().WithMessage("El tipo de comida no es válido.");
    }
}