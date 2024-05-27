using FilmoSearch.Api.ViewModels.Actor;
using FilmoSearch.Api.ViewModels.Genre;
using FilmoSearch.Api.ViewModels.Review;

namespace FilmoSearch.Api.ViewModels.Film
{
    public class FilmViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<ReviewViewModel>? Reviews { get; set; }

        public DateOnly PremiereDate { get; set; }

        public IEnumerable<ActorViewModel>? Actors { get; set; }

        public IEnumerable<GenreViewModel>? Genres { get; set; }
    }
}
