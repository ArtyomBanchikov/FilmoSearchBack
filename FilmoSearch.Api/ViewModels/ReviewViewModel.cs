namespace FilmoSearch.Api.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Stars { get; set; }

        public int FilmId { get; set; }

        public FilmViewModel? Film { get; set; }

        public int UserId { get; set; }

        public UserViewModel? User { get; set; }
    }
}
