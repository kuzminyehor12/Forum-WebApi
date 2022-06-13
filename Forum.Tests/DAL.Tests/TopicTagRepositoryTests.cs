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
    public class TopicTagRepositoryTests
    {
        private readonly Mock<ForumDataContext> _mockContext = new Mock<ForumDataContext>();
        private Mock<DbSet<TopicTag>> _mockDbSet = new Mock<DbSet<TopicTag>>();

        [TestMethod]
        public async Task AddAsync_WithMock_AddTopicTag()
        {
            _mockContext.Setup(tt => tt.TopicTags).Returns(_mockDbSet.Object);
            ITopicTagRepository repo = new TopicTagRepository(_mockContext.Object);
            TopicTag topicTag = new TopicTag();

            await repo.AddAsync(topicTag);

            _mockContext.Verify(x => x.TopicTags.AddAsync(topicTag, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WithMock_ShouldRemoveTopicTag()
        {
            _mockContext.Setup(tt => tt.TopicTags).Returns(_mockDbSet.Object);
            ITopicTagRepository repo = new TopicTagRepository(_mockContext.Object);
            TopicTag topicTag = new TopicTag();

            repo.Delete(topicTag);

            _mockContext.Verify(x => x.TopicTags.Remove(topicTag), Times.Once);
        }
    }
}
