using AutoMapper;
using FilmoSearch.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearch.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class GenericController<TViewModel, TModel> : Controller
        where TViewModel : class
        where TModel : class
    {
        private readonly IGenericService<TModel> _genericService;
        private readonly IMapper _mapper;
        public GenericController(
            IGenericService<TModel> genericService,
            IMapper mapper)
        {
            _mapper = mapper;
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IEnumerable<TViewModel>> GetAllAsync(CancellationToken token)
        {
            var result = await _genericService.GetAllAsync(token);

            return _mapper.Map<IEnumerable<TViewModel>>(result);
        }


        [HttpGet("{id}")]
        public async Task<TViewModel> GetByIdAsync(int id, CancellationToken token)
        {
            var result = await _genericService.GetByIdAsync(id, token);

            return _mapper.Map<TViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id,
            CancellationToken token)
        {
            await _genericService.DeleteAsync(id, token);
        }

        [HttpPut("{id}")]
        public virtual async Task<TViewModel> UpdateAsync(TViewModel tChangeModel,
            CancellationToken token)
        {
            var tModel = _mapper.Map<TModel>(tChangeModel);
            var result = await _genericService.UpdateAsync(tModel, token);

            return _mapper.Map<TViewModel>(result);
        }

        [HttpPost]
        public async Task<TViewModel> CreateAsync(TViewModel tChangeModel,
            CancellationToken token)
        {
            var tModel = _mapper.Map<TModel>(tChangeModel);
            var result = await _genericService.CreateAsync(tModel, token);

            return _mapper.Map<TViewModel>(result);
        }
    }
}
