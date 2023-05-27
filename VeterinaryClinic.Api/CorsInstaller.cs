using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Core.Installers;

namespace VeterinaryClinic.Api
{
    public class CorsInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: configuration["Cors:Name"]!,
                    policy =>
                    {
                        policy.WithOrigins(configuration.GetSection("Cors:Origins").Get<string[]>()!);
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                    });
            });
        }
    }
}
