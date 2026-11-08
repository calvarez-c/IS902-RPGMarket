using FluentValidation;
using RPGMarket.Application.Features.Items.DTOs;

namespace RPGMarket.Application.Features.Items.Validators;

public class CreateItemValidator : AbstractValidator<CreateItemRequestDto>
{
    public CreateItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del objeto no puede estar vacío.")
            .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El nombre no debe superar 100 caracteres.");

        RuleFor(x => x.Category)
            .Must(c => new[] { "Arma", "Armadura", "Consumible", "Material" }.Contains(c))
            .WithMessage("La categoría debe ser Arma, Armadura, Consumible o Material.");

        RuleFor(x => x.Rarity)
            .Must(r => new[] { "Común", "Raro", "Épico", "Legendario" }.Contains(r))
            .WithMessage("La rareza debe ser Común, Raro, Épico o Legendario.");

        RuleFor(x => x.BasePrice)
            .GreaterThan(0).WithMessage("El precio base debe ser mayor a cero.");
    }
}
