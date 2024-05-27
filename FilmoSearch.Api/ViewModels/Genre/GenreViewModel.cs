using FilmoSearch.Api.ViewModels.Film;

namespace FilmoSearch.Api.ViewModels.Genre
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<FilmViewModel>? Films { get; set; }
    }
}
