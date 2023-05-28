using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.DataAccess.InitialDataCreate
{
    public class MigrationsService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public MigrationsService(IServiceProvider serviceProvider, ILogger logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                await context.Database.MigrateAsync(stoppingToken);
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Cannot migrate database");
                return;
            }

            await SeedDogs(context, stoppingToken); 
        }

        private static async Task SeedDogs(ApplicationDbContext context, CancellationToken stoppingToken)
        {
            if (await context.Dogs.AnyAsync(stoppingToken))
            {
                return;
            }
            context.AddRange(
             new Dog
             {
                 Id = Guid.NewGuid(),
                 Name = "Нео",
                 Color = "red & amber",
                 TailLength = 22,
                 Weight = 32

             },
            new Dog
            {
                Id = Guid.NewGuid(),
                Name = "Jessy",
                Color = "black & white",
                TailLength = 7,
                Weight = 14
            }
          );

            await context.SaveChangesAsync(stoppingToken);
        }
    }
}
