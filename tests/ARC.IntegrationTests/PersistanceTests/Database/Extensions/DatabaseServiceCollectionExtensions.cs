using ARC.Application.Abstractions.Services;
using ARC.Application.Features.Auth.Models;
using ARC.Infrastructure.Common.Services;
using ARC.Persistence;
using ARC.Persistence.Extensions;
using ARC.Persistence.Identity;
using ARC.Persistence.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ARC.IntegrationTests.Infrastructure.Extensions
{
    public static class DatabaseServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDatabaseServices(this IServiceCollection services, string connectionString, IConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("No connection string was found.");
            }

            return services
                .ConfigureLogging()
                .ConfigureIdentity()
                .ConfigureDbContext(connectionString)
                .ConfigureRepositories()
                .Configure<RefreshTokenSettings>(configuration.GetSection("RefreshToken"));
        }

        private static IServiceCollection ConfigureLogging(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddDebug();
                builder.SetMinimumLevel(LogLevel.Warning);
            });

            return services;
        }

        private static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddAppIdentity();
            return services;
        }

        private static IServiceCollection ConfigureDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.ScanAndRegisterRepositories();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
