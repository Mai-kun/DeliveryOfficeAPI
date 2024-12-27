using DeliveryOffice.Services.Abstractions.RequestModels;
using DeliveryOffice.Services.Validators;
using DeliveryOffice.Services.Validators.Buyer;
using FluentValidation.TestHelper;
using Xunit;

namespace DeliveryOffice.Services.Tests.Validators;

/// <summary>
///     Тесты для <see cref="BuyerRequestValidator" />
/// </summary>
public class BuyerRequestValidatorTests
{
    private readonly BuyerRequestValidator validator = new();

    /// <summary>
    ///     Получение ошибки при превышении ограничения строки
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForMaximumLength()
    {
        var model = new BuyerRequest
        {
            Name = new string('a', 300),
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Name);
    }

    /// <summary>
    ///     Получение ошибки при работе с null
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForNull()
    {
        var model = new BuyerRequest
        {
            Name = null,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Name);
    }

    /// <summary>
    ///     Получение ошибки при пустой строки
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForEmpty()
    {
        var model = new BuyerRequest
        {
            Name = string.Empty,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Name);
    }

    /// <summary>
    ///     Отсутствие ошибки при корректных данных
    /// </summary>
    [Fact]
    public void ShouldHaveNoErrors()
    {
        var model = new BuyerRequest
        {
            Name = "Platonist",
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(member => member.Name);
    }
}
