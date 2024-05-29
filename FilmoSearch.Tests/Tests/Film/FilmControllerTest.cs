using AutoMapper;
using FilmoSearch.Api.Controllers;
using FilmoSearch.Api.ViewModels.Film;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FilmoSearch.Tests.Tests.Film
{
    public class FilmControllerTest
    {
        private readonly Mock<IGenericService<FilmModel>> _filmService;
        private readonly GenericController<FilmViewModel, AddFilmViewModel, FilmModel> _filmController;
        private readonly IMapper _mapper;
        public FilmControllerTest()
        {
            _filmService = new Mock<IGenericService<FilmModel>>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FilmViewModel, FilmModel>();
                cfg.CreateMap<FilmModel, FilmViewModel>();

                cfg.CreateMap<FilmModel, FilmEntity>();
                cfg.CreateMap<FilmEntity, FilmModel>();

                cfg.CreateMap<AddFilmViewModel, FilmModel>();
                cfg.CreateMap<FilmModel, AddFilmViewModel>();
            });
            _mapper = mapperConfig.CreateMapper();
            _filmController = new GenericController<FilmViewModel, AddFilmViewModel, FilmModel>(_filmService.Object, _mapper);
        }

        [Fact]
        public async Task GetFilmExisted_Success()
        {
            //Arrange
            var filmId = 1;
            var filmEntity = TestData.GetFilmEntities().First();
            var filmModel = _mapper.Map<FilmModel>(filmEntity);
            var filmViewModel = _mapper.Map<FilmViewModel>(filmModel);
            _filmService.Setup(service => service.GetByIdAsync(filmId, new CancellationToken())).Returns(Task.FromResult(filmModel));

            //Act
            var result = await _filmController.GetByIdAsync(filmId, new CancellationToken());

            //Assert
            Assert.Equivalent(filmModel, result);
        }

        [Fact]
        public async Task GetFilmNonExisted_Fails()
        {
            //Arrange
            var filmId = 4;
            _filmService.Setup(service => service.GetByIdAsync(filmId, new CancellationToken())).Returns(Task.FromResult((FilmModel)null));

            //Act
            var result = await _filmController.GetByIdAsync(filmId, new CancellationToken());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllFilms_Success()
        {
            //Arrange
            var filmEntities = TestData.GetFilmEntities();
            var filmModels = _mapper.Map<IEnumerable<FilmModel>>(TestData.GetFilmEntities());
            var filmViewModels = _mapper.Map<IEnumerable<FilmViewModel>>(filmModels);
            _filmService.Setup(service => service.GetAllAsync(new CancellationToken())).Returns(Task.FromResult(filmModels));

            //Act
            var result = await _filmController.GetAllAsync(new CancellationToken());

            //Assert
            Assert.Equivalent(filmModels, result);
        }

        [Fact]
        public async Task UpdateFilmExisted_Success()
        {
            //Arrange
            var filmEntity = TestData.GetFilmEntities().First();
            var filmModel = _mapper.Map<FilmModel>(filmEntity);
            var filmViewModel = _mapper.Map<FilmViewModel>(filmModel);
            //Act
            var result = await _filmController.UpdateAsync(filmViewModel, new CancellationToken());

            //Assert
            _filmService.Verify(service => service.UpdateAsync(It.IsAny<FilmModel>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task UpdateFilmNonExisted_Fails()
        {
            //Arrange
            var filmViewModel = new FilmViewModel { Id = 4, PremiereDate = new DateOnly(2022, 1, 1), Title = "UpdateFilmTestTitle", Description = "UpdateFilmDescription" };
            _filmService.Setup(service => service.UpdateAsync(It.IsAny<FilmModel>(), new CancellationToken())).Throws(new DbUpdateConcurrencyException());

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _filmController.UpdateAsync(filmViewModel, new CancellationToken()));
        }

        [Fact]
        public async Task CreateFilm_Success()
        {
            //Arrange
            var addFilmViewModel = new AddFilmViewModel { PremiereDate = new DateOnly(2022, 1, 1), Title = "CreateFilmTestTitle", Description = "CreateFilmDescription" };

            //Act
            var result = await _filmController.CreateAsync(addFilmViewModel, new CancellationToken());

            //Assert
            _filmService.Verify(service => service.CreateAsync(It.IsAny<FilmModel>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task DeleteFilm_Success()
        {
            //Arrange
            var filmId = 1;

            //Act
            await _filmController.DeleteAsync(filmId, new CancellationToken());

            //Assert
            _filmService.Verify(service => service.DeleteAsync(filmId, new CancellationToken()), Times.Once());
        }
    }
}
