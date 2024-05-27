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

            CreateMap<GenreViewModel, GenreModel>();
            CreateMap<GenreModel, GenreViewModel>();

            CreateMap<ReviewViewModel, ReviewModel>();
            CreateMap<ReviewModel, ReviewViewModel>();

            CreateMap<UserViewModel, UserModel>();
            CreateMap<UserModel, UserViewModel>();
        }
    }
}
