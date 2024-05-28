using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Repositories;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Tests.Tests.Actor
{
    public class ActorRepositoryTests
    {

        [Fact]
        public async Task GetActorExisted_Succes()
        {
            //Arrange
            var actorId = 1;
            var actor = TestData.GetActorEntities()[0];
            var films = TestData.GetFilmEntities();
            actor.Films = new List<FilmEntity> { films[0], films[2] };
            var context = FilmoContextFactory.Create("ActorTestDb");
            context.Films.AddRange(films);
            context.Actors.Add(actor);
            context.SaveChanges();
            var repo = new ActorRepository(context);

            //Act
            var result = await repo.GetByIdAsync(actorId, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(actor, result);
        }
        
        [Fact]
        public async Task GetActorNonExisted_Fails()
        {
            //Arrange
            var actorId = 4;
            var actors = TestData.GetActorEntities();
            var context = FilmoContextFactory.Create("ActorTestDb");
            context.Actors.AddRange(actors);
            context.SaveChanges();
            var repo = new ActorRepository(context);

            //Act
            var result = await repo.GetByIdAsync(actorId, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Null(result);
        }
        
        [Fact]
        public async Task GetAllActors_Succes()
        {
            //Arrange
            var actors = TestData.GetActorEntities();
            var context = FilmoContextFactory.Create("ActorTestDb");
            context.Actors.AddRange(actors);
            context.SaveChanges();
            var repo = new ActorRepository(context);

            //Act
            var result = await repo.GetAllAsync(new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equivalent(actors, result);
        }
        
        [Fact]
        public async Task UpdateActorExisted_Succes()
        {
            var actorId = 1;
            var updateActor = new ActorEntity { Id = actorId, Birthday = new DateOnly(1880, 1, 1), FirstName = "UpdateActorTestDbName", LastName = "UpdateActorTestDbLastName" };
            var actor = TestData.GetActorEntities().First();
            var context = FilmoContextFactory.Create("ActorTestDb");
            context.Actors.Add(actor);
            context.SaveChanges();
            context.Entry(actor).State = EntityState.Detached;
            var repo = new ActorRepository(context);

            //Act
            var result = await repo.UpdateAsync(updateActor, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(updateActor, result);
        }
        
        [Fact]
        public async Task UpdateActorNonExisted_Failed()
        {
            //Arrange
            var actorId = 4;
            var updateActor = new ActorEntity { Id = actorId, Birthday = new DateOnly(1880, 1, 1), FirstName = "UpdateActorTestDbName", LastName = "UpdateActorTestDbLastName" };
            var actor = TestData.GetActorEntities().First();
            var context = FilmoContextFactory.Create("ActorTestDb");
            context.Actors.Add(actor);
            context.SaveChanges();
            var repo = new ActorRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repo.UpdateAsync(updateActor, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }

        [Fact]
        public async Task CreateActorNonExisted_Succes()
        {
            var actorId = 4;
            var createActor = new ActorEntity { Id = actorId, Birthday = new DateOnly(1880, 1, 1), FirstName = "CreateActorTestDbName", LastName = "CreateActorTestDbLastName" };
            var actors = TestData.GetActorEntities();
            var context = FilmoContextFactory.Create("ActorTestDb");
            context.Actors.AddRange(actors);
            context.SaveChanges();
            var repo = new ActorRepository(context);

            //Act
            var result = await repo.CreateAsync(createActor, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(createActor, result);
        }

        [Fact]
        public async Task CreateActorExisted_Failed()
        {
            //Arrange
            var actorId = 1;
            var createActor = new ActorEntity { Id = actorId, Birthday = new DateOnly(1880, 1, 1), FirstName = "CreateActorTestDbName", LastName = "CreateActorTestDbLastName" };
            var actor = TestData.GetActorEntities().First();
            var context = FilmoContextFactory.Create("ActorTestDb");
            context.Actors.Add(actor);
            context.SaveChanges();
            context.Entry(actor).State = EntityState.Detached;
            var repo = new ActorRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await repo.CreateAsync(createActor, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }

        [Fact]
        public void DeleteActorExisted_Succes()
        {
            var actorId = 1;
            var deleteActor = new ActorEntity { Id = actorId, Birthday = new DateOnly(1880, 1, 1), FirstName = "DeleteActorTestDbName", LastName = "DeleteActorTestDbLastName" };
            var actor = TestData.GetActorEntities().First();
            var context = FilmoContextFactory.Create("ActorTestDb");
            context.Actors.Add(actor);
            context.SaveChanges();
            context.Entry(actor).State = EntityState.Detached;
            var repo = new ActorRepository(context);

            //Act
            var result = repo.DeleteAsync(deleteActor, new CancellationToken()).IsFaulted;
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.False(result);
        }
        
        [Fact]
        public async Task DeleteActorNonExisted_Failed()
        {
            //Arrange
            var actorId = 4;
            var actor = new ActorEntity { Id = actorId, Birthday = new DateOnly(1880, 1, 1), FirstName = "DeleteActorTestDbName", LastName = "DeleteActorTestDbLastName" };
            var actors = TestData.GetActorEntities();
            var context = FilmoContextFactory.Create("ActorTestDb");
            context.Actors.AddRange(actors);
            context.SaveChanges();
            var repo = new ActorRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repo.DeleteAsync(actor, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }
    }
}
