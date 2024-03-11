namespace Doczy.Business.Services.Interfaces
{
    public interface IPayriffService
    {
        Task Pay(double sumAmount, string desc);
    }
}
