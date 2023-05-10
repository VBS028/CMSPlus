using System.Net.Mail;

namespace CMSPlus.Application.Helpers;

public static class EmailMessageFactoryHelper
{
    public static List<MailAddress> GetEmailsFromString(string emails)
    {
        if (string.IsNullOrEmpty(emails))
        {
            throw new ArgumentNullException("emails");
        }

        return emails.Split("|").Select(x => new MailAddress(x)).ToList();
    }
}
