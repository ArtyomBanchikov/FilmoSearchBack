using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Repositories;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Tests.Tests.Genre
{
    public class GenreRepositoryTests
    {
        [Fact]
        public async Task GetGenreExisted_Succes()
        {
            //Arrange
            var genreId = 1;
            var genre = TestData.GetGenreEntities()[0];
            var films = TestData.GetFilmEntities();
            genre.Films = new List<FilmEntity> { films[0]};
            var context = FilmoContextFactory.Create("GenreTestDb");
            context.Genres.Add(genre);
            context.SaveChanges();
            var repo = new GenreRepository(context);

            //Act
            var result = await repo.GetByIdAsync(genreId, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(genre, result);
        }

        [Fact]
        public async Task GetGenreNonExisted_Fails()
        {
            //Arrange
            var genreId = 4;
            var genres = TestData.GetGenreEntities();
            var context = FilmoContextFactory.Create("GenreTestDb");
            context.Genres.AddRange(genres);
            context.SaveChanges();
            var repo = new GenreRepository(context);

            //Act
            var result = await repo.GetByIdAsync(genreId, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllGenres_Succes()
        {
            //Arrange
            var genres = TestData.GetGenreEntities();
            var context = FilmoContextFactory.Create("GenreTestDb");
            context.Genres.AddRange(genres);
            context.SaveChanges();
            var repo = new GenreRepository(context);

            //Act
            var result = await repo.GetAllAsync(new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equivalent(genres, result);
        }

        [Fact]
        public async Task UpdateGenreExisted_Succes()
        {
            var genreId = 1;
            var updateGenre = new GenreEntity { Id = genreId, Description = "UpdateGenreTestDescription", Name = "UpdateGenreTestName" };
            var genre = TestData.GetGenreEntities().First();
            var context = FilmoContextFactory.Create("GenreTestDb");
            context.Genres.Add(genre);
            context.SaveChanges();
            context.Entry(genre).State = EntityState.Detached;
            var repo = new GenreRepository(context);

            //Act
            var result = await repo.UpdateAsync(updateGenre, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(updateGenre, result);
        }

        [Fact]
        public async Task UpdateGenreNonExisted_Failed()
        {
            //Arrange
            var genreId = 4;
            var updateGenre = new GenreEntity { Id = genreId, Description = "UpdateGenreTestDescription", Name = "UpdateGenreTestName" };
            var genre = TestData.GetGenreEntities().First();
            var context = FilmoContextFactory.Create("GenreTestDb");
            context.Genres.Add(genre);
            context.SaveChanges();
            var repo = new GenreRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repo.UpdateAsync(updateGenre, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }

        [Fact]
        public async Task CreateGenreNonExisted_Succes()
        {
            var genreId = 4;
            var createGenre = new GenreEntity { Id = genreId, Description = "CreateGenreTestDescription", Name = "CreateGenreTestName" };
            var genres = TestData.GetGenreEntities();
            var context = FilmoContextFactory.Create("GenreTestDb");
            context.Genres.AddRange(genres);
            context.SaveChanges();
            var repo = new GenreRepository(context);

            //Act
            var result = await repo.CreateAsync(createGenre, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(createGenre, result);
        }

        [Fact]
        public async Task CreateGenreExisted_Failed()
        {
            //Arrange
            var genreId = 1;
            var createGenre = new GenreEntity { Id = genreId, Description = "CreateGenreTestDescription", Name = "CreateGenreTestName" };
            var genre = TestData.GetGenreEntities().First();
            var context = FilmoContextFactory.Create("GenreTestDb");
            context.Genres.Add(genre);
            context.SaveChanges();
            context.Entry(genre).State = EntityState.Detached;
            var repo = new GenreRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await repo.CreateAsync(createGenre, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }

        [Fact]
        public void DeleteGenreExisted_Succes()
        {
            var genreId = 1;
            var deleteGenre = new GenreEntity { Id = genreId, Description = "DeleteGenreTestDescription", Name = "DeleteGenreTestName" };
            var genre = TestData.GetGenreEntities().First();
            var context = FilmoContextFactory.Create("GenreTestDb");
            context.Genres.Add(genre);
            context.SaveChanges();
            context.Entry(genre).State = EntityState.Detached;
            var repo = new GenreRepository(context);

            //Act
            var result = repo.DeleteAsync(deleteGenre, new CancellationToken()).IsFaulted;
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteGenreNonExisted_Failed()
        {
            //Arrange
            var genreId = 4;
            var genre = new GenreEntity { Id = genreId, Description = "DeleteGenreTestDescription", Name = "DeleteGenreTestName" };
            var genres = TestData.GetGenreEntities();
            var context = FilmoContextFactory.Create("GenreTestDb");
            context.Genres.AddRange(genres);
            context.SaveChanges();
            var repo = new GenreRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repo.DeleteAsync(genre, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }
    }
}
