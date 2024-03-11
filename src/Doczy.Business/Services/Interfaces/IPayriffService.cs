namespace Doczy.Business.Services.Interfaces
{
    public interface IPayriffService
    {
        Task<dynamic> Pay(double sumAmount, string desc);
    }
}
