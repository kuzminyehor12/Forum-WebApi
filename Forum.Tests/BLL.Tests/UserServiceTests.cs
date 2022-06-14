using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models;
using BLL.Services;
using BLL.Validation;
using DAL.Entities;
using DAL.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Forum.Tests.BLL.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepo = new Mock<IUserRepository>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        [TestMethod]
        public async Task AddAsync_WithMock_ShouldAddUserModel()
        {
            _mockUnitOfWork.Setup(m => m.UserRepository).Returns(_mockRepo.Object);
            UserService service = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, new UserValidator());
            UserModel model = new UserModel { 
                Id = 1,
                Nickname = "Nick",
                Role = Roles.AuthorizedUser,
                BirthDate = new DateTime(2003, 03, 12)
            };

            await service.AddAsync(model);

            _mockUnitOfWork.Verify(x => x.UserRepository.AddAsync(It.IsAny<User>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task AddAsync_WithIncorrecrModel_ShouldThrowForumException()
        {
            _mockUnitOfWork.Setup(m => m.UserRepository).Returns(_mockRepo.Object);
            UserService service = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, new UserValidator());
            UserModel model = new UserModel();

            await Assert.ThrowsExceptionAsync<ForumException>(() => service.AddAsync(model));
        }

        [TestMethod]
        public async Task DeleteAsync_WithMock_ShouldDeleteUser()
        {
            _mockUnitOfWork.Setup(m => m.UserRepository).Returns(_mockRepo.Object);
            UserService service = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, new UserValidator());
            int id = 1;

            await service.DeleteAsync(id);

            _mockUnitOfWork.Verify(x => x.UserRepository.DeleteByIdAsync(id), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetAllAsync_WithMock_GetUserModels()
        {
            _mockUnitOfWork.Setup(m => m.UserRepository).Returns(_mockRepo.Object);
            UserService service = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, new UserValidator());

            var entities = await service.GetAllAsync();

            Assert.IsInstanceOfType(entities, typeof(IEnumerable<UserModel>));
            _mockUnitOfWork.Verify(x => x.UserRepository.GetAllWithDetailsAsync());
        }

        [TestMethod]
        public async Task GetByIdAsync_WithMock_GetUserModel()
        {
            _mockUnitOfWork.Setup(m => m.UserRepository).Returns(_mockRepo.Object);
            UserService service = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, new UserValidator());
            int id = 1;

            var entity = await service.GetByIdAsync(id);

            _mockUnitOfWork.Verify(x => x.UserRepository.GetByIdWithDetailsAsync(id));
        }

        [TestMethod]
        public async Task LikeTopicAsync_WithMock_ShouldUpdateUser()
        {
            Mock<ILikerTopicRepository> mockRepo = new Mock<ILikerTopicRepository>();
            _mockUnitOfWork.Setup(m => m.LikerTopicRepository).Returns(mockRepo.Object);
            UserService service = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, new UserValidator());
            LikerTopicModel model = new LikerTopicModel();

            await service.LikeTopicAsync(model);

            _mockUnitOfWork.Verify(x => x.LikerTopicRepository.AddAsync(It.IsAny<LikerTopic>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task LikeResponseAsync_WithMock_ShouldAddLikerResponse()
        {
            Mock<ILikerResponseRepository> mockRepo = new Mock<ILikerResponseRepository>();
            _mockUnitOfWork.Setup(m => m.LikerResponseRepository).Returns(mockRepo.Object);
            UserService service = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, new UserValidator());
            LikerResponseModel model = new LikerResponseModel();

            await service.LikeResponseAsync(model);

            _mockUnitOfWork.Verify(x => x.LikerResponseRepository.AddAsync(It.IsAny<LikerResponse>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task RemoveLikeTopicAsync_WithMock_ShouldAddLikerTopic()
        {
            Mock<ILikerTopicRepository> mockRepo = new Mock<ILikerTopicRepository>();
            _mockUnitOfWork.Setup(m => m.LikerTopicRepository).Returns(mockRepo.Object);
            UserService service = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, new UserValidator());
            LikerTopicModel model = new LikerTopicModel();

            await service.RemoveLikeTopicAsync(model);

            _mockUnitOfWork.Verify(x => x.LikerTopicRepository.Delete(It.IsAny<LikerTopic>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task RemoveLikeResponseAsync_WithMock_ShouldUpdateUser()
        {
            Mock<ILikerResponseRepository> mockRepo = new Mock<ILikerResponseRepository>();
            _mockUnitOfWork.Setup(m => m.LikerResponseRepository).Returns(mockRepo.Object);
            UserService service = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, new UserValidator());
            LikerResponseModel model = new LikerResponseModel();

            await service.RemoveLikeResponseAsync(model);

            _mockUnitOfWork.Verify(x => x.LikerResponseRepository.Delete(It.IsAny<LikerResponse>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_WithMock_ShouldUpdateUser()
        {
            _mockUnitOfWork.Setup(m => m.UserRepository).Returns(_mockRepo.Object);
            UserService service = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, new UserValidator());
            UserModel model = new UserModel
            {
                Id = 1,
                Nickname = "Nick",
                Role = Roles.AuthorizedUser,
                BirthDate = new DateTime(2003, 03, 12)
            };

            await service.UpdateAsync(model);

            _mockUnitOfWork.Verify(x => x.UserRepository.Update(It.IsAny<User>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_WithIncorrecrModel_ShouldThrowForumException()
        {
            _mockUnitOfWork.Setup(m => m.UserRepository).Returns(_mockRepo.Object);
            UserService service = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, new UserValidator());
            UserModel model = new UserModel();

            await Assert.ThrowsExceptionAsync<ForumException>(() => service.UpdateAsync(model));
        }
    }
}
