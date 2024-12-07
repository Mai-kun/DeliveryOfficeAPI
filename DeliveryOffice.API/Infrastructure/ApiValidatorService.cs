using DeliveryOffice.API.Validators.Supplier;
using DeliveryOffice.Services.ServiceExceptions;
using FluentValidation;

namespace DeliveryOffice.API.Infrastructure;

/// <inheritdoc />
public class ApiValidatorService : IValidatorService
{
    private readonly Dictionary<Type, IValidator> validators = new();

    public ApiValidatorService()
    {
        validators.Add(typeof(CreateSupplierRequestValidator), new CreateSupplierRequestValidator());
        validators.Add(typeof(SupplierRequestValidator), new SupplierRequestValidator());
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
