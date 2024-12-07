using DeliveryOffice.Core.RequestModels;
using FluentValidation;

namespace DeliveryOffice.API.Validators.Supplier;

/// <summary>
///     Содержит правила валидации для модели <see cref="SupplierRequest" />
/// </summary>
public class SupplierRequestValidator : AbstractValidator<SupplierRequest>
{
    public SupplierRequestValidator()
    {
        RuleFor(supplierRequest => supplierRequest.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

        Include(new CreateSupplierRequestValidator());
    }
}
