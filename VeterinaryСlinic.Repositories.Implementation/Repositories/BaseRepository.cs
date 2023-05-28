using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.DataAccess;
using VeterinaryClinic.Domain.Entities;
using VeterinaryСlinic.Repositories.Contracts.Repositories;

namespace VeterinaryСlinic.Repositories.Implementation.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
       where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext Context;

        protected BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> GetAll() => Context.Set<TEntity>();

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token)
            => await Context.Set<TEntity>().SingleOrDefaultAsync(entity => entity.Id == id, token);

        public TEntity Create(TEntity entity)
        {
            Context.Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Context.Update(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
