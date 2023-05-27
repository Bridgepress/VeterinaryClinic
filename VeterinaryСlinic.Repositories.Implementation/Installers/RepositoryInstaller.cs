using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using VeterinaryClinic.Core.Installers;
using VeterinaryСlinic.Repositories.Contracts;
using VeterinaryСlinic.Repositories.Implementation.Repositories;

namespace VeterinaryСlinic.Repositories.Implementation.Installers
{
    public class RepositoryInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            var repositories = typeof(RepositoryManager).Assembly.GetTypes()
                .Where(type => type.BaseType is not null && type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>));

            foreach (var repository in repositories)
            {
                var repositoryInterface = repository.GetInterfaces()
                    .Single(i => !i.IsGenericType);
                services.AddScoped(repositoryInterface, repository);
            }
        }
    }
}
