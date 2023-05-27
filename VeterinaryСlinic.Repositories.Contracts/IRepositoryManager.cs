using VeterinaryСlinic.Repositories.Contracts.Repositories;

namespace VeterinaryСlinic.Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IDogRepository DogRepository { get; }

        Task<bool> SaveChangesAsync(CancellationToken token = default);
    }
}
