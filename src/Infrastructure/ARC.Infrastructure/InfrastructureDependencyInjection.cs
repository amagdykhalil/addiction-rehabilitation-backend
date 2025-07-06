using ARC.Application.Abstractions.Infrastructure;
using ARC.Application.Abstractions.Services;
using ARC.Application.Abstractions.UserContext;
using ARC.Infrastructure.Authentication;
using ARC.Infrastructure.Common.Services;
using ARC.Infrastructure.Email;
using ARC.Infrastructure.Email.Models;
using ARC.Infrastructure.Localization;
using ARC.Infrastructure.Localization.Models;
using Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ARC.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            // Localization services
            services.Configure<LocalizationSettings>(configuration.GetSection("LocalizationSettings"));
            services.AddLocalizationSetup();

            // Email services
            services.Configure<EmailTemplateSettings>(configuration.GetSection("EmailTemplateSettings"));
            services.Configure<EmailWorkerSettings>(configuration.GetSection("EmailWorkerSettings"));
            
            services.AddTransient<IEmailTemplate, EmailTemplate>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserEmailService, UserEmailService>();
            services.AddTransient<IUserActionLinkBuilder, UserActionLinkBuilder>();
            
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IEmailQueue, InMemoryEmailQueue>();
            services.AddHostedService<EmailWorker>();

            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            services.AddJWT(configuration);


            return services;
        }
    }
}



