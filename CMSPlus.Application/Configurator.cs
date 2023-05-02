

using CMSPlus.Application.Builders;
using CMSPlus.Application.Factories;
using CMSPlus.Application.Services;
using CMSPlus.Application.Services.EmailService;
using CMSPlus.Domain.Interfaces;
using CMSPlus.Domain.Interfaces.Builders;
using CMSPlus.Domain.Interfaces.Factories;
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
        services.AddScoped<IBlogCommentFactory, BlogCommentFactory>();
        services.AddScoped<IBlogBuilder, BlogBuilder>();
    }
}