namespace Doczy.DataAccess.Abstractions.Common
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
