using DeliveryOffice.Core.Abstractions.Repositories;
using DeliveryOffice.DataAccess.Repositories;
using DeliveryOffice.Services;
using DeliveryOffice.Services.Contracts.Abstractions;

namespace DeliveryOffice.API.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddDependence(this IServiceCollection services)
    {
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ISuppliersService, SuppliersService>();

        services.AddAutoMapper(typeof(AutoMapperProfile));
    }
}
