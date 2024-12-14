using DeliveryOffice.API.Common.Abstractions;

namespace DeliveryOffice.API.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;

        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
