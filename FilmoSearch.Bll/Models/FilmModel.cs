namespace FilmoSearch.Bll.Models
{
    public class FilmModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<ReviewModel>? Reviews { get; set; }

        public DateOnly PremiereDate { get; set; }

        public string Description { get; set; }

        public IEnumerable<ActorModel>? Actors { get; set; }

        public IEnumerable<GenreModel>? Genres { get; set; }
    }
}
