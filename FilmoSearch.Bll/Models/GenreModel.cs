namespace FilmoSearch.Bll.Models
{
    public class GenreModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<FilmModel>? Films { get; set; }
    }
}
