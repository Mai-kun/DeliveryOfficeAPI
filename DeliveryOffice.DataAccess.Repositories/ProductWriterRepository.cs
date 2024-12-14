using DeliveryOffice.API.Common.Abstractions;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;

namespace DeliveryOffice.DataAccess.Repositories;

public class ProductWriterRepository : BaseWriteDbRepository<Product>, IProductWriterRepository
{
    public ProductWriterRepository(IDbWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider)
    {
    }
}
