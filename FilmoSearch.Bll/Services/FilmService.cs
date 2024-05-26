using AutoMapper;
using FilmoSearch.Bll.Models;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Interfaces;

namespace FilmoSearch.Bll.Services
{
    public class FilmService : GenericService<FilmModel, FilmEntity>
    {
        public FilmService(IGenericRepository<FilmEntity> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
        }
    }
}
