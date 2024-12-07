using DeliveryOffice.Core.Abstractions.Repositories;
using DeliveryOffice.Core.Abstractions.Services;
using DeliveryOffice.DataAccess.Repositories;
using DeliveryOffice.Services;

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