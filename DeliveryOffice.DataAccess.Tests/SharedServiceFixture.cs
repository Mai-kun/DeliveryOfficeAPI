using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Repositories;
using DeliveryOffice.Services;
using DeliveryOffice.Services.Abstractions;

namespace DeliveryOffice.DataAccess.Tests;

/// <summary>
///     Базовый класс фикстуры
/// </summary>
public abstract class SharedServiceFixture : BaseAppContextTest
{
    /// <inheritdoc cref="ISuppliersService"/>
    public ISuppliersService SuppliersService { get; }

    /// <inheritdoc cref="IProductsService"/>
    public IProductsService ProductsService { get; }

    /// <inheritdoc cref="IBuyerService"/>
    public IBuyerService BuyerService { get; }

    /// <inheritdoc cref="IBillService"/>
    public IBillService BillService { get; }

    protected SharedServiceFixture()
    {
        var supplierReaderRepository = new SupplierReaderRepository(Context);
        var supplierWriterRepository = new SupplierWriterRepository(Context, DateTimeProvider);

        var productReaderRepository = new ProductReaderRepository(Context);
        var productWriterRepository = new ProductWriterRepository(Context, DateTimeProvider);

        var buyerReaderRepository = new BuyerReaderRepository(Context);
        var buyerWriterRepository = new BuyerWriterRepository(Context, DateTimeProvider);

        var billReaderRepository = new BillReaderRepository(Context);
        var billWriterRepository = new BillWriterRepository(Context, DateTimeProvider);

        SuppliersService = new SuppliersService(
            Mapper,
            supplierReaderRepository,
            supplierWriterRepository,
            UnitOfWork
        );

        ProductsService = new ProductsService(
            Mapper,
            productReaderRepository,
            productWriterRepository,
            UnitOfWork
        );

        BuyerService = new BuyersService(
            Mapper,
            buyerReaderRepository,
            buyerWriterRepository,
            UnitOfWork
        );

        BillService = new BillsService(
            Mapper,
            billReaderRepository,
            billWriterRepository,
            UnitOfWork,
            supplierReaderRepository,
            buyerReaderRepository,
            productReaderRepository
        );
    }
}
