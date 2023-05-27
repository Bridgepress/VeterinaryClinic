using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Core.Installers;

namespace VeterinaryСlinic.Handlers.Installers
{
    public class HandlersInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(serviceConfiguration =>
                serviceConfiguration.RegisterServicesFromAssemblyContaining<HandlersInstaller>());
        }
    }
}
