using System.ComponentModel.DataAnnotations;

namespace FilmoSearch.Dal.Entity
{
    public class GenreEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public IEnumerable<FilmEntity>? Films { get; set; }
    }
}
