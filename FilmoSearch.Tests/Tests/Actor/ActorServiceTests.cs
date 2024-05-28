using AutoMapper;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using FilmoSearch.Bll.Services;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Interfaces;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FilmoSearch.Tests.Tests.Actor
{
    public class ActorServiceTests
    {
        private readonly Mock<IGenericRepository<ActorEntity>> _actorRepository;
        private readonly IGenericService<ActorModel> _actorService;
        private readonly IMapper _mapper;

        public ActorServiceTests()
        {
            _actorRepository = new Mock<IGenericRepository<ActorEntity>>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ActorEntity, ActorModel>();
                cfg.CreateMap<ActorModel, ActorEntity>();
            });
            _mapper = mapperConfig.CreateMapper();
            _actorService = new GenericService<ActorModel, ActorEntity>(_actorRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetActorExisted_Success()
        {
            //Arrange
            var actorId = 1;
            var actorEntity = TestData.GetActorEntities().First();
            var actorModel = _mapper.Map<ActorModel>(actorEntity);
            _actorRepository.Setup(repo => repo.GetByIdAsync(actorId, new CancellationToken())).Returns(Task.FromResult(TestData.GetActorEntities().First()));

            //Act
            var result = await _actorService.GetByIdAsync(actorId, new CancellationToken());

            //Assert
            Assert.Equivalent(actorModel, result);
        }

        [Fact]
        public async Task GetActorNonExisted_Fails()
        {
            //Arrange
            var actorId = 4;
            _actorRepository.Setup(repo => repo.GetByIdAsync(actorId, new CancellationToken())).Returns(Task.FromResult((ActorEntity)null));

            //Act
            var result = await _actorService.GetByIdAsync(actorId, new CancellationToken());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllActors_Success()
        {
            //Arrange
            var actorModels = _mapper.Map<IEnumerable<ActorModel>>(TestData.GetActorEntities());
            _actorRepository.Setup(repo => repo.GetAllAsync(new CancellationToken())).Returns(Task.FromResult(TestData.GetActorEntities().AsEnumerable()));

            //Act
            var result = await _actorService.GetAllAsync(new CancellationToken());

            //Assert
            Assert.Equivalent(actorModels, result);
        }

        [Fact]
        public async Task UpdateActorExisted_Success()
        {
            //Arrange
            var actorEntity = TestData.GetActorEntities().First();
            var actorModel = _mapper.Map<ActorModel>(actorEntity);

            //Act
            var result = await _actorService.UpdateAsync(actorModel, new CancellationToken());

            //Assert
            _actorRepository.Verify(repo => repo.UpdateAsync(It.IsAny<ActorEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task UpdateActorNonExisted_Fails()
        {
            //Arrange
            var actorModel = new ActorModel { Id = 4, Birthday = new DateOnly(1981, 1, 1), FirstName = "UpdateActorTestName", LastName = "UpdateActorTestLastName" };
            _actorRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ActorEntity>(), new CancellationToken())).Throws(new DbUpdateConcurrencyException());

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _actorService.UpdateAsync(actorModel, new CancellationToken()));
        }

        [Fact]
        public async Task CreateActorNonExisted_Success()
        {
            //Arrange
            var actorModel = new ActorModel { Id = 4, Birthday = new DateOnly(1981, 1, 1), FirstName = "CreateActorTestName", LastName = "CreateActorTestLastName" };

            //Act
            var result = await _actorService.CreateAsync(actorModel, new CancellationToken());

            //Assert
            _actorRepository.Verify(repo => repo.CreateAsync(It.IsAny<ActorEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task CreateActorExisted_Fails()
        {
            //Arrange
            var actorEntity = TestData.GetActorEntities().First();
            var actorModel = _mapper.Map<ActorModel>(actorEntity);
            _actorRepository.Setup(repo => repo.CreateAsync(It.IsAny<ActorEntity>(), new CancellationToken())).Throws(new ArgumentException());

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _actorService.CreateAsync(actorModel, new CancellationToken()));
        }

        [Fact]
        public async Task DeleteActorExisted_Success()
        {
            //Arrange
            var actorId = 1;

            //Act
            await _actorService.DeleteAsync(actorId, new CancellationToken());

            //Assert
            _actorRepository.Verify(repo => repo.GetByIdAsync(actorId, new CancellationToken()), Times.Once());
            _actorRepository.Verify(repo => repo.DeleteAsync(It.IsAny<ActorEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task DeleteActorNonExisted_Fails()
        {
            //Arrange
            var actorId = 4;
            _actorRepository.Setup(repo => repo.GetByIdAsync(actorId, new CancellationToken())).Returns(Task.FromResult((ActorEntity)null));

            //Act
            await _actorService.DeleteAsync(actorId,new CancellationToken());

            //Assert
            _actorRepository.Verify(repo => repo.DeleteAsync(null, new CancellationToken()), Times.Once());
        }
    }
}
