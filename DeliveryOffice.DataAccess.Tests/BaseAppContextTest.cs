using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.DataAccess.Tests;

public abstract class BaseAppContextTest : IDisposable
{
    private readonly DeliveryOfficeDbContext context;
    private readonly CancellationTokenSource cancellationTokenSource;

    public DeliveryOfficeDbContext Context => context;

    public CancellationToken Token => cancellationTokenSource.Token;

    public BaseAppContextTest()
    {
        var options = new DbContextOptionsBuilder<DeliveryOfficeDbContext>()
            .UseInMemoryDatabase($"AppContext{Guid.NewGuid()}")
            .Options;

        context = new DeliveryOfficeDbContext(options);
        cancellationTokenSource = new CancellationTokenSource();
    }

    void IDisposable.Dispose()
    {
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
        context.Dispose();
    }
}
