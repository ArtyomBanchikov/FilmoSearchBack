using AutoMapper;
using FilmoSearch.Bll.Models;
using FilmoSearch.Dal.Entity;

namespace FilmoSearch.Bll.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActorEntity, ActorModel>();
            CreateMap<ActorModel, ActorEntity>();

            CreateMap<FilmEntity, FilmModel>();
            CreateMap<FilmModel, FilmEntity>();

            CreateMap<ReviewEntity, ReviewModel>();
            CreateMap<ReviewModel, ReviewEntity>();
        }
    }
}
