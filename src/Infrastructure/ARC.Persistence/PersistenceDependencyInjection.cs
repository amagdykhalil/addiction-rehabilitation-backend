using ARC.Application.Abstractions.UserContext;
using ARC.Application.Contracts.Persistence.UoW;
using ARC.Persistence.Extensions;
using ARC.Persistence.Identity;
using ARC.Persistence.UoW;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ARC.Persistence
{
    public static class PersistenceDependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddAppIdentity();
            services.AddDbContextWithInterceptors(configuration);
            services.ScanAndRegisterRepositories();

            return services;
        }
    }
}



