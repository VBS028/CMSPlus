using CMSPlus.Application.Services.EmailService;
using CMSPlus.Domain.Interfaces;
using CMSPlus.Domain.Models.TopicModels;
using CMSPlus.Domain.Persistence;
using CMSPlus.Presentation.AutoMapperProfiles;
using CMSPlus.Presentation.CustomPolicy;
using CMSPlus.Presentation.CustomPolicy.CustomHandlers;
using CMSPlus.Presentation.CustomPolicy.CustomPolicyProviders;
using CMSPlus.Presentation.Validations.Helpers;
using CMSPlus.Presentation.Validations.TopicValidators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CMSPlus.Presentation;

public static class Configurator
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddRazorPages().AddRazorRuntimeCompilation();
        services.AddScoped<ValidatorHelpers>();
        services.AddScoped<UserManager<IdentityUser>>();
        services.AddScoped<RoleManager<IdentityRole>>();
        services.AddScoped<SignInManager<IdentityUser>>();
        services.AddScoped<IValidator<TopicCreateViewModel>, TopicCreateModelValidator>();
        services.AddScoped<IValidator<TopicEditViewViewModel>, TopicEditModelValidator>();
        services.AddControllersWithViews();
        services.AddValidatorsFromAssemblyContaining<TopicEditModelValidator>();
        services.AddIdentity<IdentityUser,IdentityRole>(options=>
                options.SignIn.RequireConfirmedEmail=true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);;
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddControllersWithViews();
        services.AddScoped<IEmailSender,EmailSender>();
        services.AddScoped<IExtendedEmailSender,ExtendedEmailSender>();
    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            //todo read via reflection
            cfg.AddProfile<TopicProfile>();
            cfg.AddProfile<BlogProfile>();
        }, typeof(Program).Assembly);
    }

}