using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DeliveryOffice.DataAccess;

public class DeliveryOfficeContextFactory : IDesignTimeDbContextFactory<DeliveryOfficeDbContext>
{
    public DeliveryOfficeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DeliveryOfficeDbContext>();
        optionsBuilder.UseSqlServer(
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DeliveryOfficeDB;Integrated Security=True;");

        return new DeliveryOfficeDbContext(optionsBuilder.Options);
    }
}
