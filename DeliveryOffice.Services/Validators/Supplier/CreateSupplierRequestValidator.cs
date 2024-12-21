using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using FluentValidation;

namespace DeliveryOffice.Services.Validators.Supplier;

/// <summary>
///     Содержит правила валидации для модели <see cref="CreateSupplierRequest" />
/// </summary>
public class CreateSupplierRequestValidator : AbstractValidator<CreateSupplierRequest>
{
    private const int MaximumLength = 255;

    public CreateSupplierRequestValidator()
    {
        RuleFor(supplier => supplier.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(MaximumLength)
            .WithMessage($"Name is required and must not exceed {MaximumLength} characters.");

        RuleFor(supplier => supplier.Address)
            .NotEmpty()
            .NotNull()
            .WithMessage("Address is required.");
    }
}
