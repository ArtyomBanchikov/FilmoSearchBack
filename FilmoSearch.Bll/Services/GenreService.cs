using AutoMapper;
using FilmoSearch.Bll.Models;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Interfaces;

namespace FilmoSearch.Bll.Services
{
    public class GenreService : GenericService<GenreModel, GenreEntity>
    {
        public GenreService(IGenericRepository<GenreEntity> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
        }
    }
}
