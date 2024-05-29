using AutoMapper;
using FilmoSearch.Api.Controllers;
using FilmoSearch.Api.ViewModels.User;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FilmoSearch.Tests.Tests.User
{
    public class UserControllerTest
    {
        private readonly Mock<IGenericService<UserModel>> _userService;
        private readonly GenericController<UserViewModel, AddUserViewModel, UserModel> _userController;
        private readonly IMapper _mapper;
        public UserControllerTest()
        {
            _userService = new Mock<IGenericService<UserModel>>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserViewModel, UserModel>();
                cfg.CreateMap<UserModel, UserViewModel>();

                cfg.CreateMap<UserModel, UserEntity>();
                cfg.CreateMap<UserEntity, UserModel>();

                cfg.CreateMap<AddUserViewModel, UserModel>();
                cfg.CreateMap<UserModel, AddUserViewModel>();
            });
            _mapper = mapperConfig.CreateMapper();
            _userController = new GenericController<UserViewModel, AddUserViewModel, UserModel>(_userService.Object, _mapper);
        }

        [Fact]
        public async Task GetUserExisted_Success()
        {
            //Arrange
            var userId = 1;
            var userEntity = TestData.GetUserEntities().First();
            var userModel = _mapper.Map<UserModel>(userEntity);
            var userViewModel = _mapper.Map<UserViewModel>(userModel);
            _userService.Setup(service => service.GetByIdAsync(userId, new CancellationToken())).Returns(Task.FromResult(userModel));

            //Act
            var result = await _userController.GetByIdAsync(userId, new CancellationToken());

            //Assert
            Assert.Equivalent(userModel, result);
        }

        [Fact]
        public async Task GetUserNonExisted_Fails()
        {
            //Arrange
            var userId = 4;
            _userService.Setup(service => service.GetByIdAsync(userId, new CancellationToken())).Returns(Task.FromResult((UserModel)null));

            //Act
            var result = await _userController.GetByIdAsync(userId, new CancellationToken());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllUsers_Success()
        {
            //Arrange
            var userEntities = TestData.GetUserEntities();
            var userModels = _mapper.Map<IEnumerable<UserModel>>(TestData.GetUserEntities());
            var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(userModels);
            _userService.Setup(service => service.GetAllAsync(new CancellationToken())).Returns(Task.FromResult(userModels));

            //Act
            var result = await _userController.GetAllAsync(new CancellationToken());

            //Assert
            Assert.Equivalent(userModels, result);
        }

        [Fact]
        public async Task UpdateUserExisted_Success()
        {
            //Arrange
            var userEntity = TestData.GetUserEntities().First();
            var userModel = _mapper.Map<UserModel>(userEntity);
            var userViewModel = _mapper.Map<UserViewModel>(userModel);
            //Act
            var result = await _userController.UpdateAsync(userViewModel, new CancellationToken());

            //Assert
            _userService.Verify(service => service.UpdateAsync(It.IsAny<UserModel>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task UpdateUserNonExisted_Fails()
        {
            //Arrange
            var userViewModel = new UserViewModel
            {
                Id = 4,
                Name = "UpgradeUserTestName",
                Lastname = "UpgradeUserTestLastname",
                Email = "UpgradeUserTest@gmail.com",
                Password = "UpgradeUserTestPassword",
                RegistrationDate = new DateOnly(2024, 1, 1)
            };
            _userService.Setup(service => service.UpdateAsync(It.IsAny<UserModel>(), new CancellationToken())).Throws(new DbUpdateConcurrencyException());

            //Act and assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _userController.UpdateAsync(userViewModel, new CancellationToken()));
        }

        [Fact]
        public async Task CreateUser_Success()
        {
            //Arrange
            var addUserViewModel = new AddUserViewModel
            {
                Name = "UpgradeUserTestName",
                Lastname = "UpgradeUserTestLastname",
                Email = "UpgradeUserTest@gmail.com",
                Password = "UpgradeUserTestPassword"
            };

            //Act
            var result = await _userController.CreateAsync(addUserViewModel, new CancellationToken());

            //Assert
            _userService.Verify(service => service.CreateAsync(It.IsAny<UserModel>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async Task DeleteUser_Success()
        {
            //Arrange
            var userId = 1;

            //Act
            await _userController.DeleteAsync(userId, new CancellationToken());

            //Assert
            _userService.Verify(service => service.DeleteAsync(userId, new CancellationToken()), Times.Once());
        }
    }
}
