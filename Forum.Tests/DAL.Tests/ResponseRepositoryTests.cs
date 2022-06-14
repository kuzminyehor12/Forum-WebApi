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
    public class ResponseRepositoryTests
    {
        private readonly Mock<ForumDataContext> _mockContext = new Mock<ForumDataContext>();
        private Mock<DbSet<Response>> _mockDbSet = new Mock<DbSet<Response>>();

        [TestMethod]
        public async Task AddAsync_WithMock_ShouldAddResponse()
        {
            _mockContext.Setup(r => r.Responses).Returns(_mockDbSet.Object);
            IResponseRepository repo = new ResponseRepository(_mockContext.Object);
            Response response = new Response();

            await repo.AddAsync(response);

            _mockContext.Verify(x => x.Responses.AddAsync(response, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WithMock_ShouldRemoveResponse()
        {
            _mockContext.Setup(r => r.Responses).Returns(_mockDbSet.Object);
            IResponseRepository repo = new ResponseRepository(_mockContext.Object);
            Response response = new Response();

            repo.Delete(response);

            _mockContext.Verify(x => x.Responses.Remove(response), Times.Once);
        }

        [TestMethod]
        public async Task DeleteById_WithMock_ShouldRemoveResponse()
        {
            _mockContext.Setup(r => r.Responses).Returns(_mockDbSet.Object);
            IResponseRepository repo = new ResponseRepository(_mockContext.Object);
            int id = 1;

            await repo.DeleteByIdAsync(id);

            _mockContext.Verify(x => x.Responses.Find(id));
            _mockContext.Verify(x => x.Responses.Remove(It.IsAny<Response>()), Times.Once);
        }

        [TestMethod]
        public void Update_WithMock_ShouldUpdateResponse()
        {
            _mockContext.Setup(r => r.Responses).Returns(_mockDbSet.Object);
            IResponseRepository repo = new ResponseRepository(_mockContext.Object);
            Response response = new Response();

            repo.Update(response);

            _mockContext.Verify(x => x.Responses.Update(response), Times.Once);
        }
    }
}
