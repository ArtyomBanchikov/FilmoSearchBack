using AutoMapper;
using FilmoSearch.Bll.Models;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Interfaces;

namespace FilmoSearch.Bll.Services
{
    public class ReviewService : GenericService<ReviewModel, ReviewEntity>
    {
        public ReviewService(IGenericRepository<ReviewEntity> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
        }
    }
}
