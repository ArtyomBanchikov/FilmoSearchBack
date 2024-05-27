using System.ComponentModel.DataAnnotations;

namespace FilmoSearch.Dal.Entity
{
    public class FilmEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        public IEnumerable<ReviewEntity>? Reviews { get; set; }

        [Required]
        public DateOnly PremiereDate { get; set; }

        public IEnumerable<ActorEntity>? Actors { get; set; }

        public IEnumerable<GenreEntity>? Genres { get; set; }
    }
}
