using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Core.Installers;
using VeterinaryClinic.DataAccess.InitialDataCreate;

namespace VeterinaryClinic.DataAccess.Installers
{
    public class DataAccessInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ApplicationDbContext.ConnectionStringKey),
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly("VeterinaryClinic.DataAccess")));
            services.AddHostedService<MigrationsService>();
        }
    }
}
