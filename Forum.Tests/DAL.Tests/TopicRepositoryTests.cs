using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Forum.Tests.DAL.Tests
{
    [TestClass]
    public class TopicRepositoryTests
    {
        private readonly Mock<ForumDataContext> _mockContext = new Mock<ForumDataContext>();
        private Mock<DbSet<Topic>> _mockDbSet = new Mock<DbSet<Topic>>();

        [TestMethod]
        public async Task AddAsync_WithMock_ShouldAddTopic()
        {
            _mockContext.Setup(t => t.Topics).Returns(_mockDbSet.Object);
            ITopicRepository repo = new TopicRepository(_mockContext.Object);
            Topic topic = new Topic();

            await repo.AddAsync(topic);

            _mockContext.Verify(x => x.Topics.AddAsync(topic, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WithMock_ShouldRemoveTopic()
        {
            _mockContext.Setup(t => t.Topics).Returns(_mockDbSet.Object);
            ITopicRepository repo = new TopicRepository(_mockContext.Object);
            Topic topic = new Topic();

            repo.Delete(topic);

            _mockContext.Verify(x => x.Topics.Remove(topic), Times.Once);
        }

        [TestMethod]
        public async Task DeleteById_WithMock_ShouldRemoveTopic()
        {
            _mockContext.Setup(t => t.Topics).Returns(_mockDbSet.Object);
            ITopicRepository repo = new TopicRepository(_mockContext.Object);
            int id = 1;

            await repo.DeleteByIdAsync(id);

            _mockContext.Verify(x => x.Topics.Find(id));
            _mockContext.Verify(x => x.Topics.Remove(It.IsAny<Topic>()), Times.Once);
        }

        [TestMethod]
        public void Update_WithMock_ShouldUpdateResponse()
        {
            _mockContext.Setup(t => t.Topics).Returns(_mockDbSet.Object);
            ITopicRepository repo = new TopicRepository(_mockContext.Object);
            Topic topic = new Topic();

            repo.Update(topic);

            _mockContext.Verify(x => x.Topics.Update(topic), Times.Once);
        }
    }
}
