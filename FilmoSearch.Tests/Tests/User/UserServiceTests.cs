using AutoMapper;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using FilmoSearch.Bll.Services;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Interfaces;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FilmoSearch.Tests.Tests.User
{
    public class UserServiceTests
    {
        private readonly Mock<IGenericRepository<UserEntity>> _userRepository;
        private readonly IGenericService<UserModel> _userService;
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            _userRepository = new Mock<IGenericRepository<UserEntity>>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserEntity, UserModel>();
                cfg.CreateMap<UserModel, UserEntity>();
            });
            _mapper = mapperConfig.CreateMapper();
            _userService = new GenericService<UserModel, UserEntity>(_userRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetUserExisted_Success()
        {
            //Arrange
            var userId = 1;
            var userEntity = TestData.GetUserEntities().First();
            var userModel = _mapper.Map<UserModel>(userEntity);
            _userRepository.Setup(repo => repo.GetByIdAsync(userId, new CancellationToken())).Returns(Task.FromResult(TestData.GetUserEntities().First()));

            //Act
            var result = await _userService.GetByIdAsync(userId, new CancellationToken());

            //Assert
            Assert.Equivalent(userModel, result);
        }

        [Fact]
        public async Task GetUserNonExisted_Fails()
        {
            //Arrange
            var userId = 4;
            _userRepository.Setup(repo => repo.GetByIdAsync(userId, new CancellationToken())).Returns(Task.FromResult((UserEntity)null));

            //Act
            var result = await _userService.GetByIdAsync(userId, new CancellationToken());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllUsers_Success()
        {
            //Arrange
            var actorModels = _mapper.Map<IEnumerable<UserModel>>(TestData.GetUserEntities());
            _userRepository.Setup(repo => repo.GetAllAsync(new CancellationToken())).Returns(Task.FromResult(TestData.GetUserEntities().AsEnumerable()));

            //Act
            var result = await _userService.GetAllAsync(new CancellationToken());

            //Assert
            Assert.Equivalent(actorModels, result);
        }

        [Fact]
        public async Task UpdateUserExisted_Success()
        {
            //Arrange
            var userEntity = TestData.GetUserEntities().First();
            var userModel = _mapper.Map<UserModel>(userEntity);

            //Act
            var result = await _userService.UpdateAsync(userModel, new CancellationToken());

            //Assert
            _userRepository.Verify(repo => repo.UpdateAsync(It.IsAny<UserEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task UpdateUserNonExisted_Fails()
        {
            //Arrange
            var userModel = new UserModel
            {
                Id = 4,
                Name = "UpdateUserTestName",
                Lastname = "UpdateUserTestLastname",
                Password = "UpdateUserTestPassword",
                Email = "UpdateUserTest@gmail.com",
                RegistrationDate = new DateOnly(2021, 4, 4)
            };
            _userRepository.Setup(repo => repo.UpdateAsync(It.IsAny<UserEntity>(), new CancellationToken())).Throws(new DbUpdateConcurrencyException());

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _userService.UpdateAsync(userModel, new CancellationToken()));
        }

        [Fact]
        public async Task CreateUserNonExisted_Success()
        {
            //Arrange
            var userModel = new UserModel
            {
                Id = 4,
                Name = "CreateUserTestName",
                Lastname = "CreateUserTestLastname",
                Password = "CreateUserTestPassword",
                Email = "CreateUserTest@gmail.com",
                RegistrationDate = new DateOnly(2021, 4, 4)
            };

            //Act
            var result = await _userService.CreateAsync(userModel, new CancellationToken());

            //Assert
            _userRepository.Verify(repo => repo.CreateAsync(It.IsAny<UserEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task CreateUserExisted_Fails()
        {
            //Arrange
            var userEntity = TestData.GetUserEntities().First();
            var userModel = _mapper.Map<UserModel>(userEntity);
            _userRepository.Setup(repo => repo.CreateAsync(It.IsAny<UserEntity>(), new CancellationToken())).Throws(new ArgumentException());

            //Act and assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _userService.CreateAsync(userModel, new CancellationToken()));
        }

        [Fact]
        public async Task DeleteUserExisted_Success()
        {
            //Arrange
            var userId = 1;

            //Act
            await _userService.DeleteAsync(userId, new CancellationToken());

            //Assert
            _userRepository.Verify(repo => repo.GetByIdAsync(userId, new CancellationToken()), Times.Once());
            _userRepository.Verify(repo => repo.DeleteAsync(It.IsAny<UserEntity>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task DeleteUserNonExisted_Fails()
        {
            //Arrange
            var userId = 4;
            _userRepository.Setup(repo => repo.GetByIdAsync(userId, new CancellationToken())).Returns(Task.FromResult((UserEntity)null));

            //Act
            await _userService.DeleteAsync(userId, new CancellationToken());

            //Assert
            _userRepository.Verify(repo => repo.DeleteAsync(null, new CancellationToken()), Times.Once());
        }
    }
}
