using System.Net;
using System.Net.Mail;
using CMSPlus.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace CMSPlus.Application.Services.EmailService;

public class EmailSender : IEmailSender
{
    private readonly IEmailConfiguration _emailConfiguration;
    public EmailSender(IEmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        await Execute(email, subject, htmlMessage);
    }

    private async Task Execute(string email, string subject, string htmlMessage)
    {
        try
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_emailConfiguration.FromAddress)
            };

            mail.To.Add(new MailAddress(email));

            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = htmlMessage;

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
}