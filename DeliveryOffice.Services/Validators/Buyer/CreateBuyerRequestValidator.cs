using DeliveryOffice.Services.Abstractions.RequestModels;
using FluentValidation;

namespace DeliveryOffice.Services.Validators.Buyer;

/// <summary>
///     Содержит правила валидации для модели <see cref="CreateBuyerRequest" />
/// </summary>
public class CreateBuyerRequestValidator : AbstractValidator<CreateBuyerRequest>
{
    private const int MaximumLength = 255;

    public CreateBuyerRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(MaximumLength)
            .WithMessage($"Name is required and must not exceed {MaximumLength} characters.");
    }
}
