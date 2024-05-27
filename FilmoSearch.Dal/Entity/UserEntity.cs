using System.ComponentModel.DataAnnotations;

namespace FilmoSearch.Dal.Entity
{
    public class UserEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateOnly RegistrationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public IEnumerable<ReviewEntity>? Reviews { get; set; }
    }
}
