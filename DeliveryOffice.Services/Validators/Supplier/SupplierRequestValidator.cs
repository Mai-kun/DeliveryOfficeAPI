using DeliveryOffice.Core.RequestModels;
using FluentValidation;

namespace DeliveryOffice.Services.Validators.Supplier;

/// <summary>
///     Содержит правила валидации для модели <see cref="SupplierRequest" />
/// </summary>
public class SupplierRequestValidator : AbstractValidator<SupplierRequest>
{
    public SupplierRequestValidator()
    {
        RuleFor(supplier => supplier.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

        RuleFor(supplier => supplier.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .WithMessage("Name is required and must not exceed 255 characters.");

        RuleFor(supplier => supplier.Address)
            .NotEmpty()
            .NotNull()
            .WithMessage("Address is required.");
    }
}
