using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace ARC.Infrastructure.Localization
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalizationSetup(this IServiceCollection services)
        {
            services.AddLocalization();
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddSingleton<RequestCultureMiddleware>();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("ar"),
                    new CultureInfo("en"),
                };

                options.SupportedCultures = supportedCultures;
            });

            return services;
        }

        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder app)
        {
            var supportedCultures = new[] { "ar", "en" };
            var localizationOptions = new RequestLocalizationOptions()
                .AddSupportedCultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            return app.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
