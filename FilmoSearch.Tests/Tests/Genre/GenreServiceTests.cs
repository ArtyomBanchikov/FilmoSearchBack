using AutoMapper;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using FilmoSearch.Bll.Services;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Interfaces;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmoSearch.Tests.Tests.Genre
{
    public class GenreServiceTests
    {
        private readonly Mock<IGenericRepository<GenreEntity>> _genreRepository;
        private readonly IGenericService<GenreModel> _genreService;
        private readonly IMapper _mapper;

        public GenreServiceTests()
        {
            _genreRepository = new Mock<IGenericRepository<GenreEntity>>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GenreEntity, GenreModel>();
                cfg.CreateMap<GenreModel, GenreEntity>();
            });
            _mapper = mapperConfig.CreateMapper();
            _genreService = new GenericService<GenreModel, GenreEntity>(_genreRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetGenreExisted_Success()
        {
            //Arrange
            var genreId = 1;
            var genreEntity = TestData.GetGenreEntities().First();
            var genreModel = _mapper.Map<GenreModel>(genreEntity);
            _genreRepository.Setup(repo => repo.GetByIdAsync(genreId, new CancellationToken())).Returns(Task.FromResult(TestData.GetGenreEntities().First()));

            //Act
            var result = await _genreService.GetByIdAsync(genreId, new CancellationToken());

            //Assert
            Assert.Equivalent(genreModel, result);
        }

        [Fact]
        public async Task GetGenreNonExisted_Fails()
        {
            //Arrange
            var genreId = 4;
            _genreRepository.Setup(repo => repo.GetByIdAsync(genreId, new CancellationToken())).Returns(Task.FromResult((GenreEntity)null));

            //Act
            var result = await _genreService.GetByIdAsync(genreId, new CancellationToken());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllGenres_Success()
        {
            //Arrange
            var actorModels = _mapper.Map<IEnumerable<GenreModel>>(TestData.GetGenreEntities());
            _genreRepository.Setup(repo => repo.GetAllAsync(new CancellationToken())).Returns(Task.FromResult(TestData.GetGenreEntities().AsEnumerable()));

            //Act
            var result = await _genreService.GetAllAsync(new CancellationToken());

            //Assert
            Assert.Equivalent(actorModels, result);
        }

        [Fact]
        public async Task UpdateGenreExisted_Success()
        {
            //Arrange
            var genreEntity = TestData.GetGenreEntities().First();
            var genreModel = _mapper.Map<GenreModel>(genreEntity);

            //Act
            var result = await _genreService.UpdateAsync(genreModel, new CancellationToken());

            //Assert
            _genreRepository.Verify(repo => repo.UpdateAsync(It.IsAny<GenreEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task UpdateGenreNonExisted_Fails()
        {
            //Arrange
            var genreModel = new GenreModel { Id = 4, Name = "UpdateGenreTestName", Description = "UpdateGenreTestDescription" };
            _genreRepository.Setup(repo => repo.UpdateAsync(It.IsAny<GenreEntity>(), new CancellationToken())).Throws(new DbUpdateConcurrencyException());

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _genreService.UpdateAsync(genreModel, new CancellationToken()));
        }

        [Fact]
        public async Task CreateGenreNonExisted_Success()
        {
            //Arrange
            var genreModel = new GenreModel { Id = 4, Name = "CreateGenreTestName", Description = "CreateGenreTestDescription" };

            //Act
            var result = await _genreService.CreateAsync(genreModel, new CancellationToken());

            //Assert
            _genreRepository.Verify(repo => repo.CreateAsync(It.IsAny<GenreEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task CreateGenreExisted_Fails()
        {
            //Arrange
            var genreEntity = TestData.GetGenreEntities().First();
            var genreModel = _mapper.Map<GenreModel>(genreEntity);
            _genreRepository.Setup(repo => repo.CreateAsync(It.IsAny<GenreEntity>(), new CancellationToken())).Throws(new ArgumentException());

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _genreService.CreateAsync(genreModel, new CancellationToken()));
        }

        [Fact]
        public async Task DeleteGenreExisted_Success()
        {
            //Arrange
            var genreId = 1;

            //Act
            await _genreService.DeleteAsync(genreId, new CancellationToken());

            //Assert
            _genreRepository.Verify(repo => repo.GetByIdAsync(genreId, new CancellationToken()), Times.Once());
            _genreRepository.Verify(repo => repo.DeleteAsync(It.IsAny<GenreEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task DeleteGenreNonExisted_Fails()
        {
            //Arrange
            var genreId = 4;
            _genreRepository.Setup(repo => repo.GetByIdAsync(genreId, new CancellationToken())).Returns(Task.FromResult((GenreEntity)null));

            //Act
            await _genreService.DeleteAsync(genreId, new CancellationToken());

            //Assert
            _genreRepository.Verify(repo => repo.DeleteAsync(null, new CancellationToken()), Times.Once());
        }
    }
}
