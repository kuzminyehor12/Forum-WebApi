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
    public class CommentServiceTests
    {
        private readonly Mock<ICommentRepository> _mockRepo = new Mock<ICommentRepository>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        [TestMethod]
        public async Task AddAsync_WithMock_ShouldAddCommentModel()
        {
            _mockUnitOfWork.Setup(m => m.CommentRepository).Returns(_mockRepo.Object);
            CommentService service = new CommentService(_mockUnitOfWork.Object, _mockMapper.Object, new CommentValidator());
            CommentModel model = new CommentModel
            {
                Id = 1,
                Text = "Text",
                PublicationDate = DateTime.Now
            };

            await service.AddAsync(model);

            _mockUnitOfWork.Verify(x => x.CommentRepository.AddAsync(It.IsAny<Comment>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task AddAsync_WithIncorrecrModel_ShouldThrowForumException()
        {
            _mockUnitOfWork.Setup(m => m.CommentRepository).Returns(_mockRepo.Object);
            CommentService service = new CommentService(_mockUnitOfWork.Object, _mockMapper.Object, new CommentValidator());
            CommentModel model = new CommentModel();

            await Assert.ThrowsExceptionAsync<ForumException>(() => service.AddAsync(model));
        }

        [TestMethod]
        public async Task DeleteAsync_WithMock_ShouldDeleteComment()
        {
            _mockUnitOfWork.Setup(m => m.CommentRepository).Returns(_mockRepo.Object);
            CommentService service = new CommentService(_mockUnitOfWork.Object, _mockMapper.Object, new CommentValidator());
            int id = 1;

            await service.DeleteAsync(id);

            _mockUnitOfWork.Verify(x => x.CommentRepository.DeleteByIdAsync(id), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetAllAsync_WithMock_GetCommentModels()
        {
            _mockUnitOfWork.Setup(m => m.CommentRepository).Returns(_mockRepo.Object);
            CommentService service = new CommentService(_mockUnitOfWork.Object, _mockMapper.Object, new CommentValidator());

            var entities = await service.GetAllAsync();

            Assert.IsInstanceOfType(entities, typeof(IEnumerable<CommentModel>));
            _mockUnitOfWork.Verify(x => x.CommentRepository.GetAllWithDetailsAsync());
        }

        [TestMethod]
        public async Task GetByIdAsync_WithMock_GetCommentModel()
        {
            _mockUnitOfWork.Setup(m => m.CommentRepository).Returns(_mockRepo.Object);
            CommentService service = new CommentService(_mockUnitOfWork.Object, _mockMapper.Object, new CommentValidator());
            int id = 1;

            var entity = await service.GetByIdAsync(id);

            _mockUnitOfWork.Verify(x => x.CommentRepository.GetByIdWithDetailsAsync(id));
        }

        [TestMethod]
        public async Task UpdateAsync_WithIncorrecrModel_ShouldThrowForumException()
        {
            _mockUnitOfWork.Setup(m => m.CommentRepository).Returns(_mockRepo.Object);
            CommentService service = new CommentService(_mockUnitOfWork.Object, _mockMapper.Object, new CommentValidator());
            CommentModel model = new CommentModel();

            await Assert.ThrowsExceptionAsync<ForumException>(() => service.UpdateAsync(model));
        }


        [TestMethod]
        public async Task UpdateAsync_WithMock_ShouldUpdateComment()
        {
            _mockUnitOfWork.Setup(m => m.CommentRepository).Returns(_mockRepo.Object);
            CommentService service = new CommentService(_mockUnitOfWork.Object, _mockMapper.Object, new CommentValidator());
            CommentModel model = new CommentModel
            {
                Id = 1,
                Text = "Text",
                PublicationDate = DateTime.Now
            };

            await service.UpdateAsync(model);

            _mockUnitOfWork.Verify(x => x.CommentRepository.Update(It.IsAny<Comment>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
