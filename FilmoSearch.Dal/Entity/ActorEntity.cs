using System.ComponentModel.DataAnnotations;

namespace FilmoSearch.Dal.Entity
{
    public class ActorEntity
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
