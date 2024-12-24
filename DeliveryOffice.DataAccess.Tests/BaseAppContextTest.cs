using AutoMapper;
using DeliveryOffice.API.Common;
using DeliveryOffice.API.Common.Abstractions;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.Services;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.DataAccess.Tests;

public abstract class BaseAppContextTest : IDisposable
{
    private readonly CancellationTokenSource cancellationTokenSource;

    public CancellationToken Token => cancellationTokenSource.Token;

    public DeliveryOfficeDbContext Context { get; }

    public IUnitOfWork UnitOfWork => Context;

    public IDateTimeProvider DateTimeProvider { get; }

    public IMapper Mapper { get; }

    protected BaseAppContextTest()
    {
        var options = new DbContextOptionsBuilder<DeliveryOfficeDbContext>()
                      .UseInMemoryDatabase($"AppContext{Guid.NewGuid()}")
                      .Options;

        Context = new DeliveryOfficeDbContext(options);
        cancellationTokenSource = new CancellationTokenSource();

        Mapper = new MapperConfiguration(cfg => cfg.AddProfile(typeof(AutoMapperRequestProfile)))
            .CreateMapper();

        DateTimeProvider = new DateTimeProvider();
    }

    void IDisposable.Dispose()
    {
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}
