using AutoMapper;
using FilmoSearch.Api.Controllers;
using FilmoSearch.Api.ViewModels.Actor;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FilmoSearch.Tests.Tests.Actor
{
    public class ActorControllerTests
    {
        private readonly Mock<IGenericService<ActorModel>> _actorService;
        private readonly GenericController<ActorViewModel, AddActorViewModel, ActorModel> _actorController;
        private readonly IMapper _mapper;
        public ActorControllerTests()
        {
            _actorService = new Mock<IGenericService<ActorModel>>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ActorViewModel, ActorModel>();
                cfg.CreateMap<ActorModel, ActorViewModel>();

                cfg.CreateMap<ActorModel, ActorEntity>();
                cfg.CreateMap<ActorEntity, ActorModel>();

                cfg.CreateMap<AddActorViewModel, ActorModel>();
                cfg.CreateMap<ActorModel, AddActorViewModel>();
            });
            _mapper = mapperConfig.CreateMapper();
            _actorController = new GenericController<ActorViewModel, AddActorViewModel, ActorModel>(_actorService.Object, _mapper);
        }

        [Fact]
        public async Task GetActorExisted_Success()
        {
            //Arrange
            var actorId = 1;
            var actorEntity = TestData.GetActorEntities().First();
            var actorModel = _mapper.Map<ActorModel>(actorEntity);
            var actorViewModel = _mapper.Map<ActorViewModel>(actorModel);
            _actorService.Setup(service => service.GetByIdAsync(actorId, new CancellationToken())).Returns(Task.FromResult(actorModel));

            //Act
            var result = await _actorController.GetByIdAsync(actorId, new CancellationToken());

            //Assert
            Assert.Equivalent(actorModel, result);
        }

        [Fact]
        public async Task GetActorNonExisted_Fails()
        {
            //Arrange
            var actorId = 4;
            _actorService.Setup(service => service.GetByIdAsync(actorId, new CancellationToken())).Returns(Task.FromResult((ActorModel)null));

            //Act
            var result = await _actorController.GetByIdAsync(actorId, new CancellationToken());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllActors_Success()
        {
            //Arrange
            var actorEntities = TestData.GetActorEntities();
            var actorModels = _mapper.Map<IEnumerable<ActorModel>>(TestData.GetActorEntities());
            var actorViewModels = _mapper.Map<IEnumerable<ActorViewModel>>(actorModels);
            _actorService.Setup(service => service.GetAllAsync(new CancellationToken())).Returns(Task.FromResult(actorModels));

            //Act
            var result = await _actorController.GetAllAsync(new CancellationToken());

            //Assert
            Assert.Equivalent(actorModels, result);
        }

        [Fact]
        public async Task UpdateActorExisted_Success()
        {
            //Arrange
            var actorEntity = TestData.GetActorEntities().First();
            var actorModel = _mapper.Map<ActorModel>(actorEntity);
            var actorViewModel = _mapper.Map<ActorViewModel>(actorModel);
            //Act
            var result = await _actorController.UpdateAsync(actorViewModel, new CancellationToken());

            //Assert
            _actorService.Verify(service => service.UpdateAsync(It.IsAny<ActorModel>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task UpdateActorNonExisted_Fails()
        {
            //Arrange
            var actorViewModel = new ActorViewModel { Id = 4, Birthday = new DateOnly(1981, 1, 1), FirstName = "UpdateActorTestName", LastName = "UpdateActorTestLastName" };
            _actorService.Setup(service => service.UpdateAsync(It.IsAny<ActorModel>(), new CancellationToken())).Throws(new DbUpdateConcurrencyException());

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _actorController.UpdateAsync(actorViewModel, new CancellationToken()));
        }

        [Fact]
        public async Task CreateActor_Success()
        {
            //Arrange
            var addActorViewModel = new AddActorViewModel { Birthday = new DateOnly(1981, 1, 1), FirstName = "CreateActorTestName", LastName = "CreateActorTestLastName" };

            //Act
            var result = await _actorController.CreateAsync(addActorViewModel, new CancellationToken());

            //Assert
            _actorService.Verify(service => service.CreateAsync(It.IsAny<ActorModel>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task DeleteActor_Success()
        {
            //Arrange
            var actorId = 1;

            //Act
            await _actorController.DeleteAsync(actorId, new CancellationToken());

            //Assert
            _actorService.Verify(service => service.DeleteAsync(actorId, new CancellationToken()), Times.Once());
        }
    }
}
