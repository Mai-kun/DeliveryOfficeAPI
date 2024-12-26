using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.Validators.Bill;
using FluentValidation.TestHelper;
using Xunit;

namespace DeliveryOffice.Services.Tests.Validators;

/// <summary>
///     Тесты для <see cref="BillRequestValidator" />
/// </summary>
public class BillRequestValidatorTests
{
    private readonly BillRequestValidator validator = new();

    /// <summary>
    ///     Получение ошибки при превышении ограничения строки
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForMaximumLength()
    {
        var model = new BillRequest
        {
            Warehouse = new string('a', 300),
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Warehouse);
    }

    /// <summary>
    ///     Получение ошибки при работе с null
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForNull()
    {
        var model = new BillRequest
        {
            Warehouse = null,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Warehouse);
    }

    /// <summary>
    ///     Получение ошибки при пустои значении
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForEmpty()
    {
        var model = new BillRequest
        {
            Id = Guid.Empty,
            Warehouse = string.Empty,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Id);
        result.ShouldHaveValidationErrorFor(member => member.Warehouse);
    }

    /// <summary>
    ///     Отсутствие ошибки при корректных данных
    /// </summary>
    [Fact]
    public void ShouldHaveNoErrors()
    {
        var model = new BillRequest
        {
            Id = Guid.NewGuid(),
            Warehouse = "Nonmassa",
            TotalAmount = 321,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(member => member.Id);
        result.ShouldNotHaveValidationErrorFor(member => member.Warehouse);
        result.ShouldNotHaveValidationErrorFor(member => member.TotalAmount);
    }
}
