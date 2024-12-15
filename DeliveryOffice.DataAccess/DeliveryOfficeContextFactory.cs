using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DeliveryOffice.DataAccess;

public class DeliveryOfficeContextFactory : IDesignTimeDbContextFactory<DeliveryOfficeDbContext>
{
    public DeliveryOfficeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DeliveryOfficeDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=DeliveryOfficeDB;Trusted_Connection=True;TrustServerCertificate=True");

        return new DeliveryOfficeDbContext(optionsBuilder.Options);
    }
}
