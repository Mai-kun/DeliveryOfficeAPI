using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using FluentValidation;

namespace DeliveryOffice.Services.Validators.Bill;

/// <summary>
///     Содержит правила валидации для модели <see cref="CreateBillRequest" />
/// </summary>
public class CreateBillRequestValidator : AbstractValidator<CreateBillRequest>
{
    private const int MaximumLength = 255;
    private const int TotalAmountMinimumValue = 0;

    public CreateBillRequestValidator()
    {
        RuleFor(b => b.Warehouse)
            .NotNull()
            .NotEmpty()
            .MaximumLength(MaximumLength)
            .WithMessage($"Warehouse is required and must not exceed {MaximumLength} characters.");

        RuleFor(b => b.TotalAmount)
            .GreaterThan(TotalAmountMinimumValue)
            .WithMessage($"Total amount must be greater than {TotalAmountMinimumValue}.");

        RuleFor(b => b.Products)
            .NotNull()
            .NotEmpty()
            .WithMessage("Products is required (can't be null or empty).");

        RuleFor(b => b.BuyerId)
            .NotEmpty()
            .WithMessage("BuyerId is required (can't be null or empty).");

        RuleFor(b => b.SupplierId)
            .NotEmpty()
            .WithMessage("SupplierId is required (can't be null or empty).");
    }
}
