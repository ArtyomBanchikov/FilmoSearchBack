using System.ComponentModel.DataAnnotations;

namespace FilmoSearch.Dal.Entity
{
    public class FilmEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
