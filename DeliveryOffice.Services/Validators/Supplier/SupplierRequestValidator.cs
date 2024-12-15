using DeliveryOffice.Core.RequestModels;
using FluentValidation;

namespace DeliveryOffice.Services.Validators.Supplier;

/// <summary>
///     Содержит правила валидации для модели <see cref="SupplierRequest" />
/// </summary>
public class SupplierRequestValidator : AbstractValidator<SupplierRequest>
{
    private const int MaximumLength = 255;

    public SupplierRequestValidator()
    {
        RuleFor(supplier => supplier.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

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
