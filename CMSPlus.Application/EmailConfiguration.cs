using CMSPlus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CMSPlus.Application;

public class EmailConfiguration:IEmailConfiguration
{
    private static EmailConfiguration _instance = null;
    private static readonly object padlock = new object();

    private EmailConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        var configuration = builder.Build();

        FromAddress = configuration["EmailConfiguration:FromAddress"];
        Address = configuration["EmailConfiguration:Address"];
        Port = Convert.ToInt32(configuration["EmailConfiguration:Port"]);
        UseSsl = Convert.ToBoolean(configuration["EmailConfiguration:UseSsl"]);
        Username = configuration["EmailConfiguration:UserName"];
        Password = configuration["EmailConfiguration:Password"];
    }

    public static EmailConfiguration Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new EmailConfiguration();
                    }
                }
            }
            return _instance;
        }
    }

    public string FromAddress { get; set; }
    public string Address { get; set; }
    public int Port { get; set; }
    public bool UseSsl { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}