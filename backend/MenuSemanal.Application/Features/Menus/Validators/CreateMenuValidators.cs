using FluentValidation;
using MenuSemanal.Application.Features.Menus.DTOs;

namespace MenuSemanal.Application.Features.Menus.Validators;

public class CreateMenuValidators : AbstractValidator<CreateMenuRequestDto>
{
    public CreateMenuValidators()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del hábito no puede estar vacío.")
            .MinimumLength(3).WithMessage("El hábito debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El hábito es demasiado largo.");
    }
}