using Doczy.Business.DTOs.MailDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequestDto mailRequest);
    }
}
