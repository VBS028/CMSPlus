using CMSPlus.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CMSPlus.Application.Services.EmailService
{
    public class ExtendedEmailService:IExtendedEmailService
    {
        public EmailConfiguration _emailConfiguration { get; }
        public ExtendedEmailService(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }

        private async Task ExecuteWithAttachment(List<string> emails, string subject, string htmlMessage, string attachmentPath)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailConfiguration.FromAddress)
                };

                foreach (var email in emails)
                {
                    mail.To.Add(new MailAddress(email));
                }

                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = htmlMessage;
                Attachment attachment = new Attachment(attachmentPath);
                mail.Attachments.Add(attachment);

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

        public async Task SendEmailWithAttachmentAsync(List<string> emails, string subject, string body, string attachmentPath)
        {
            await ExecuteWithAttachment(emails, subject, body, attachmentPath);
        }
    }
}
