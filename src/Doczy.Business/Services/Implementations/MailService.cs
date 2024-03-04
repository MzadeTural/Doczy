using Doczy.Business.DTOs.MailDtos;
using Doczy.Business.Helpers.Settings;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;

namespace Doczy.Business.Services.Implementations
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IWebHostEnvironment _environment;

        public MailService(IOptions<MailSettings> mailSettings, IWebHostEnvironment environment)
        {
            _mailSettings = mailSettings.Value;
            _environment = environment;
        }

        public async Task<string> GetEmailTemplateAsync(string LinkorOTP, string template)
        {
            string path = Path.Combine(_environment.WebRootPath, "uploads", "templates", template);
            using StreamReader streamReader = new StreamReader(path);
            string result = await streamReader.ReadToEndAsync();
            var body = result.Replace("[LinkorOTP]", LinkorOTP);
            return body;
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
