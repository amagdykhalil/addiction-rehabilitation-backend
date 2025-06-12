using ARC.Application.Abstractions.Services;

namespace ARC.Infrastructure.Common.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
