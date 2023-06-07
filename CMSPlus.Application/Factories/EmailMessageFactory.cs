using CMSPlus.Domain.Interfaces.Factories;
using System.Net.Mail;
using CMSPlus.Application.Helpers;
using CMSPlus.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CMSPlus.Application.Factories
{
    public class EmailMessageFactory:IEmailMessageFactory
    {
        
        private readonly IEmailConfiguration _emailConfiguration;
        public EmailMessageFactory()
        {
            _emailConfiguration = EmailConfiguration.Instance;  
        }
        
        /// <summary>
        /// Create a simple email
        /// </summary>
        /// <param name="emails">A string that contains email. Can send multiple emails by joining them using `|`</param>
        /// <param name="subject">Email subject</param>
        /// <param name="htmlMessage">Mail message body. Support html format</param>
        /// <returns>Instance of a MailMessage class</returns>
        public MailMessage CreateMailMessage(string emails, string subject, string htmlMessage)
        {
            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailConfiguration.FromAddress);
            var mailAddresses = EmailMessageFactoryHelper.GetEmailsFromString(emails);
            foreach (var mail in mailAddresses)
            {
               mailMessage.To.Add(mail);
            }

            mailMessage.Body = htmlMessage;
            mailMessage.IsBodyHtml = true;

            return mailMessage;
        }
        
        
        /// <summary>
        /// Create a email message with a file attachment
        /// </summary>
        /// <param name="emails">A string that contains email. Can send multiple emails by joining them using `|`</param>
        /// <param name="subject">Email subject</param>
        /// <param name="htmlMessage">Mail message body. Support html format</param>
        /// <param name="attachmentPath">Path to the attachment file</param>
        /// <returns>Instance of a MailMessage class</returns>
        public MailMessage CreateMailMessageWithAttachments(string emails, string subject, string htmlMessage, string attachmentPath)
        {
            var mailMessage = CreateMailMessage(emails, subject, htmlMessage);
            mailMessage.Attachments.Add(new Attachment(attachmentPath));
            return mailMessage;
        }
    }
}
