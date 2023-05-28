using VeterinaryClinic.DataAccess;
using VeterinaryClinic.Domain.Entities;
using VeterinaryСlinic.Repositories.Contracts.Repositories;

namespace VeterinaryСlinic.Repositories.Implementation.Repositories
{
    public class DogRepository : BaseRepository<Dog>, IDogRepository
    {
        public DogRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
