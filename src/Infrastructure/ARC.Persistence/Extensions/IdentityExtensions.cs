using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ARC.Persistence.Identity;

namespace ARC.Persistence.Extensions
{
    public class IdentityExtensions
    {
        public static void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        }
    }
}



