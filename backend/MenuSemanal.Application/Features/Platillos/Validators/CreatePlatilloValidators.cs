using FluentValidation;
using MenuSemanal.Application.Features.Platillos.DTOs;

namespace MenuSemanal.Application.Features.Platillos.Validators;

public class CreatePlatilloValidators : AbstractValidator<CreatePlatilloRequestDto>
{
    public CreatePlatilloValidators()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del platillo no puede estar vacío.")
            .MinimumLength(3).WithMessage("El platillo debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El nombre del platillo es demasiado largo.");

        RuleFor(x => x.Descripcion)
            .MaximumLength(500).WithMessage("La descripción es demasiado larga (máximo 500).");
    }
}