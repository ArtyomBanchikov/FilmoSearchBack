using FilmoSearch.Dal.Entity;

namespace FilmoSearch.Bll.Models
{
    public class ActorModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly Birthday { get; set; }

        public IEnumerable<FilmEntity>? Films { get; set; }
    }
}
