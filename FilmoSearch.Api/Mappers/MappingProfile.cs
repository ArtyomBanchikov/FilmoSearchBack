using AutoMapper;
using FilmoSearch.Api.ViewModels;
using FilmoSearch.Bll.Models;

namespace FilmoSearch.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActorViewModel, ActorModel>();
            CreateMap<ActorModel, ActorViewModel>();

            CreateMap<FilmViewModel, FilmModel>();
            CreateMap<FilmModel, FilmViewModel>();

            CreateMap<ReviewViewModel, ReviewModel>();
            CreateMap<ReviewModel, ReviewViewModel>();
        }
    }
}
