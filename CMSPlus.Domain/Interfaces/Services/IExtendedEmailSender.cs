namespace CMSPlus.Domain.Interfaces;

public interface IExtendedEmailSender
{
    public Task SendEmailAsync(List<string> emails, string subject, string body, string attachmentPath);
}