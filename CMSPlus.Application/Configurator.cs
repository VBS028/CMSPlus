

using CMSPlus.Application.Services;
using CMSPlus.Application.Services.EmailService;
using CMSPlus.Domain.Interfaces;
using CMSPlus.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CMSPlus.Application;

public static class Configurator
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IBlogCommentsService, BlogCommentService>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IExtendedEmailService, ExtendedEmailService>();
    }
}