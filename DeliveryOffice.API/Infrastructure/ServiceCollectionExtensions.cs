using DeliveryOffice.Core.Abstractions.Repositories;
using DeliveryOffice.Core.Abstractions.Services;
using DeliveryOffice.DataAccess.Repositories;
using DeliveryOffice.Services;

namespace DeliveryOffice.API.Infrastructure;

/// <summary>
///     Предоставялется коллекцию сервисов для их регистрации
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Регистрирует сервисы
    /// </summary>
    public static void AddDependence(this IServiceCollection services)
    {
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ISuppliersService, SuppliersService>();

        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddScoped<IValidatorService, ApiValidatorService>();
    }
}
