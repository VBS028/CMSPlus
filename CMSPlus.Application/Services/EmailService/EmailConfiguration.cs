namespace CMSPlus.Application.Services.EmailService;

public class EmailConfiguration
{
    public string FromAddress { get; set; }
    public string Address { get; set; }
    public int Port { get; set; }
    public bool UseSsl { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}