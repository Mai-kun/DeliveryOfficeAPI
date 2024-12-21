using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using FluentValidation;

namespace DeliveryOffice.Services.Validators.Buyer;

public class BuyerRequestValidator : AbstractValidator<BuyerRequest>
{
    private const int MaximumLength = 255;

    public BuyerRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(MaximumLength)
            .WithMessage($"Name is required and must not exceed {MaximumLength} characters.");
    }
}
