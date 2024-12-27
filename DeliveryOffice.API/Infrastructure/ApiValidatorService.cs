﻿using DeliveryOffice.Services.Abstractions.RequestModels;
using DeliveryOffice.Services.ServiceExceptions;
using DeliveryOffice.Services.Validators;
using DeliveryOffice.Services.Validators.Bill;
using DeliveryOffice.Services.Validators.Buyer;
using DeliveryOffice.Services.Validators.Product;
using DeliveryOffice.Services.Validators.Supplier;
using FluentValidation;

namespace DeliveryOffice.API.Infrastructure;

/// <inheritdoc />
public class ApiValidatorService : IValidatorService
{
    private readonly Dictionary<Type, IValidator> validators = new();

    public ApiValidatorService()
    {
        validators.Add(typeof(SupplierRequest), new SupplierRequestValidator());
        validators.Add(typeof(CreateSupplierRequest), new CreateSupplierRequestValidator());
        validators.Add(typeof(ProductRequest), new ProductRequestValidator());
        validators.Add(typeof(CreateProductRequest), new CreateProductRequestValidator());
        validators.Add(typeof(BuyerRequest), new BuyerRequestValidator());
        validators.Add(typeof(CreateBuyerRequest), new CreateBuyerRequestValidator());
        validators.Add(typeof(BillRequest), new BillRequestValidator());
        validators.Add(typeof(CreateBillRequest), new CreateBillRequestValidator());
    }

    void IValidatorService.Validate<TModel>(TModel model)
    {
        var validator = typeof(TModel);
        if (!validators.TryGetValue(validator, out var validatorInstance))
        {
            return;
        }

        var validationContext = new ValidationContext<TModel>(model);
        var result = validatorInstance.Validate(validationContext);

        if (!result.IsValid)
        {
            throw new ModelValidationException(
                result.Errors.Select(x => new KeyValuePair<string, string>(x.PropertyName, x.ErrorMessage)));
        }
    }
}
