using AutoMapper;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Dal.Interfaces;

namespace FilmoSearch.Bll.Services
{
    public class GenericService<TModel, TEntity> : IGenericService<TModel>
        where TModel : class
        where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> repository;

        protected readonly IMapper mapper;

        public GenericService(IGenericRepository<TEntity> genericRepository, IMapper mapper)
        {
            repository = genericRepository;
            this.mapper = mapper;
        }

        public async Task<TModel> CreateAsync(TModel model, CancellationToken ct)
        {
            var resultEntity = await repository.CreateAsync(mapper.Map<TEntity>(model), ct);

            return mapper.Map<TModel>(resultEntity);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var resultEntity = await repository.GetByIdAsync(id, ct);

            await repository.DeleteAsync(resultEntity, ct);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken ct)
        {
            var result = mapper.Map<IEnumerable<TModel>>(await repository.GetAllAsync(ct));

            return result;
        }

        public async Task<TModel> GetByIdAsync(int id, CancellationToken ct)
        {
            var resultEntity = await repository.GetByIdAsync(id, ct);

            return mapper.Map<TModel>(resultEntity);
        }

        public async Task<TModel> UpdateAsync(TModel model, CancellationToken ct)
        {
            var entity = mapper.Map<TEntity>(model);

            var resultEntity = await repository.UpdateAsync(entity, ct);

            return mapper.Map<TModel>(resultEntity);
        }
    }
}
