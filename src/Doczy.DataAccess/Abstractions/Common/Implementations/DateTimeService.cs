namespace Doczy.DataAccess.Abstractions.Common.Implementations
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }

}