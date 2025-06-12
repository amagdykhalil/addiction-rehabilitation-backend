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

            services.AddScoped<IEmailSender<User>, IdentityEmailSender>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            JWTExtensions.AddJWT(services, configuration);
            return services;
        }
    }

}



