using ARC.Application.Abstractions.Infrastructure;
using ARC.Application.Abstractions.UserContext;
using ARC.Application.Contracts;
using ARC.Domain.Entities;
using ARC.Infrastructure.Authentication;
using ARC.Infrastructure.Email;
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

            services.AddScoped<IEmailSender<User>, IdentityEmailSender>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<ITokenProvider, TokenProvider>();

            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            JWTExtensions.AddJWT(services, configuration);
            return services;
        }
    }
}



