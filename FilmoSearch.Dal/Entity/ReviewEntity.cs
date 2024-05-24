using System.ComponentModel.DataAnnotations;

namespace FilmoSearch.Dal.Entity
{
    public class ReviewEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Stars { get; set; }
    }
}
