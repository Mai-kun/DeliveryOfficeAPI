using DeliveryOffice.API.Common.Abstractions;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;

namespace DeliveryOffice.DataAccess.Repositories;

/// <inheritdoc cref="IBuyerWriterRepository" />
public class BuyerWriterRepository : BaseWriteDbRepository<Buyer>, IBuyerWriterRepository
{
    public BuyerWriterRepository(IDbWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider)
    {
    }
}
