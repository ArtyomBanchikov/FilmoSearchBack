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

        public async Task<TModel> Create(TModel model, CancellationToken ct)
        {
            var resultEntity = await repository.Create(mapper.Map<TEntity>(model), ct);

            return mapper.Map<TModel>(resultEntity);
        }

        public async Task Delete(int id, CancellationToken ct)
        {
            var resultEntity = await repository.GetById(id, ct);

            await repository.Delete(resultEntity, ct);
        }

        public async Task<IEnumerable<TModel>> GetAll(CancellationToken ct)
        {
            var result = mapper.Map<IEnumerable<TModel>>(await repository.GetAll(ct));

            return result;
        }

        public async Task<TModel> GetById(int id, CancellationToken ct)
        {
            var resultEntity = await repository.GetById(id, ct);

            return mapper.Map<TModel>(resultEntity);
        }

        public async Task<TModel> Update(TModel model, CancellationToken ct)
        {
            var entity = mapper.Map<TEntity>(model);

            var resultEntity = await repository.Update(entity, ct);

            return mapper.Map<TModel>(resultEntity);
        }
    }
}
