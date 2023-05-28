using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VeterinaryClinic.Core.Installers;

namespace VeterinaryClinic.Core.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddInstallersFromAssemblies(this IServiceCollection services, IConfiguration configuration,
            IEnumerable<Assembly> assemblies)
        {
            assemblies.Distinct()
                .SelectMany(assembly => assembly.GetTypes().Where(type => type.IsAssignableTo(typeof(IInstaller))))
                .Distinct()
                .Select(type => (IInstaller)Activator.CreateInstance(type)!)
                .OrderBy(InstallerOrder)
                .ToList()
                .ForEach(installer => installer.Install(services, configuration));
        }

        public static void AddInstallersFromAssemblies(this IServiceCollection services, IConfiguration configuration,
        params Assembly[] assemblies)
        {
            services.AddInstallersFromAssemblies(configuration, (IEnumerable<Assembly>)assemblies);
        }

        public static void AddInstallersFromAssemblies(this IServiceCollection services, IConfiguration configuration,
            params Type[] assemblyMarkers)
        {
            services.AddInstallersFromAssemblies(configuration,
                assemblyMarkers.Distinct().Select(assemblyMarker => assemblyMarker.Assembly));
        }

        private static int InstallerOrder(IInstaller installer)
        {
            if (installer is IOrderedInstaller orderedInstaller)
            {
                return orderedInstaller.Order;
            }

            return int.MinValue;
        }
    }
}
