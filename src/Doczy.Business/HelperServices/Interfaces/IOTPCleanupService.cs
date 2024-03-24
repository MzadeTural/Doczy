namespace Doczy.Business.HelperServices.Interfaces
{
    public interface IOTPCleanupService
    {
        Task CleanupExpiredOTPAsync();
    }
}
