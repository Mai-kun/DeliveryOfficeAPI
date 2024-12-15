using DeliveryOffice.API.Common.Abstractions;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;

namespace DeliveryOffice.DataAccess.Repositories;

public class SupplierWriteRepository : BaseWriteDbRepository<Supplier>, ISupplierWriterRepository
{
    public SupplierWriteRepository(IDbWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider)
    {
    }
}
