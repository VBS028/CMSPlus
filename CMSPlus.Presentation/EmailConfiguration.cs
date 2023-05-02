using CMSPlus.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace CMSPlus.Presentation;

public class EmailConfiguration:IEmailConfiguration
{
    private static readonly Lazy<EmailConfiguration> LazyInstance = new(() => new EmailConfiguration());

    private EmailConfiguration()
    {

    }

    public static EmailConfiguration Instance => LazyInstance.Value;

    public string FromAddress => ConfigurationManager.AppSettings[nameof(FromAddress)];
    public string Address => ConfigurationManager.AppSettings[nameof(Address)];
    public int Port => Convert.ToInt32(ConfigurationManager.AppSettings[nameof(Port)]);
    public bool UseSsl => Convert.ToBoolean(ConfigurationManager.AppSettings[nameof(UseSsl)]);
    public string Username => ConfigurationManager.AppSettings[nameof(Username)];
    public string Password => ConfigurationManager.AppSettings[nameof(Password)];
}