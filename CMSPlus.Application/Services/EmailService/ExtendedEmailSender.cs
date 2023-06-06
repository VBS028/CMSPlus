using CMSPlus.Domain.Interfaces;
using System.Net.Mail;
using System.Net;
using CMSPlus.Domain.Interfaces.Factories;

namespace CMSPlus.Application.Services.EmailService
{
    public class ExtendedEmailSender:IExtendedEmailSender
    {
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly IEmailMessageFactory _emailMessageFactory;
        public ExtendedEmailSender(IEmailMessageFactory emailMessageFactory)
        {
            _emailConfiguration = EmailConfiguration.Instance;
            _emailMessageFactory = emailMessageFactory;
        }

        private async Task ExecuteWithAttachment(string emails, string subject, string htmlMessage, string attachmentPath)
        {
            try
            {
                var mail = _emailMessageFactory.CreateMailMessageWithAttachments(emails, subject, htmlMessage,
                    attachmentPath);   
                
                using (SmtpClient smtp = new SmtpClient(_emailConfiguration.Address, _emailConfiguration.Port))
                {
                    smtp.Credentials = new NetworkCredential(_emailConfiguration.Username, _emailConfiguration.Password);
                    smtp.EnableSsl = _emailConfiguration.UseSsl;

                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SendEmailAsync(string emails, string subject, string body, string attachmentPath)
        {
            await ExecuteWithAttachment(emails, subject, body, attachmentPath);
        }
    }
}
