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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Forum.Tests.BLL.Tests
{
    [TestClass]
    public class ResponseServiceTests
    {
        private readonly Mock<IResponseRepository> _mockRepo = new Mock<IResponseRepository>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        [TestMethod]
        public async Task AddAsync_WithMock_ShouldAddResponseModel()
        {
            _mockUnitOfWork.Setup(m => m.ResponseRepository).Returns(_mockRepo.Object);
            ResponseService service = new ResponseService(_mockUnitOfWork.Object, _mockMapper.Object, new ResponseValidator());
            ResponseModel model = new ResponseModel
            {
                Id = 1,
                Text = "Text",
                PublicationDate = DateTime.Now
            };

            await service.AddAsync(model);

            _mockUnitOfWork.Verify(x => x.ResponseRepository.AddAsync(It.IsAny<Response>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task AddAsync_WithIncorrecrModel_ShouldThrowForumException()
        {
            _mockUnitOfWork.Setup(m => m.ResponseRepository).Returns(_mockRepo.Object);
            ResponseService service = new ResponseService(_mockUnitOfWork.Object, _mockMapper.Object, new ResponseValidator());
            ResponseModel model = new ResponseModel();

            await Assert.ThrowsExceptionAsync<ForumException>(() => service.AddAsync(model));
        }

        [TestMethod]
        public async Task DeleteAsync_WithMock_ShouldDeleteResponse()
        {
            _mockUnitOfWork.Setup(m => m.ResponseRepository).Returns(_mockRepo.Object);
            ResponseService service = new ResponseService(_mockUnitOfWork.Object, _mockMapper.Object, new ResponseValidator());
            int id = 1;

            await service.DeleteAsync(id);

            _mockUnitOfWork.Verify(x => x.ResponseRepository.DeleteByIdAsync(id), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetAllAsync_WithMock_GetResponseModels()
        {
            _mockUnitOfWork.Setup(m => m.ResponseRepository).Returns(_mockRepo.Object);
            ResponseService service = new ResponseService(_mockUnitOfWork.Object, _mockMapper.Object, new ResponseValidator());

            var entities = await service.GetAllAsync();

            Assert.IsInstanceOfType(entities, typeof(IEnumerable<ResponseModel>));
            _mockUnitOfWork.Verify(x => x.ResponseRepository.GetAllWithDetailsAsync());
        }

        [TestMethod]
        public async Task GetByIdAsync_WithMock_GetResponseModel()
        {
            _mockUnitOfWork.Setup(m => m.ResponseRepository).Returns(_mockRepo.Object);
            ResponseService service = new ResponseService(_mockUnitOfWork.Object, _mockMapper.Object, new ResponseValidator());
            int id = 1;

            var entity = await service.GetByIdAsync(id);

            _mockUnitOfWork.Verify(x => x.ResponseRepository.GetByIdWithDetailsAsync(id));
        }

        [TestMethod]
        public async Task SortByPublicationDate_WithMock_ShouldReturnSortedResponses()
        {
            _mockUnitOfWork.Setup(m => m.ResponseRepository).Returns(_mockRepo.Object);
            ResponseService service = new ResponseService(_mockUnitOfWork.Object, _mockMapper.Object, new ResponseValidator());

            var entities = await service.SortByPublicationDate();

            Assert.IsInstanceOfType(entities, typeof(IEnumerable<ResponseModel>));
            _mockUnitOfWork.Verify(x => x.ResponseRepository.GetAllWithDetailsAsync());
        }


        [TestMethod]
        public async Task SortByLikes_WithMock_ShouldReturnSortedResponses()
        {
            _mockUnitOfWork.Setup(m => m.ResponseRepository).Returns(_mockRepo.Object);
            ResponseService service = new ResponseService(_mockUnitOfWork.Object, _mockMapper.Object, new ResponseValidator());

            var entities = await service.SortByLikes();

            Assert.IsInstanceOfType(entities, typeof(IEnumerable<ResponseModel>));
            _mockUnitOfWork.Verify(x => x.ResponseRepository.GetAllWithDetailsAsync());
        }

        [TestMethod]
        public async Task UpdateAsync_WithIncorrecrModel_ShouldThrowForumException()
        {
            _mockUnitOfWork.Setup(m => m.ResponseRepository).Returns(_mockRepo.Object);
            ResponseService service = new ResponseService(_mockUnitOfWork.Object, _mockMapper.Object, new ResponseValidator());
            ResponseModel model = new ResponseModel();

            await Assert.ThrowsExceptionAsync<ForumException>(() => service.UpdateAsync(model));
        }


        [TestMethod]
        public async Task UpdateAsync_WithMock_ShouldUpdateResponse()
        {
            _mockUnitOfWork.Setup(m => m.ResponseRepository).Returns(_mockRepo.Object);
            ResponseService service = new ResponseService(_mockUnitOfWork.Object, _mockMapper.Object, new ResponseValidator());
            ResponseModel model = new ResponseModel
            {
                Id = 1,
                Text = "Text",
                PublicationDate = DateTime.Now
            };

            await service.UpdateAsync(model);

            _mockUnitOfWork.Verify(x => x.ResponseRepository.Update(It.IsAny<Response>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
