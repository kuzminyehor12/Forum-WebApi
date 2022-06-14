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
    public class TopicServiceTests
    {
        private readonly Mock<ITopicRepository> _mockRepo = new Mock<ITopicRepository>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        [TestMethod]
        public async Task AddAsync_WithMock_ShouldAddTopicModel()
        {
            _mockUnitOfWork.Setup(m => m.TopicRepository).Returns(_mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());
            TopicModel model = new TopicModel
            {
                Id = 1,
                Title = "Title",
                Description = "Description",
                PublicationDate = DateTime.Now
            };

            await service.AddAsync(model);

            _mockUnitOfWork.Verify(x => x.TopicRepository.AddAsync(It.IsAny<Topic>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task AddAsync_WithIncorrecrModel_ShouldThrowForumException()
        {
            _mockUnitOfWork.Setup(m => m.TopicRepository).Returns(_mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());
            TopicModel model = new TopicModel();

            await Assert.ThrowsExceptionAsync<ForumException>(() => service.AddAsync(model));
        }

        [TestMethod]
        public async Task AddTag_WithMock_ShouldAddTopicTag()
        {
            Mock<ITopicTagRepository> mockRepo = new Mock<ITopicTagRepository>();
            _mockUnitOfWork.Setup(m => m.TopicTagRepository).Returns(mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());
            TopicTagModel model = new TopicTagModel();

            await service.AddTag(model);

            _mockUnitOfWork.Verify(x => x.TopicTagRepository.AddAsync(It.IsAny<TopicTag>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task RemoveTag_WithMock_ShouldRemoveTopicTag()
        {
            Mock<ITopicTagRepository> mockRepo = new Mock<ITopicTagRepository>();
            _mockUnitOfWork.Setup(m => m.TopicTagRepository).Returns(mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());
            TopicTagModel model = new TopicTagModel();

            await service.RemoveTag(model);

            _mockUnitOfWork.Verify(x => x.TopicTagRepository.Delete(It.IsAny<TopicTag>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_WithMock_ShouldDeleteTopic()
        {
            _mockUnitOfWork.Setup(m => m.TopicRepository).Returns(_mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());
            int id = 1;

            await service.DeleteAsync(id);

            _mockUnitOfWork.Verify(x => x.TopicRepository.DeleteByIdAsync(id), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetAllAsync_WithMock_GetTopicModels()
        {
            _mockUnitOfWork.Setup(m => m.TopicRepository).Returns(_mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());

            var entities = await service.GetAllAsync();

            Assert.IsInstanceOfType(entities, typeof(IEnumerable<TopicModel>));
            _mockUnitOfWork.Verify(x => x.TopicRepository.GetAllWithDetailsAsync());
        }

        [TestMethod]
        public async Task GetByIdAsync_WithMock_GetTopicModel()
        {
            _mockUnitOfWork.Setup(m => m.TopicRepository).Returns(_mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());
            int id = 1;

            var entity = await service.GetByIdAsync(id);

            _mockUnitOfWork.Verify(x => x.TopicRepository.GetByIdWithDetailsAsync(id));
        }

        [TestMethod]
        public async Task GetByFilterAsync_WithFilterModel_ShouldReturnFilteredTopics()
        {
            _mockUnitOfWork.Setup(m => m.TopicRepository).Returns(_mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());
            FilterModel model = new FilterModel();

            var entities = await service.GetByFilterAsync(model);

            Assert.IsInstanceOfType(entities, typeof(IEnumerable<TopicModel>));
            _mockUnitOfWork.Verify(x => x.TopicRepository.GetAllWithDetailsAsync());
        }


        [TestMethod]
        public async Task SortByPublicationDate_WithMock_ShouldReturnSortedTopics()
        {
            _mockUnitOfWork.Setup(m => m.TopicRepository).Returns(_mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());

            var entities = await service.SortByPublicationDate();

            Assert.IsInstanceOfType(entities, typeof(IEnumerable<TopicModel>));
            _mockUnitOfWork.Verify(x => x.TopicRepository.GetAllWithDetailsAsync());
        }


        [TestMethod]
        public async Task SortByLikes_WithMock_ShouldReturnSortedTopics()
        {
            _mockUnitOfWork.Setup(m => m.TopicRepository).Returns(_mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());

            var entities = await service.SortByLikes();

            Assert.IsInstanceOfType(entities, typeof(IEnumerable<TopicModel>));
            _mockUnitOfWork.Verify(x => x.TopicRepository.GetAllWithDetailsAsync());
        }

        [TestMethod]
        public async Task UpdateAsync_WithIncorrecrModel_ShouldThrowForumException()
        {
            _mockUnitOfWork.Setup(m => m.TopicRepository).Returns(_mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());
            TopicModel model = new TopicModel();

            await Assert.ThrowsExceptionAsync<ForumException>(() => service.UpdateAsync(model));
        }


        [TestMethod]
        public async Task UpdateAsync_WithMock_ShouldUpdateTopic()
        {
            _mockUnitOfWork.Setup(m => m.TopicRepository).Returns(_mockRepo.Object);
            TopicService service = new TopicService(_mockUnitOfWork.Object, _mockMapper.Object, new TopicValidator());
            TopicModel model = new TopicModel
            {
                Id = 1,
                Title = "Title",
                Description = "Description",
                PublicationDate = DateTime.Now
            };

            await service.UpdateAsync(model);

            _mockUnitOfWork.Verify(x => x.TopicRepository.Update(It.IsAny<Topic>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
