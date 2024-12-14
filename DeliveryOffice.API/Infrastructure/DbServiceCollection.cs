using DeliveryOffice.DataAccess;
using DeliveryOffice.DataAccess.Abstractions;

namespace DeliveryOffice.API.Infrastructure;

public static class DbServiceCollection
{
    public static void AddDataBaseDependencies(this IServiceCollection services)
    {
        services.AddScoped<IDbWriter>(c => c.GetRequiredService<DeliveryOfficeDbContext>());
        services.AddScoped<IDbReader>(c => c.GetRequiredService<DeliveryOfficeDbContext>());
        services.AddScoped<IUnitOfWork>(c => c.GetRequiredService<DeliveryOfficeDbContext>());
    }
}
