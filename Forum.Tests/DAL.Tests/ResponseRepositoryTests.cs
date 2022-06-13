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
        public async Task GetAll_WithMock_ShouldGetResponses()
        {
            _mockContext.Setup(r => r.Responses).Returns(_mockDbSet.Object);
            IResponseRepository repo = new ResponseRepository(_mockContext.Object);

            var entities = await repo.GetAllAsync();

            Assert.AreEqual(entities, typeof(IEnumerable<Response>));
            _mockContext.Verify(x => x.Responses.ToListAsync(It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public async Task GetAllWithDetails_WithMock_ShouldGetResponsesWithDetails()
        {
            _mockContext.Setup(r => r.Responses).Returns(_mockDbSet.Object);
            IResponseRepository repo = new ResponseRepository(_mockContext.Object);

            var entities = await repo.GetAllAsync();

            Assert.AreEqual(entities, typeof(IEnumerable<Response>));
            _mockContext.Verify(x => x.Responses.Include(r => r.Author)
                .Include(r => r.Topic)
                .Include(r => r.Comments)
                .Include(r => r.LikedBy)
                .ThenInclude(lr => lr.Liker)
                .ToListAsync(It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public async Task GetById_WithMock_ShouldGetResponse()
        {
            _mockContext.Setup(r => r.Responses).Returns(_mockDbSet.Object);
            IResponseRepository repo = new ResponseRepository(_mockContext.Object);
            int id = 1;

            var entity = await repo.GetByIdAsync(id);

            Assert.AreEqual(entity, typeof(Response));
            _mockContext.Verify(x => x.Responses.FindAsync(id));
        }

        [TestMethod]
        public async Task GetByIdWithDetails_WithMock_ShouldGetResponseWithDetails()
        {
            _mockContext.Setup(r => r.Responses).Returns(_mockDbSet.Object);
            IResponseRepository repo = new ResponseRepository(_mockContext.Object);
            int id = 1;

            var entity = await repo.GetByIdAsync(id);

            Assert.AreEqual(entity, typeof(Response));
            _mockContext.Verify(x => x.Responses.Include(r => r.Author)
                .Include(r => r.Topic)
                .Include(r => r.Comments)
                .Include(r => r.LikedBy)
                .ThenInclude(lr => lr.Liker)
                .FirstAsync(c => c.Id == id, It.IsAny<CancellationToken>()));
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
