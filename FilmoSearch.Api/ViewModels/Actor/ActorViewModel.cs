using FilmoSearch.Api.ViewModels.Film;

namespace FilmoSearch.Api.ViewModels.Actor
{
    public class ActorViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly Birthday { get; set; }

        public IEnumerable<FilmViewModel>? Films { get; set; }
    }
}
