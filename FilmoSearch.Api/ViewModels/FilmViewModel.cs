namespace FilmoSearch.Api.ViewModels
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
