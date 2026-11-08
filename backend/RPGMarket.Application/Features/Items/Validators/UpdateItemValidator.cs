using FluentValidation;
using RPGMarket.Application.Features.Items.DTOs;

namespace RPGMarket.Application.Features.Items.Validators;

public class UpdateItemValidator : AbstractValidator<UpdateItemRequestDto>
{
    public UpdateItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del ítem no puede estar vacío.")
            .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("La categoría es requerida.")
            .MaximumLength(50).WithMessage("La categoría no puede superar los 50 caracteres.");

        RuleFor(x => x.Rarity)
            .NotEmpty().WithMessage("La rareza es requerida.")
            .MaximumLength(30).WithMessage("La rareza no puede superar los 30 caracteres.");

        RuleFor(x => x.BasePrice)
            .GreaterThan(0).WithMessage("El precio base debe ser mayor a 0.");

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500).WithMessage("La URL de la imagen no puede exceder los 500 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.ImageUrl));
    }
}