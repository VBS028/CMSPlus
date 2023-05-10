using System.Net;
using System.Net.Mail;
using CMSPlus.Domain.Interfaces;
using CMSPlus.Domain.Interfaces.Factories;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace CMSPlus.Application.Services.EmailService;

public class EmailSender : IEmailSender
{
    private readonly IEmailConfiguration _emailConfiguration;
    private readonly IEmailMessageFactory _emailMessageFactory;
    public EmailSender(IEmailMessageFactory emailMessageFactory)
    {
        _emailConfiguration = EmailConfiguration.Instance;
        _emailMessageFactory = emailMessageFactory;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        await Execute(email, subject, htmlMessage);
    }

    private async Task Execute(string email, string subject, string htmlMessage)
    {
        try
        {
            var mail = _emailMessageFactory.CreateMailMessage(email, subject, htmlMessage);

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