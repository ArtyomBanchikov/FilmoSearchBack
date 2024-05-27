namespace FilmoSearch.Bll.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Stars { get; set; }

        public int FilmId { get; set; }

        public FilmModel? Film { get; set; }

        public int UserId { get; set; }

        public UserModel? User { get; set; }
    }
}
