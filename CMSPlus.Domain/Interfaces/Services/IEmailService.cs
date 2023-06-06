namespace CMSPlus.Domain.Interfaces.Services;

public interface IEmailService
{
    Task SendEmail(List<string> to, string subject, string htmlMessage, string attachmentPath = "");
}