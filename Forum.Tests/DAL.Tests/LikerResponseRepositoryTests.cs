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
    class LikerResponseRepositoryTests
    {
        private readonly Mock<ForumDataContext> _mockContext = new Mock<ForumDataContext>();
        private Mock<DbSet<LikerResponse>> _mockDbSet = new Mock<DbSet<LikerResponse>>();

        [TestMethod]
        public async Task AddAsync_WithMock_AddTopicTag()
        {
            _mockContext.Setup(lr => lr.LikerResponses).Returns(_mockDbSet.Object);
            ILikerResponseRepository repo = new LikerResponseRepository(_mockContext.Object);
            LikerResponse likerResponse = new LikerResponse();

            await repo.AddAsync(likerResponse);

            _mockContext.Verify(x => x.LikerResponses.AddAsync(likerResponse, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WithMock_ShouldRemoveTopicTag()
        {
            _mockContext.Setup(lr => lr.LikerResponses).Returns(_mockDbSet.Object);
            ILikerResponseRepository repo = new LikerResponseRepository(_mockContext.Object);
            LikerResponse likerResponse = new LikerResponse();

            repo.Delete(likerResponse);

            _mockContext.Verify(x => x.LikerResponses.Remove(likerResponse), Times.Once);
        }
    }
}
