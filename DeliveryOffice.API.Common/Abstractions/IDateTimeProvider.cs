namespace DeliveryOffice.API.Common.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now { get; }

        DateTimeOffset UtcNow { get; }
    }
}
