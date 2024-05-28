using AutoMapper;
using FilmoSearch.Api.Controllers;
using FilmoSearch.Api.ViewModels.Review;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FilmoSearch.Tests.Tests.Review
{
    public class ReviewControllerTest
    {
        private readonly Mock<IGenericService<ReviewModel>> _reviewService;
        private readonly GenericController<ReviewViewModel, AddReviewViewModel, ReviewModel> _reviewController;
        private readonly IMapper _mapper;
        public ReviewControllerTest()
        {
            _reviewService = new Mock<IGenericService<ReviewModel>>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReviewViewModel, ReviewModel>();
                cfg.CreateMap<ReviewModel, ReviewViewModel>();

                cfg.CreateMap<ReviewModel, ReviewEntity>();
                cfg.CreateMap<ReviewEntity, ReviewModel>();

                cfg.CreateMap<AddReviewViewModel, ReviewModel>();
                cfg.CreateMap<ReviewModel, AddReviewViewModel>();
            });
            _mapper = mapperConfig.CreateMapper();
            _reviewController = new GenericController<ReviewViewModel, AddReviewViewModel, ReviewModel>(_reviewService.Object, _mapper);
        }

        [Fact]
        public async Task GetReviewExisted_Success()
        {
            //Arrange
            var reviewId = 1;
            var reviewEntity = TestData.GetReviewEntities().First();
            var reviewModel = _mapper.Map<ReviewModel>(reviewEntity);
            var reviewViewModel = _mapper.Map<ReviewViewModel>(reviewModel);
            _reviewService.Setup(service => service.GetByIdAsync(reviewId, new CancellationToken())).Returns(Task.FromResult(reviewModel));

            //Act
            var result = await _reviewController.GetByIdAsync(reviewId, new CancellationToken());

            //Assert
            Assert.Equivalent(reviewModel, result);
        }

        [Fact]
        public async Task GetReviewNonExisted_Fails()
        {
            //Arrange
            var reviewId = 4;
            _reviewService.Setup(service => service.GetByIdAsync(reviewId, new CancellationToken())).Returns(Task.FromResult((ReviewModel)null));

            //Act
            var result = await _reviewController.GetByIdAsync(reviewId, new CancellationToken());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllReviews_Success()
        {
            //Arrange
            var reviewEntities = TestData.GetReviewEntities();
            var reviewModels = _mapper.Map<IEnumerable<ReviewModel>>(TestData.GetReviewEntities());
            var reviewViewModels = _mapper.Map<IEnumerable<ReviewViewModel>>(reviewModels);
            _reviewService.Setup(service => service.GetAllAsync(new CancellationToken())).Returns(Task.FromResult(reviewModels));

            //Act
            var result = await _reviewController.GetAllAsync(new CancellationToken());

            //Assert
            Assert.Equivalent(reviewModels, result);
        }

        [Fact]
        public async Task UpdateReviewExisted_Success()
        {
            //Arrange
            var reviewEntity = TestData.GetReviewEntities().First();
            var reviewModel = _mapper.Map<ReviewModel>(reviewEntity);
            var reviewViewModel = _mapper.Map<ReviewViewModel>(reviewModel);
            //Act
            var result = await _reviewController.UpdateAsync(reviewViewModel, new CancellationToken());

            //Assert
            _reviewService.Verify(service => service.UpdateAsync(It.IsAny<ReviewModel>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task UpdateReviewNonExisted_Fails()
        {
            //Arrange
            var reviewViewModel = new ReviewViewModel { Id = 4, Description = "UpgradeReviewTestDescription", Title = "UpgradeReviewTestTitile", FilmId = 2, UserId = 2, Stars = 5 };
            _reviewService.Setup(service => service.UpdateAsync(It.IsAny<ReviewModel>(), new CancellationToken())).Throws(new DbUpdateConcurrencyException());

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _reviewController.UpdateAsync(reviewViewModel, new CancellationToken()));
        }

        [Fact]
        public async Task CreateReview_Success()
        {
            //Arrange
            var addReviewViewModel = new AddReviewViewModel { Description = "UpgradeReviewTestDescription", Title = "UpgradeReviewTestTitile", FilmId = 2, UserId = 2, Stars = 5 };

            //Act
            var result = await _reviewController.CreateAsync(addReviewViewModel, new CancellationToken());

            //Assert
            _reviewService.Verify(service => service.CreateAsync(It.IsAny<ReviewModel>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task DeleteReview_Success()
        {
            //Arrange
            var reviewId = 1;

            //Act
            await _reviewController.DeleteAsync(reviewId, new CancellationToken());

            //Assert
            _reviewService.Verify(service => service.DeleteAsync(reviewId, new CancellationToken()), Times.Once());
        }
    }
}
