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
    public class LikerTopicRepositoryTests
    {
        private readonly Mock<ForumDataContext> _mockContext = new Mock<ForumDataContext>();
        private Mock<DbSet<LikerTopic>> _mockDbSet = new Mock<DbSet<LikerTopic>>();

        [TestMethod]
        public async Task AddAsync_WithMock_AddTopicTag()
        {
            _mockContext.Setup(lt => lt.LikerTopics).Returns(_mockDbSet.Object);
            ILikerTopicRepository repo = new LikerTopicRepository(_mockContext.Object);
            LikerTopic likerTopic = new LikerTopic();

            await repo.AddAsync(likerTopic);

            _mockContext.Verify(x => x.LikerTopics.AddAsync(likerTopic, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WithMock_ShouldRemoveTopicTag()
        {
            _mockContext.Setup(lt => lt.LikerTopics).Returns(_mockDbSet.Object);
            ILikerTopicRepository repo = new LikerTopicRepository(_mockContext.Object);
            LikerTopic likerTopic = new LikerTopic();
            
            repo.Delete(likerTopic);

            _mockContext.Verify(x => x.LikerTopics.Remove(likerTopic), Times.Once);
        }
    }
}
