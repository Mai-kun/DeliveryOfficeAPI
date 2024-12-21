using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.Validators.Bill;
using FluentValidation.TestHelper;
using Xunit;

namespace DeliveryOffice.Services.Tests.Validators;

/// <summary>
///     Тесты для <see cref="CreateBillRequestValidator" />
/// </summary>
public class CreateBillRequestValidatorTests
{
    private readonly CreateBillRequestValidator validator = new();

    /// <summary>
    ///     Получение ошибки при превышении ограничения строки
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForMaximumLength()
    {
        var model = new CreateBillRequest
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
        var model = new CreateBillRequest
        {
            Warehouse = null,
            Products = null,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Warehouse);
        result.ShouldHaveValidationErrorFor(member => member.Products);
    }

    /// <summary>
    ///     Получение ошибки при пустои значении
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForEmpty()
    {
        var model = new CreateBillRequest
        {
            Warehouse = string.Empty,
            Products = new List<Guid>(),
            BuyerId = Guid.Empty,
            SupplierId = Guid.Empty,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Warehouse);
        result.ShouldHaveValidationErrorFor(member => member.Products);
        result.ShouldHaveValidationErrorFor(member => member.BuyerId);
        result.ShouldHaveValidationErrorFor(member => member.SupplierId);
    }

    /// <summary>
    ///     Получение ошибки на минимальное ограничение строки
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForMinimumValue()
    {
        var model = new CreateBillRequest
        {
            TotalAmount = -5,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.TotalAmount);
    }

    /// <summary>
    ///     Отсутствие ошибки при корректных данных
    /// </summary>
    [Fact]
    public void ShouldHaveNoErrors()
    {
        var model = new CreateBillRequest
        {
            Warehouse = "Nonmassa",
            TotalAmount = 321,
            Products = new List<Guid>()
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
            },
            BuyerId = Guid.NewGuid(),
            SupplierId = Guid.NewGuid(),
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(member => member.Warehouse);
        result.ShouldNotHaveValidationErrorFor(member => member.TotalAmount);
        result.ShouldNotHaveValidationErrorFor(member => member.Products);
        result.ShouldNotHaveValidationErrorFor(member => member.BuyerId);
        result.ShouldNotHaveValidationErrorFor(member => member.SupplierId);
    }
}
