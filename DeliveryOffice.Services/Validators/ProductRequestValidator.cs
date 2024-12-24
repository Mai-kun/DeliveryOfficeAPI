﻿using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using FluentValidation;

namespace DeliveryOffice.Services.Validators;

/// <summary>
///     Содержит правила валидации для модели <see cref="ProductRequest" />
/// </summary>
public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    private const int NameMaximumLength = 255;
    private const int UnitMaximumLength = 50;
    private const int QuantityMinimumValue = 0;
    private const int PriceMinimumValue = 0;

    public ProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(NameMaximumLength)
            .WithMessage($"Product name must be less than {NameMaximumLength} characters");

        RuleFor(x => x.Quantity)
            .GreaterThan(QuantityMinimumValue)
            .WithMessage($"Quantity must be greater than {QuantityMinimumValue}");

        RuleFor(x => x.Unit)
            .NotEmpty()
            .NotNull()
            .MaximumLength(UnitMaximumLength)
            .WithMessage($"Unit must be less than {UnitMaximumLength} characters");

        RuleFor(x => x.Price)
            .GreaterThan(PriceMinimumValue)
            .WithMessage($"Price must be greater than {PriceMinimumValue}");
    }
}
