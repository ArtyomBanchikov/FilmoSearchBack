using AutoMapper;
using FilmoSearch.Api.Controllers;
using FilmoSearch.Api.ViewModels.Genre;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FilmoSearch.Tests.Tests.Genre
{
    public class GenreControllerTest
    {
        private readonly Mock<IGenericService<GenreModel>> _genreService;
        private readonly GenericController<GenreViewModel, AddGenreViewModel, GenreModel> _genreController;
        private readonly IMapper _mapper;
        public GenreControllerTest()
        {
            _genreService = new Mock<IGenericService<GenreModel>>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GenreViewModel, GenreModel>();
                cfg.CreateMap<GenreModel, GenreViewModel>();

                cfg.CreateMap<GenreModel, GenreEntity>();
                cfg.CreateMap<GenreEntity, GenreModel>();

                cfg.CreateMap<AddGenreViewModel, GenreModel>();
                cfg.CreateMap<GenreModel, AddGenreViewModel>();
            });
            _mapper = mapperConfig.CreateMapper();
            _genreController = new GenericController<GenreViewModel, AddGenreViewModel, GenreModel>(_genreService.Object, _mapper);
        }

        [Fact]
        public async Task GetGenreExisted_Success()
        {
            //Arrange
            var genreId = 1;
            var genreEntity = TestData.GetGenreEntities().First();
            var genreModel = _mapper.Map<GenreModel>(genreEntity);
            var genreViewModel = _mapper.Map<GenreViewModel>(genreModel);
            _genreService.Setup(service => service.GetByIdAsync(genreId, new CancellationToken())).Returns(Task.FromResult(genreModel));

            //Act
            var result = await _genreController.GetByIdAsync(genreId, new CancellationToken());

            //Assert
            Assert.Equivalent(genreModel, result);
        }

        [Fact]
        public async Task GetGenreNonExisted_Fails()
        {
            //Arrange
            var genreId = 4;
            _genreService.Setup(service => service.GetByIdAsync(genreId, new CancellationToken())).Returns(Task.FromResult((GenreModel)null));

            //Act
            var result = await _genreController.GetByIdAsync(genreId, new CancellationToken());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllGenres_Success()
        {
            //Arrange
            var genreEntities = TestData.GetGenreEntities();
            var genreModels = _mapper.Map<IEnumerable<GenreModel>>(TestData.GetGenreEntities());
            var genreViewModels = _mapper.Map<IEnumerable<GenreViewModel>>(genreModels);
            _genreService.Setup(service => service.GetAllAsync(new CancellationToken())).Returns(Task.FromResult(genreModels));

            //Act
            var result = await _genreController.GetAllAsync(new CancellationToken());

            //Assert
            Assert.Equivalent(genreModels, result);
        }

        [Fact]
        public async Task UpdateGenreExisted_Success()
        {
            //Arrange
            var genreEntity = TestData.GetGenreEntities().First();
            var genreModel = _mapper.Map<GenreModel>(genreEntity);
            var genreViewModel = _mapper.Map<GenreViewModel>(genreModel);
            //Act
            var result = await _genreController.UpdateAsync(genreViewModel, new CancellationToken());

            //Assert
            _genreService.Verify(service => service.UpdateAsync(It.IsAny<GenreModel>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task UpdateGenreNonExisted_Fails()
        {
            //Arrange
            var genreViewModel = new GenreViewModel { Id = 4, Name = "UpdateGenreTestName", Description = "UpdateGenreTestDescription" };
            _genreService.Setup(service => service.UpdateAsync(It.IsAny<GenreModel>(), new CancellationToken())).Throws(new DbUpdateConcurrencyException());

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _genreController.UpdateAsync(genreViewModel, new CancellationToken()));
        }

        [Fact]
        public async Task CreateGenre_Success()
        {
            //Arrange
            var addGenreViewModel = new AddGenreViewModel { Name = "UpdateGenreTestName", Description = "UpdateGenreTestDescription" };

            //Act
            var result = await _genreController.CreateAsync(addGenreViewModel, new CancellationToken());

            //Assert
            _genreService.Verify(service => service.CreateAsync(It.IsAny<GenreModel>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task DeleteGenre_Success()
        {
            //Arrange
            var genreId = 1;

            //Act
            await _genreController.DeleteAsync(genreId, new CancellationToken());

            //Assert
            _genreService.Verify(service => service.DeleteAsync(genreId, new CancellationToken()), Times.Once());
        }
    }
}
