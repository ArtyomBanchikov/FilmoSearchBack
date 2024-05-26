using AutoMapper;
using FilmoSearch.Bll.Models;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Interfaces;

namespace FilmoSearch.Bll.Services
{
    public class ActorService : GenericService<ActorModel, ActorEntity>
    {
        public ActorService(IGenericRepository<ActorEntity> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
        }
    }
}
