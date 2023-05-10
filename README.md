# Laboratorul 2
## Creationals patterns
In the next sections I will describe the way I implemented 4 creational patterns:
1. Builder
2. Factory method
3. Singletone
4. Prototype
## Builder pattern
I used the builder pattern for creating the blog entity, which can contain multiple fields.
The Builder interface looks like:
```cs
    public interface IBlogBuilder
    {
        IBlogBuilder WithTitle(string title);
        IBlogBuilder WithBody(string body);
        IBlogBuilder WithSystemName(string systemName);
        IBlogBuilder WithAuthor(string? author);
        IBlogBuilder From(BlogEntity blogEntity);
        BlogEntity Build();
    }
```
The implimentation sets the private fields of a builder and the Build method:
```cs
    public interface IBlogBuilder
    {
        IBlogBuilder WithTitle(string title);
        IBlogBuilder WithBody(string body);
        IBlogBuilder WithSystemName(string systemName);
        IBlogBuilder WithAuthor(string? author);
        IBlogBuilder From(BlogEntity blogEntity);
        BlogEntity Build();
    }
```
Example of builder usage:
```cs
    if (!string.IsNullOrEmpty(identityUser?.UserName))
    {
        copy = _blogBuilder.From(copy).WithAuthor(identityUser.UserName).Build();
    }
```
## Factory pattern
For factory pattent I decide to make a factory method for a MailMessage of 2 types - With and Without file attachment
The Interface:
```cs
    public interface IEmailMessageFactory
    {
        MailMessage CreateMailMessage(string email, string subject, string htmlMessage);

        MailMessage CreateMailMessageWithAttachments(string emails, string subject, string htmlMessage,
            string attachmentPath);
    }
 ```
 Implimentation example:
 ```cs
        public MailMessage CreateMailMessage(string emails, string subject, string htmlMessage)
        {
            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailConfiguration.FromAddress);
            var mailAddresses = EmailMessageFactoryHelper.GetEmailsFromString(emails);
            foreach (var mail in mailAddresses)
            {
                mailAddresses.Add(mail);
            }

            mailMessage.Body = htmlMessage;
            mailMessage.IsBodyHtml = true;

            return mailMessage;
        }
 ```
 
 Usage example:
 
 ```cs
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
```

## Prototype pattern

For prototype patten I created a new interface `IClonable<T>`
```cs
    public interface IClonable<T> where T : BaseEntity
    {
        T Clone();
    }
```
Implimentation:
```cs
    public BlogEntity(BlogEntity other): base()
    {
        Title= other.Title;
        Body= other.Body;
        SystemName= other.SystemName;
    }

    public BlogEntity Clone()
    {
        return new BlogEntity(this);
    }
```
Usage:

```cs
    [Authorize(Permissions.Blog.Create)]
    public async Task<IActionResult> Clone(BlogEntity source)
    {
        var copy = (BlogEntity)source.Clone();
        var user = this.User;
        var identityUser = await _userManager.GetUserAsync(user);
        if (!string.IsNullOrEmpty(identityUser?.UserName))
        {
            copy = _blogBuilder.From(copy).WithAuthor(identityUser.UserName).Build();
        }
        await _blogService.Create(copy);

        return RedirectToAction("Details", new {id=copy.Id});
    }
```

## Singletone

The singletone pattern I implemented for Email client settings.

Implimentation:

```cs
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
 ```
 
 Usage:
 
 ```cs
    public EmailSender(IEmailMessageFactory emailMessageFactory)
    {
        _emailConfiguration = EmailConfiguration.Instance;
        _emailMessageFactory = emailMessageFactory;
    }
 ```
 ## Conclusion
 In conclusion, the implementation of various creational patterns, including the Factory Method, Singleton, Prototype, and Builder patterns, has greatly enhanced the flexibility, scalability, and maintainability of our lab work.
 Overall, the utilization of these creational patterns in our lab work has resulted in code that is more modular, extensible, and efficient. By applying these patterns appropriately, we have achieved improved code organization, reduced duplication, and enhanced the overall design of our software. These creational patterns have proven to be valuable tools in creating robust and flexible systems.


