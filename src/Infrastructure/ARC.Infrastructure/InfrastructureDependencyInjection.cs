using ARC.Application.Abstractions.Infrastructure;
using ARC.Application.Abstractions.Services;
using ARC.Application.Abstractions.UserContext;
using ARC.Application.Contracts;
using ARC.Domain.Entities;
using ARC.Infrastructure.Authentication;
using ARC.Infrastructure.Common.Services;
using ARC.Infrastructure.Email;
using ARC.Infrastructure.Localization;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ARC.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddLocalizationSetup();

            // Email services
            services.AddTransient<IEmailTemplate, EmailTemplate>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserEmailService, UserEmailService>();
            
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            JWTExtensions.AddJWT(services, configuration);

            return services;
        }
    }
}



