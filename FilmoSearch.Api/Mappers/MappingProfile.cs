using AutoMapper;
using FilmoSearch.Api.ViewModels.Actor;
using FilmoSearch.Api.ViewModels.Film;
using FilmoSearch.Api.ViewModels.Genre;
using FilmoSearch.Api.ViewModels.Review;
using FilmoSearch.Api.ViewModels.User;
using FilmoSearch.Bll.Models;

namespace FilmoSearch.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActorViewModel, ActorModel>();
            CreateMap<ActorModel, ActorViewModel>();
            CreateMap<AddActorViewModel, ActorModel>();
            CreateMap<ActorModel, AddActorViewModel>();

            CreateMap<FilmViewModel, FilmModel>();
            CreateMap<FilmModel, FilmViewModel>();
            CreateMap<AddFilmViewModel, FilmModel>();
            CreateMap<FilmModel, AddFilmViewModel>();

            CreateMap<GenreViewModel, GenreModel>();
            CreateMap<GenreModel, GenreViewModel>();
            CreateMap<AddGenreViewModel, GenreModel>();
            CreateMap<GenreModel, AddGenreViewModel>();

            CreateMap<ReviewViewModel, ReviewModel>();
            CreateMap<ReviewModel, ReviewViewModel>();
            CreateMap<AddReviewViewModel, ReviewModel>();
            CreateMap<ReviewModel, AddReviewViewModel>();

            CreateMap<UserViewModel, UserModel>();
            CreateMap<UserModel, UserViewModel>();
            CreateMap<AddUserViewModel, UserModel>();
            CreateMap<UserModel, AddUserViewModel>();
        }
    }
}
