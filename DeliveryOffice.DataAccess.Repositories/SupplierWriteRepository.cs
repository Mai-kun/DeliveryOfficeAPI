﻿using DeliveryOffice.API.Common.Abstractions;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;

namespace DeliveryOffice.DataAccess.Repositories;

/// <inheritdoc cref="ISupplierWriterRepository" />
public class SupplierWriterRepository : BaseWriteDbRepository<Supplier>, ISupplierWriterRepository
{
    public SupplierWriterRepository(IDbWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider)
    {
    }
}
