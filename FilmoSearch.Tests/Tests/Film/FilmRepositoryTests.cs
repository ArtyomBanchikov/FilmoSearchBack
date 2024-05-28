using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Repositories;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Tests.Tests.Film
{
    public class FilmRepositoryTests
    {
        [Fact]
        public async Task GetFilmExisted_Succes()
        {
            //Arrange
            var filmId = 1;
            var film = TestData.GetFilmEntities()[0];
            var actors = TestData.GetActorEntities();
            var genres = TestData.GetGenreEntities();
            var reviews = TestData.GetReviewEntities();
            film.Actors = new List<ActorEntity> { actors[0], actors[1] };
            film.Genres = new List<GenreEntity> { genres[0] };
            film.Reviews = new List<ReviewEntity> { reviews[0] };
            var context = FilmoContextFactory.Create("FilmTestDb");
            context.Films.Add(film);
            context.SaveChanges();
            var repo = new FilmRepository(context);

            //Act
            var result = await repo.GetByIdAsync(filmId, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(film, result);
        }

        [Fact]
        public async Task GetFilmNonExisted_Fails()
        {
            //Arrange
            var filmId = 4;
            var films = TestData.GetFilmEntities();
            var context = FilmoContextFactory.Create("FilmTestDb");
            context.Films.AddRange(films);
            context.SaveChanges();
            var repo = new FilmRepository(context);

            //Act
            var result = await repo.GetByIdAsync(filmId, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllFilms_Success()
        {
            //Arrange
            var films = TestData.GetFilmEntities();
            foreach (var film in films)
            {
                film.Genres = new List<GenreEntity>();
            }

            var context = FilmoContextFactory.Create("FilmTestDb");
            context.Films.AddRange(films);
            context.SaveChanges();
            var repo = new FilmRepository(context);

            //Act
            var result = await repo.GetAllAsync(new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equivalent(films, result);
        }

        [Fact]
        public async Task UpdateFilmExisted_Succes()
        {
            var filmId = 1;
            var updateFilm = new FilmEntity { Id = filmId, PremiereDate = new DateOnly(2020, 1, 1), Title = "UpdateFilmTestTitle"};
            var film = TestData.GetFilmEntities().First();
            var context = FilmoContextFactory.Create("FilmTestDb");
            context.Films.Add(film);
            context.SaveChanges();
            context.Entry(film).State = EntityState.Detached;
            var repo = new FilmRepository(context);

            //Act
            var result = await repo.UpdateAsync(updateFilm, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(updateFilm, result);
        }

        [Fact]
        public async Task UpdateFilmNonExisted_Failed()
        {
            //Arrange
            var filmId = 4;
            var updateFilm = new FilmEntity { Id = filmId, PremiereDate = new DateOnly(2020, 1, 1), Title = "UpdateFilmTestTitle" };
            var film = TestData.GetFilmEntities().First();
            var context = FilmoContextFactory.Create("FilmTestDb");
            context.Films.Add(film);
            context.SaveChanges();
            var repo = new FilmRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repo.UpdateAsync(updateFilm, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }

        [Fact]
        public async Task CreateFilmNonExisted_Succes()
        {
            var filmId = 4;
            var createFilm = new FilmEntity { Id = filmId, PremiereDate = new DateOnly(2020, 1, 1), Title = "CreateFilmTestTitle" };
            var films = TestData.GetFilmEntities();
            var context = FilmoContextFactory.Create("FilmTestDb");
            context.Films.AddRange(films);
            context.SaveChanges();
            var repo = new FilmRepository(context);

            //Act
            var result = await repo.CreateAsync(createFilm, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(createFilm, result);
        }

        [Fact]
        public async Task CreateFilmExisted_Failed()
        {
            //Arrange
            var filmId = 1;
            var createFilm = new FilmEntity { Id = filmId, PremiereDate = new DateOnly(2020, 1, 1), Title = "CreateFilmTestTitle" };
            var film = TestData.GetFilmEntities().First();
            var context = FilmoContextFactory.Create("FilmTestDb");
            context.Films.Add(film);
            context.SaveChanges();
            context.Entry(film).State = EntityState.Detached;
            var repo = new FilmRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await repo.CreateAsync(createFilm, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }

        [Fact]
        public void DeleteFilmExisted_Succes()
        {
            var filmId = 1;
            var deleteFilm = new FilmEntity { Id = filmId, PremiereDate = new DateOnly(2020, 1, 1), Title = "DeleteFilmTestTitle" };
            var film = TestData.GetFilmEntities().First();
            var context = FilmoContextFactory.Create("FilmTestDb");
            context.Films.Add(film);
            context.SaveChanges();
            context.Entry(film).State = EntityState.Detached;
            var repo = new FilmRepository(context);

            //Act
            var result = repo.DeleteAsync(deleteFilm, new CancellationToken()).IsFaulted;
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteFilmNonExisted_Failed()
        {
            //Arrange
            var filmId = 4;
            var film = new FilmEntity { Id = filmId, PremiereDate = new DateOnly(2020, 1, 1), Title = "DeleteFilmTestTitle" };
            var films = TestData.GetFilmEntities();
            var context = FilmoContextFactory.Create("FilmTestDb");
            context.Films.AddRange(films);
            context.SaveChanges();
            var repo = new FilmRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repo.DeleteAsync(film, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }
    }
}
