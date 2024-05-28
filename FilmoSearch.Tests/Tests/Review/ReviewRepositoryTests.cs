using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Repositories;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Tests.Tests.Review
{
    public class ReviewRepositoryTests
    {
        [Fact]
        public async Task GetReviewExisted_Succes()
        {
            //Arrange
            var reviewId = 1;
            var review = TestData.GetReviewEntities()[0];
            var context = FilmoContextFactory.Create("ReviewTestDb");
            context.Reviews.Add(review);
            context.SaveChanges();
            var repo = new ReviewRepository(context);

            //Act
            var result = await repo.GetByIdAsync(reviewId, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(review, result);
        }

        [Fact]
        public async Task GetReviewNonExisted_Fails()
        {
            //Arrange
            var reviewId = 4;
            var reviews = TestData.GetReviewEntities();
            var context = FilmoContextFactory.Create("ReviewTestDb");
            context.Reviews.AddRange(reviews);
            context.SaveChanges();
            var repo = new ReviewRepository(context);

            //Act
            var result = await repo.GetByIdAsync(reviewId, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllReviews_Succes()
        {
            //Arrange
            var reviews = TestData.GetReviewEntities();
            var context = FilmoContextFactory.Create("ReviewTestDb");
            context.Reviews.AddRange(reviews);
            context.SaveChanges();
            var repo = new ReviewRepository(context);

            //Act
            var result = await repo.GetAllAsync(new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equivalent(reviews, result);
        }

        [Fact]
        public async Task UpdateReviewExisted_Succes()
        {
            var reviewId = 1;
            var updateReview = new ReviewEntity { Id = reviewId, Description = "UpdateReviewTestDescription", FilmId = 1, UserId = 1, Stars = 4, Title = "UpdateReviewTestTitle" };
            var review = TestData.GetReviewEntities().First();
            var context = FilmoContextFactory.Create("ReviewTestDb");
            context.Reviews.Add(review);
            context.SaveChanges();
            context.Entry(review).State = EntityState.Detached;
            var repo = new ReviewRepository(context);

            //Act
            var result = await repo.UpdateAsync(updateReview, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(updateReview, result);
        }

        [Fact]
        public async Task UpdateReviewNonExisted_Failed()
        {
            //Arrange
            var reviewId = 4;
            var updateReview = new ReviewEntity { Id = reviewId, Description = "UpdateReviewTestDescription", FilmId = 1, UserId = 1, Stars = 4, Title = "UpdateReviewTestTitle" };
            var review = TestData.GetReviewEntities().First();
            var context = FilmoContextFactory.Create("ReviewTestDb");
            context.Reviews.Add(review);
            context.SaveChanges();
            var repo = new ReviewRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repo.UpdateAsync(updateReview, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }

        [Fact]
        public async Task CreateReviewNonExisted_Succes()
        {
            var reviewId = 4;
            var createReview = new ReviewEntity { Id = reviewId, Description = "CreateReviewTestDescription", FilmId = 1, UserId = 1, Stars = 4, Title = "CreateReviewTestTitle" };
            var reviews = TestData.GetReviewEntities();
            var context = FilmoContextFactory.Create("ReviewTestDb");
            context.Reviews.AddRange(reviews);
            context.SaveChanges();
            var repo = new ReviewRepository(context);

            //Act
            var result = await repo.CreateAsync(createReview, new CancellationToken());
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.Equal(createReview, result);
        }

        [Fact]
        public async Task CreateReviewExisted_Failed()
        {
            //Arrange
            var reviewId = 1;
            var createReview = new ReviewEntity { Id = reviewId, Description = "CreateReviewTestDescription", FilmId = 1, UserId = 1, Stars = 4, Title = "CreateReviewTestTitle" };
            var review = TestData.GetReviewEntities().First();
            var context = FilmoContextFactory.Create("ReviewTestDb");
            context.Reviews.Add(review);
            context.SaveChanges();
            context.Entry(review).State = EntityState.Detached;
            var repo = new ReviewRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await repo.CreateAsync(createReview, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }

        [Fact]
        public void DeleteReviewExisted_Succes()
        {
            var reviewId = 1;
            var deleteReview = new ReviewEntity { Id = reviewId, Description = "DeleteReviewTestDescription", FilmId = 1, UserId = 1, Stars = 4, Title = "DeleteReviewTestTitle" };
            var review = TestData.GetReviewEntities().First();
            var context = FilmoContextFactory.Create("ReviewTestDb");
            context.Reviews.Add(review);
            context.SaveChanges();
            context.Entry(review).State = EntityState.Detached;
            var repo = new ReviewRepository(context);

            //Act
            var result = repo.DeleteAsync(deleteReview, new CancellationToken()).IsFaulted;
            FilmoContextFactory.Destroy(context);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteReviewNonExisted_Failed()
        {
            //Arrange
            var reviewId = 4;
            var review = new ReviewEntity { Id = reviewId, Description = "DeleteReviewTestDescription", FilmId = 1, UserId = 1, Stars = 4, Title = "DeleteReviewTestTitle" };
            var reviews = TestData.GetReviewEntities();
            var context = FilmoContextFactory.Create("ReviewTestDb");
            context.Reviews.AddRange(reviews);
            context.SaveChanges();
            var repo = new ReviewRepository(context);

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repo.DeleteAsync(review, new CancellationToken()));
            FilmoContextFactory.Destroy(context);
        }
    }
}
