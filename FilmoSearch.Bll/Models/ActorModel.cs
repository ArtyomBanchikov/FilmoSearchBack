namespace FilmoSearch.Bll.Models
{
    public class ActorModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly Birthday { get; set; }

        public IEnumerable<FilmModel>? Films { get; set; }
    }
}
