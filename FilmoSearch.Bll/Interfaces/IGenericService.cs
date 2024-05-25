namespace FilmoSearch.Bll.Interfaces
{
    public interface IGenericService<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync(CancellationToken ct);

        Task<TModel> GetByIdAsync(int id, CancellationToken ct);

        Task<TModel> CreateAsync(TModel model, CancellationToken ct);

        Task<TModel> UpdateAsync(TModel model, CancellationToken ct);

        Task DeleteAsync(int id, CancellationToken ct);
    }
}
