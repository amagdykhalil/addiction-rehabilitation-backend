using ARC.API.Extensions.Startup;
using ARC.API.Middleware;
using ARC.Application;
using ARC.Application.Common.Validator;
using ARC.Infrastructure;
using ARC.Persistence;
using Scalar.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace ARC.API
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureBuilder(WebApplicationBuilder builder)
        {
            LoggingExtensions.UseLogging(builder);
            AzureKeyVaultExtensions.UseAzureKeyVault(builder);
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.AddProblemDetails();
            services.AddAuthorization();


            services.AddInfrastructure(_configuration)
                    .AddPersistence(_configuration)
                    .AddApplication(_configuration);

            services.AddSingleton<IFluentValidationAutoValidationResultFactory, ValidationResultFactory>();
            services.AddOpenApi();

            CorsExtensions.AddCors(services, _configuration);
            APIVersioningExtentions.AddAPIVersioning(services);
            RateLimiterExtension.AddRateLimiter(services);
        }
        public void Configure(WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("ARC API Reference")
                           .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
                });
            }

            app.UseForwardedHeaders();

            app.UseExceptionHandler();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(CorsExtensions.AllowsOrigins);

            app.MapControllers();
        }
    }
}



