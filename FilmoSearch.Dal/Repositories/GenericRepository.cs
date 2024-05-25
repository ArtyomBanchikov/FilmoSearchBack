using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Dal.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly FilmoContext context;

        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(FilmoContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken token)
        {
            return await dbSet.AsNoTracking().ToListAsync(token);
        }

        public virtual async Task<TEntity> Create(TEntity entity, CancellationToken token)
        {
            await dbSet.AddAsync(entity, token);

            await context.SaveChangesAsync(token);

            return entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity, CancellationToken token)
        {
            dbSet.Update(entity);

            await context.SaveChangesAsync(token);

            return entity;
        }

        public virtual async Task<TEntity?> GetById(int id, CancellationToken token)
        {
            return await dbSet.FindAsync(new object[] { id }, token);
        }

        public virtual async Task Delete(TEntity entity, CancellationToken token)
        {
            dbSet.Remove(entity);

            await context.SaveChangesAsync(token);
        }
    }
}
