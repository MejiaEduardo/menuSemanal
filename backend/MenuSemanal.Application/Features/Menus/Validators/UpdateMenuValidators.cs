using FluentValidation;
using MenuSemanal.Application.Features.Menus.DTOs;

namespace MenuSemanal.Application.Feactures.Menus.Validators;

public class UpdateMenuValidators : AbstractValidator<UpdateMenuRequestDto>
{
    public UpdateMenuValidators()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre no puede estar vacio")
            .MinimumLength(3).WithMessage("El menú debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El nombre del menu es demasiado largo");
    }
}
