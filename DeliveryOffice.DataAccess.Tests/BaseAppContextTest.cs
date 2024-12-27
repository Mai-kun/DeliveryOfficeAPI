using AutoMapper;
using DeliveryOffice.API.Common;
using DeliveryOffice.API.Common.Abstractions;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.Services;
using DeliveryOffice.Services.Abstractions.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.DataAccess.Tests;

/// <summary>
///     Базовый класс-помощник с настройками базы данных
/// </summary>
public abstract class BaseAppContextTest : IDisposable
{
    private readonly CancellationTokenSource cancellationTokenSource;

    /// <inheritdoc cref="CancellationToken" />
    public CancellationToken Token => cancellationTokenSource.Token;

    /// <summary>
    ///     Контекст базы данных
    /// </summary>
    public DeliveryOfficeDbContext Context { get; }

    /// <inheritdoc cref="IUnitOfWork" />
    public IUnitOfWork UnitOfWork => Context;

    /// <inheritdoc cref="IDbReader" />
    public IDbReader Reader => Context;

    /// <summary>
    ///     Провайдер времени
    /// </summary>
    public IDateTimeProvider DateTimeProvider { get; }

    /// <summary>
    ///     Маппер
    /// </summary>
    public IMapper Mapper { get; }

    protected BaseAppContextTest()
    {
        var options = new DbContextOptionsBuilder<DeliveryOfficeDbContext>()
                      .UseInMemoryDatabase($"AppContext{Guid.NewGuid()}")
                      .Options;

        Context = new DeliveryOfficeDbContext(options);
        cancellationTokenSource = new CancellationTokenSource();

        Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ProductRequest, Product>().ReverseMap();
            cfg.AddProfile(typeof(AutoMapperRequestProfile));
        }).CreateMapper();

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
