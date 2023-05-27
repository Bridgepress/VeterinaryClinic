using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.DataAccess;
using VeterinaryСlinic.Repositories.Contracts;
using VeterinaryСlinic.Repositories.Contracts.Repositories;

namespace VeterinaryСlinic.Repositories.Implementation
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public RepositoryManager(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IDogRepository DogRepository => _serviceProvider.GetRequiredService<IDogRepository>();

        public async Task<bool> SaveChangesAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token) > 0;
        }
    }
}
