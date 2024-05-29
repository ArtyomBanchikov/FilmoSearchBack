using AutoMapper;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using FilmoSearch.Bll.Services;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Interfaces;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FilmoSearch.Tests.Tests.Film
{
    public class FilmServiceTests
    {
        private readonly Mock<IGenericRepository<FilmEntity>> _filmRepository;
        private readonly IGenericService<FilmModel> _filmService;
        private readonly IMapper _mapper;

        public FilmServiceTests()
        {
            _filmRepository = new Mock<IGenericRepository<FilmEntity>>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FilmEntity, FilmModel>();
                cfg.CreateMap<FilmModel, FilmEntity>();
            });
            _mapper = mapperConfig.CreateMapper();
            _filmService = new GenericService<FilmModel, FilmEntity>(_filmRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetFilmExisted_Success()
        {
            //Arrange
            var filmId = 1;
            var filmEntity = TestData.GetFilmEntities().First();
            var filmModel = _mapper.Map<FilmModel>(filmEntity);
            _filmRepository.Setup(repo => repo.GetByIdAsync(filmId, new CancellationToken())).Returns(Task.FromResult(TestData.GetFilmEntities().First()));

            //Act
            var result = await _filmService.GetByIdAsync(filmId, new CancellationToken());

            //Assert
            Assert.Equivalent(filmModel, result);
        }

        [Fact]
        public async Task GetFilmNonExisted_Fails()
        {
            //Arrange
            var filmId = 4;
            _filmRepository.Setup(repo => repo.GetByIdAsync(filmId, new CancellationToken())).Returns(Task.FromResult((FilmEntity)null));

            //Act
            var result = await _filmService.GetByIdAsync(filmId, new CancellationToken());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllFilms_Success()
        {
            //Arrange
            var actorModels = _mapper.Map<IEnumerable<FilmModel>>(TestData.GetFilmEntities());
            _filmRepository.Setup(repo => repo.GetAllAsync(new CancellationToken())).Returns(Task.FromResult(TestData.GetFilmEntities().AsEnumerable()));

            //Act
            var result = await _filmService.GetAllAsync(new CancellationToken());

            //Assert
            Assert.Equivalent(actorModels, result);
        }

        [Fact]
        public async Task UpdateFilmExisted_Success()
        {
            //Arrange
            var filmEntity = TestData.GetFilmEntities().First();
            var filmModel = _mapper.Map<FilmModel>(filmEntity);

            //Act
            var result = await _filmService.UpdateAsync(filmModel, new CancellationToken());

            //Assert
            _filmRepository.Verify(repo => repo.UpdateAsync(It.IsAny<FilmEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task UpdateFilmNonExisted_Fails()
        {
            //Arrange
            var filmModel = new FilmModel { Id = 4, PremiereDate = new DateOnly(1991, 1, 1), Title = "UpdateFilmTestTitle", Description = "UpdateFilmDescription" };
            _filmRepository.Setup(repo => repo.UpdateAsync(It.IsAny<FilmEntity>(), new CancellationToken())).Throws(new DbUpdateConcurrencyException());

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _filmService.UpdateAsync(filmModel, new CancellationToken()));
        }

        [Fact]
        public async Task CreateFilmNonExisted_Success()
        {
            //Arrange
            var filmModel = new FilmModel { Id = 4, PremiereDate = new DateOnly(1991, 1, 1), Title = "UpdateFilmTestTitle", Description = "CreateFilmDescription" };

            //Act
            var result = await _filmService.CreateAsync(filmModel, new CancellationToken());

            //Assert
            _filmRepository.Verify(repo => repo.CreateAsync(It.IsAny<FilmEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task CreateFilmExisted_Fails()
        {
            //Arrange
            var filmEntity = TestData.GetFilmEntities().First();
            var filmModel = _mapper.Map<FilmModel>(filmEntity);
            _filmRepository.Setup(repo => repo.CreateAsync(It.IsAny<FilmEntity>(), new CancellationToken())).Throws(new ArgumentException());

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _filmService.CreateAsync(filmModel, new CancellationToken()));
        }

        [Fact]
        public async Task DeleteFilmExisted_Success()
        {
            //Arrange
            var filmId = 1;

            //Act
            await _filmService.DeleteAsync(filmId, new CancellationToken());

            //Assert
            _filmRepository.Verify(repo => repo.GetByIdAsync(filmId, new CancellationToken()), Times.Once());
            _filmRepository.Verify(repo => repo.DeleteAsync(It.IsAny<FilmEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task DeleteFilmNonExisted_Fails()
        {
            //Arrange
            var filmId = 4;
            _filmRepository.Setup(repo => repo.GetByIdAsync(filmId, new CancellationToken())).Returns(Task.FromResult((FilmEntity)null));

            //Act
            await _filmService.DeleteAsync(filmId, new CancellationToken());

            //Assert
            _filmRepository.Verify(repo => repo.DeleteAsync(null, new CancellationToken()), Times.Once());
        }
    }
}
