namespace CMSPlus.Domain.Interfaces;

public interface IExtendedEmailService
{
    public Task SendEmailWithAttachmentAsync(List<string> emails, string subject, string body, string attachmentPath);
}