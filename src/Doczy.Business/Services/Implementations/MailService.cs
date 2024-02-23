using Doczy.Business.DTOs.MailDtos;
using Doczy.Business.Helpers.Settings;
using Doczy.Business.Services.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Net;
using System.Net.Mail;

namespace Doczy.Business.Services.Implementations
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequestDto mailRequest)
        {
            using (var client = new SmtpClient(_mailSettings.Host, _mailSettings.Port))
            {
                client.Credentials =
                  new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                client.EnableSsl = true;
                //  client.UseDefaultCredentials = true;
                var htmlTextPart = new TextPart(TextFormat.Html)
                {
                    Text = mailRequest.Body
                };
                MailMessage msg = new MailMessage(_mailSettings.Mail, mailRequest.ToEmail);
                msg.IsBodyHtml = true;
                msg.Body = mailRequest.Body;
                msg.Subject = mailRequest.Subject;

                client.Send(msg);
                //  await  client.SendMailAsync(msg);
            }
        }
    }
}
