using DeliveryOffice.API.Common;
using DeliveryOffice.API.Common.Abstractions;
using DeliveryOffice.DataAccess.Repositories;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using DeliveryOffice.Services;
using DeliveryOffice.Services.Abstractions;

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
        services.AddScoped<ISupplierReaderRepository, SupplierReaderRepository>();
        services.AddScoped<ISupplierWriterRepository, SupplierWriteRepository>();
        services.AddScoped<ISuppliersService, SuppliersService>();

        services.AddScoped<IProductReaderRepository, ProductReaderRepository>();
        services.AddScoped<IProductWriterRepository, ProductWriterRepository>();
        services.AddScoped<IProductsService, ProductsService>();

        services.AddScoped<IBuyerReaderRepository, BuyerReaderRepository>();
        services.AddScoped<IBuyerWriterRepository, BuyerWriterRepository>();
        services.AddScoped<IBuyerService, BuyerService>();

        services.AddAutoMapper(typeof(AutoMapperResponseProfile), typeof(AutoMapperRequestProfile));
        services.AddScoped<IValidatorService, ApiValidatorService>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

    }
}
