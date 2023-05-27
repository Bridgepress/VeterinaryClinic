using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VeterinaryClinic.Core.Installers
{
    public interface IInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}
