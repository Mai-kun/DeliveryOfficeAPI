using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories;
using DeliveryOffice.Services;
using DeliveryOffice.Services.Abstractions;
using Moq;

namespace DeliveryOffice.DataAccess.Tests;

public abstract class SharedServiceFixture : BaseAppContextTest
{
    public ISuppliersService SuppliersService { get; }
    public IProductsService ProductsService { get; }
    public IBuyerService BuyerService { get; }
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

        BuyerService = new BuyerService(
            Mapper,
            buyerReaderRepository,
            buyerWriterRepository,
            UnitOfWork
        );

        BillService = new BillService(
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
