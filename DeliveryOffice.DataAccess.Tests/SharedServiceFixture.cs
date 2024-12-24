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

    public Mock<IUnitOfWork> UnitOfWorkMock { get; }

    protected SharedServiceFixture()
    {
        UnitOfWorkMock = new Mock<IUnitOfWork>();
        SuppliersService = new SuppliersService(
            Mapper,
            new SupplierReaderRepository(Context),
            new SupplierWriterRepository(Context, DateTimeProvider),
            UnitOfWorkMock.Object
        );

        ProductsService = new ProductsService(
            Mapper,
            new ProductReaderRepository(Context),
            new ProductWriterRepository(Context, DateTimeProvider),
            UnitOfWorkMock.Object
        );

        BuyerService = new BuyerService(
            Mapper,
            new BuyerReaderRepository(Context),
            new BuyerWriterRepository(Context, DateTimeProvider),
            UnitOfWorkMock.Object
        );

        BillService = new BillService(
            Mapper,
            new BillReaderRepository(Context),
            new BillWriterRepository(Context, DateTimeProvider),
            UnitOfWorkMock.Object,
            new SupplierReaderRepository(Context),
            new BuyerReaderRepository(Context),
            new ProductReaderRepository(Context)
        );
    }
}
