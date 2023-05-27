using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Core.Installers;

namespace VeterinaryClinic.Api.Installers
{
    public class AutomapperInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ApiAssemblyMarker));
        }
    }
}
