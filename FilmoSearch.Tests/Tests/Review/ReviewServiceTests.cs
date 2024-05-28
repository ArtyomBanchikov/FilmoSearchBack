using AutoMapper;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using FilmoSearch.Bll.Services;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Interfaces;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FilmoSearch.Tests.Tests.Review
{
    public class ReviewServiceTests
    {
        private readonly Mock<IGenericRepository<ReviewEntity>> _reviewRepository;
        private readonly IGenericService<ReviewModel> _reviewService;
        private readonly IMapper _mapper;

        public ReviewServiceTests()
        {
            _reviewRepository = new Mock<IGenericRepository<ReviewEntity>>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReviewEntity, ReviewModel>();
                cfg.CreateMap<ReviewModel, ReviewEntity>();
            });
            _mapper = mapperConfig.CreateMapper();
            _reviewService = new GenericService<ReviewModel, ReviewEntity>(_reviewRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetReviewExisted_Success()
        {
            //Arrange
            var reviewId = 1;
            var reviewEntity = TestData.GetReviewEntities().First();
            var reviewModel = _mapper.Map<ReviewModel>(reviewEntity);
            _reviewRepository.Setup(repo => repo.GetByIdAsync(reviewId, new CancellationToken())).Returns(Task.FromResult(TestData.GetReviewEntities().First()));

            //Act
            var result = await _reviewService.GetByIdAsync(reviewId, new CancellationToken());

            //Assert
            Assert.Equivalent(reviewModel, result);
        }

        [Fact]
        public async Task GetReviewNonExisted_Fails()
        {
            //Arrange
            var reviewId = 4;
            _reviewRepository.Setup(repo => repo.GetByIdAsync(reviewId, new CancellationToken())).Returns(Task.FromResult((ReviewEntity)null));

            //Act
            var result = await _reviewService.GetByIdAsync(reviewId, new CancellationToken());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllReviews_Success()
        {
            //Arrange
            var actorModels = _mapper.Map<IEnumerable<ReviewModel>>(TestData.GetReviewEntities());
            _reviewRepository.Setup(repo => repo.GetAllAsync(new CancellationToken())).Returns(Task.FromResult(TestData.GetReviewEntities().AsEnumerable()));

            //Act
            var result = await _reviewService.GetAllAsync(new CancellationToken());

            //Assert
            Assert.Equivalent(actorModels, result);
        }

        [Fact]
        public async Task UpdateReviewExisted_Success()
        {
            //Arrange
            var reviewEntity = TestData.GetReviewEntities().First();
            var reviewModel = _mapper.Map<ReviewModel>(reviewEntity);

            //Act
            var result = await _reviewService.UpdateAsync(reviewModel, new CancellationToken());

            //Assert
            _reviewRepository.Verify(repo => repo.UpdateAsync(It.IsAny<ReviewEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task UpdateReviewNonExisted_Fails()
        {
            //Arrange
            var reviewModel = new ReviewModel { Id = 4, Title = "UpdateReviewTestTitle", Description = "UpdateReviewTestDescription", FilmId = 3, UserId = 3, Stars = 4 };
            _reviewRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ReviewEntity>(), new CancellationToken())).Throws(new DbUpdateConcurrencyException());

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _reviewService.UpdateAsync(reviewModel, new CancellationToken()));
        }

        [Fact]
        public async Task CreateReviewNonExisted_Success()
        {
            //Arrange
            var reviewModel = new ReviewModel { Id = 4, Title = "CreateReviewTestTitle", Description = "CreateReviewTestDescription", FilmId = 3, UserId = 3, Stars = 4 };

            //Act
            var result = await _reviewService.CreateAsync(reviewModel, new CancellationToken());

            //Assert
            _reviewRepository.Verify(repo => repo.CreateAsync(It.IsAny<ReviewEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task CreateReviewExisted_Fails()
        {
            //Arrange
            var reviewEntity = TestData.GetReviewEntities().First();
            var reviewModel = _mapper.Map<ReviewModel>(reviewEntity);
            _reviewRepository.Setup(repo => repo.CreateAsync(It.IsAny<ReviewEntity>(), new CancellationToken())).Throws(new ArgumentException());

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _reviewService.CreateAsync(reviewModel, new CancellationToken()));
        }

        [Fact]
        public async Task DeleteReviewExisted_Success()
        {
            //Arrange
            var reviewId = 1;

            //Act
            await _reviewService.DeleteAsync(reviewId, new CancellationToken());

            //Assert
            _reviewRepository.Verify(repo => repo.GetByIdAsync(reviewId, new CancellationToken()), Times.Once());
            _reviewRepository.Verify(repo => repo.DeleteAsync(It.IsAny<ReviewEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task DeleteReviewNonExisted_Fails()
        {
            //Arrange
            var reviewId = 4;
            _reviewRepository.Setup(repo => repo.GetByIdAsync(reviewId, new CancellationToken())).Returns(Task.FromResult((ReviewEntity)null));

            //Act
            await _reviewService.DeleteAsync(reviewId, new CancellationToken());

            //Assert
            _reviewRepository.Verify(repo => repo.DeleteAsync(null, new CancellationToken()), Times.Once());
        }
    }
}
