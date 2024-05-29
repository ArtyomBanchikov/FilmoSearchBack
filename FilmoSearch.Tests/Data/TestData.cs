using FilmoSearch.Dal.Entity;

namespace FilmoSearch.Tests.Data
{
    public static class TestData
    {
        public static List<ActorEntity> GetActorEntities()
        {
            ActorEntity actor1 = new ActorEntity
            {
                Id = 1,
                FirstName = "TestActorName1",
                LastName = "TestActorLastName1",
                Birthday = new DateOnly(1981, 1, 1)
            };
            ActorEntity actor2 = new ActorEntity
            {
                Id = 2,
                FirstName = "TestActorName2",
                LastName = "TestActorLastName2",
                Birthday = new DateOnly(1982, 2, 2)
            };
            ActorEntity actor3 = new ActorEntity
            {
                Id = 3,
                FirstName = "TestActorName3",
                LastName = "TestActorLastName3",
                Birthday = new DateOnly(1983, 3, 3)
            };

            return new List<ActorEntity> { actor1, actor2, actor3 };
        }
        public static List<FilmEntity> GetFilmEntities()
        {
            FilmEntity film1 = new FilmEntity
            {
                Id = 1,
                Title = "TestFilmTitle1",
                PremiereDate = new DateOnly(2021, 1, 1),
                Description = "TestFilmDescription1"
            };
            FilmEntity film2 = new FilmEntity
            {
                Id = 2,
                Title = "TestFilmTitle2",
                PremiereDate = new DateOnly(2022, 2, 2),
                Description = "TestFilmDescription2"
            };
            FilmEntity film3 = new FilmEntity
            {
                Id = 3,
                Title = "TestFilmTitle3",
                PremiereDate = new DateOnly(2023, 3, 3),
                Description = "TestFilmDescription3"
            };

            return new List<FilmEntity> { film1, film2, film3 };
        }
        public static List<GenreEntity> GetGenreEntities()
        {
            GenreEntity genre1 = new GenreEntity
            {
                Id = 1,
                Name = "TestGenreName1",
                Description = "TestGenreDescription1"
            };
            GenreEntity genre2 = new GenreEntity
            {
                Id = 2,
                Name = "TestGenreName2",
                Description = "TestGenreDescription2"
            };
            GenreEntity genre3 = new GenreEntity
            {
                Id = 3,
                Name = "TestGenreName3",
                Description = "TestGenreDescription3"
            };

            return new List<GenreEntity> { genre1, genre2, genre3 };
        }
        public static List<ReviewEntity> GetReviewEntities()
        {
            ReviewEntity review1 = new ReviewEntity
            {
                Id = 1,
                Title = "TestReviewTitle1",
                Description = "TestReviewDescription1",
                Stars = 1,
                FilmId = 1,
                UserId = 1
            };
            ReviewEntity review2 = new ReviewEntity
            {
                Id = 2,
                Title = "TestReviewTitle2",
                Description = "TestReviewDescription2",
                Stars = 2,
                FilmId = 2,
                UserId = 2
            };
            ReviewEntity review3 = new ReviewEntity
            {
                Id = 3,
                Title = "TestReviewTitle3",
                Description = "TestReviewDescription3",
                Stars = 3,
                FilmId = 3,
                UserId = 3
            };

            return new List<ReviewEntity> { review1, review2, review3 };
        }
        public static List<UserEntity> GetUserEntities()
        {
            UserEntity user1 = new UserEntity
            {
                Id = 1,
                Name = "TestUserName1",
                Lastname = "TestUserLastName1",
                Email = "TestUser1@gmail.com",
                Password = "TestUserPassword1",
                RegistrationDate = new DateOnly(2021, 1, 1)
            };
            UserEntity user2 = new UserEntity
            {
                Id = 2,
                Name = "TestUserName2",
                Lastname = "TestUserLastName2",
                Email = "TestUser2@gmail.com",
                Password = "TestUserPassword2",
                RegistrationDate = new DateOnly(2022, 2, 2)
            };
            UserEntity user3 = new UserEntity
            {
                Id = 3,
                Name = "TestUserName3",
                Lastname = "TestUserLastName3",
                Email = "TestUser3@gmail.com",
                Password = "TestUserPassword3",
                RegistrationDate = new DateOnly(2023, 3, 3)
            };

            return new List<UserEntity> { user1, user2, user3 };
        }
    }
}
