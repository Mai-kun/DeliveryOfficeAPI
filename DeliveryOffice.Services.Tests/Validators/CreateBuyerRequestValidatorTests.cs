using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.Validators.Buyer;
using FluentValidation.TestHelper;
using Xunit;

namespace DeliveryOffice.Services.Tests.Validators;

/// <summary>
///     Тесты для <see cref="CreateBuyerRequestValidator" />
/// </summary>
public class CreateBuyerRequestValidatorTests
{
    private readonly CreateBuyerRequestValidator validator = new();

    /// <summary>
    ///     Получение ошибки при превышении ограничения строки
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForMaximumLength()
    {
        var model = new CreateBuyerRequest
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
        var model = new CreateBuyerRequest
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
        var model = new CreateBuyerRequest
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
        var model = new CreateBuyerRequest
        {
            Name = "Platonist",
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(member => member.Name);
    }
}
