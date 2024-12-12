using DeliveryOffice.Core.RequestModels;
using FluentValidation;

namespace DeliveryOffice.API.Validators.Supplier;

/// <summary>
///     Содержит правила валидации для модели <see cref="CreateSupplierRequest" />
/// </summary>
public class CreateSupplierRequestValidator : AbstractValidator<CreateSupplierRequest>
{
    public CreateSupplierRequestValidator()
    {
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
