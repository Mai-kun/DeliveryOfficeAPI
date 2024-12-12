using DeliveryOffice.Core.RequestModels;
using FluentValidation;

namespace DeliveryOffice.API.Validators.Product;

/// <summary>
///     Содержит правила валидации для модели <see cref="CreateProductRequest" />
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255)
            .WithMessage("Product name must be less than 255 characters");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.Unit)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50)
            .WithMessage("Unit must be less than 50 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}
