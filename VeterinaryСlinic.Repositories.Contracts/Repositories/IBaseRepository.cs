using VeterinaryClinic.Domain.Entities;

namespace VeterinaryСlinic.Repositories.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
