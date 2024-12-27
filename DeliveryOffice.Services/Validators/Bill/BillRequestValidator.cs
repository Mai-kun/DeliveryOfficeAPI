using DeliveryOffice.Services.Abstractions.RequestModels;
using FluentValidation;

namespace DeliveryOffice.Services.Validators.Bill;

/// <summary>
///     Содержит правила валидации для модели <see cref="BillRequest" />
/// </summary>
public class BillRequestValidator : AbstractValidator<BillRequest>
{
    private const int MaximumLength = 255;
    private const int TotalAmountMinimumValue = 0;

    public BillRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

        RuleFor(b => b.Warehouse)
            .NotNull()
            .NotEmpty()
            .MaximumLength(MaximumLength)
            .WithMessage($"Warehouse is required and must not exceed {MaximumLength} characters.");

        RuleFor(b => b.TotalAmount)
            .GreaterThan(TotalAmountMinimumValue)
            .WithMessage($"Total amount must be greater than {TotalAmountMinimumValue}.");
    }
}
