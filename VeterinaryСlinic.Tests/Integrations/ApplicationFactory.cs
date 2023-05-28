using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using VeterinaryClinic.DataAccess;
using VeterinaryClinic.DataAccess.InitialDataCreate;
using Microsoft.Extensions.DependencyInjection;

namespace VeterinaryСlinic.Tests.Integrations
{
    public class ApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Where(descriptor =>
                        descriptor.ServiceType == typeof(ApplicationDbContext) ||
                        descriptor.ServiceType == typeof(DbContextOptions) ||
                        descriptor.ServiceType == typeof(DbContextOptions<ApplicationDbContext>) ||
                        descriptor.ImplementationType == typeof(MigrationsService))
                    .ToList()
                    .ForEach(descriptor => services.Remove(descriptor));

                services.AddScoped(_ => DatabaseFixture.Context);
            });
        }
    }
}
