using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Repositories;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Tests.Tests.User
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task GetUserExisted_Succes()
        {
            //Arrange
            var userId = 1;
            var user = TestData.GetUserEntities()[0];
            var reviews = TestData.GetReviewEntities();
            user.Reviews = new List<ReviewEntity> { reviews[0]};
            var context = FilmoContextFactory.Create("UserTestDb");
            context.Users.Add(user);
            context.SaveChanges();
            var repo = new UserRepository(context);

            //Act
            var result = await repo.GetByIdAsync(userId, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserNonExisted_Fails()
        {
            //Arrange
            var userId = 4;
            var users = TestData.GetUserEntities();
            var context = FilmoContextFactory.Create("UserTestDb");
            context.Users.AddRange(users);
            context.SaveChanges();
            var repo = new UserRepository(context);

            //Act
            var result = await repo.GetByIdAsync(userId, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllUsers_Succes()
        {
            //Arrange
            var users = TestData.GetUserEntities();
            var context = FilmoContextFactory.Create("UserTestDb");
            context.Users.AddRange(users);
            context.SaveChanges();
            var repo = new UserRepository(context);

            //Act
            var result = await repo.GetAllAsync(new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equivalent(users, result);
        }

        [Fact]
        public async Task UpdateUserExisted_Succes()
        {
            var userId = 1;
            var updateUser = new UserEntity
            {
                Id = userId,
                RegistrationDate = new DateOnly(1880, 1, 1),
                Name = "UpdateUserTestName",
                Lastname = "UpdateUserTestLastname",
                Password = "UpdateTestUserTestPassword",
                Email = "UpdateTestUserEmail"
            };
            var user = TestData.GetUserEntities().First();
            var context = FilmoContextFactory.Create("UserTestDb");
            context.Users.Add(user);
            context.SaveChanges();
            context.Entry(user).State = EntityState.Detached;
            var repo = new UserRepository(context);

            //Act
            var result = await repo.UpdateAsync(updateUser, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(updateUser, result);
        }

        [Fact]
        public async Task UpdateUserNonExisted_Failed()
        {
            //Arrange
            var userId = 4;
            var updateUser = new UserEntity
            {
                Id = userId,
                RegistrationDate = new DateOnly(1880, 1, 1),
                Name = "UpdateUserTestName",
                Lastname = "UpdateUserTestLastname",
                Password = "UpdateTestUserTestPassword",
                Email = "UpdateTestUserEmail"
            };
            var user = TestData.GetUserEntities().First();
            var context = FilmoContextFactory.Create("UserTestDb");
            context.Users.Add(user);
            context.SaveChanges();
            var repo = new UserRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repo.UpdateAsync(updateUser, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }

        [Fact]
        public async Task CreateUserNonExisted_Succes()
        {
            var userId = 4;
            var createUser = new UserEntity
            {
                Id = userId,
                RegistrationDate = new DateOnly(1880, 1, 1),
                Name = "CreateUserTestName",
                Lastname = "CreateUserTestLastname",
                Password = "CreateTestUserTestPassword",
                Email = "CreateTestUserEmail"
            };
            var users = TestData.GetUserEntities();
            var context = FilmoContextFactory.Create("UserTestDb");
            context.Users.AddRange(users);
            context.SaveChanges();
            var repo = new UserRepository(context);

            //Act
            var result = await repo.CreateAsync(createUser, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(createUser, result);
        }

        [Fact]
        public async Task CreateUserExisted_Failed()
        {
            //Arrange
            var userId = 1;
            var createUser = new UserEntity
            {
                Id = userId,
                RegistrationDate = new DateOnly(1880, 1, 1),
                Name = "CreateUserTestName",
                Lastname = "CreateUserTestLastname",
                Password = "CreateTestUserTestPassword",
                Email = "CreateTestUserEmail"
            };
            var user = TestData.GetUserEntities().First();
            var context = FilmoContextFactory.Create("UserTestDb");
            context.Users.Add(user);
            context.SaveChanges();
            context.Entry(user).State = EntityState.Detached;
            var repo = new UserRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await repo.CreateAsync(createUser, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }

        [Fact]
        public void DeleteUserExisted_Succes()
        {
            var userId = 1;
            var deleteUser = new UserEntity
            {
                Id = userId,
                RegistrationDate = new DateOnly(1880, 1, 1),
                Name = "DeleteUserTestName",
                Lastname = "DeleteUserTestLastname",
                Password = "DeleteTestUserTestPassword",
                Email = "DeleteTestUserEmail"
            };
            var user = TestData.GetUserEntities().First();
            var context = FilmoContextFactory.Create("UserTestDb");
            context.Users.Add(user);
            context.SaveChanges();
            context.Entry(user).State = EntityState.Detached;
            var repo = new UserRepository(context);

            //Act
            var result = repo.DeleteAsync(deleteUser, new CancellationToken()).IsFaulted;
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserNonExisted_Failed()
        {
            //Arrange
            var userId = 4;
            var user = new UserEntity
            {
                Id = userId,
                RegistrationDate = new DateOnly(1880, 1, 1),
                Name = "DeleteUserTestName",
                Lastname = "DeleteUserTestLastname",
                Password = "DeleteTestUserTestPassword",
                Email = "DeleteTestUserEmail"
            };
            var users = TestData.GetUserEntities();
            var context = FilmoContextFactory.Create("UserTestDb");
            context.Users.AddRange(users);
            context.SaveChanges();
            var repo = new UserRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repo.DeleteAsync(user, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }
    }
}
