namespace FilmoSearch.Dal.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token);
        Task<TEntity?> GetByIdAsync(int id, CancellationToken token);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token);
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken token);
        Task DeleteAsync(TEntity entity, CancellationToken token);
    }
}
