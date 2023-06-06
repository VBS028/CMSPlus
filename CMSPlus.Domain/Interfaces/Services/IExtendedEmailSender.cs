namespace CMSPlus.Domain.Interfaces;

public interface IExtendedEmailSender
{
    public Task SendEmailAsync(string emails, string subject, string body, string attachmentPath);
}