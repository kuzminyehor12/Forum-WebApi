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
    public class CommentRepositoryTests
    {
        private readonly Mock<ForumDataContext> _mockContext = new Mock<ForumDataContext>();
        private Mock<DbSet<Comment>> _mockDbSet = new Mock<DbSet<Comment>>();

        [TestMethod]
        public async Task AddAsync_WithMock_ShouldAddComment()
        {
            _mockContext.Setup(c => c.Comments).Returns(_mockDbSet.Object);
            ICommentRepository repo = new CommentRepository(_mockContext.Object);
            Comment comment = new Comment();

            await repo.AddAsync(comment);

            _mockContext.Verify(x => x.Comments.AddAsync(comment, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WithMock_ShouldRemoveComment()
        {
            _mockContext.Setup(c => c.Comments).Returns(_mockDbSet.Object);
            ICommentRepository repo = new CommentRepository(_mockContext.Object);
            Comment comment = new Comment();

            repo.Delete(comment);

            _mockContext.Verify(x => x.Comments.Remove(comment), Times.Once);
        }

        [TestMethod]
        public async Task DeleteById_WithMock_ShouldRemoveComment()
        {
            _mockContext.Setup(c => c.Comments).Returns(_mockDbSet.Object);
            ICommentRepository repo = new CommentRepository(_mockContext.Object);
            int id = 1;

            await repo.DeleteByIdAsync(id);

            _mockContext.Verify(x => x.Comments.Find(id));
            _mockContext.Verify(x => x.Comments.Remove(It.IsAny<Comment>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAll_WithMock_ShouldGetComments()
        {
            _mockContext.Setup(c => c.Comments).Returns(_mockDbSet.Object);
            ICommentRepository repo = new CommentRepository(_mockContext.Object);

            var entities = await repo.GetAllAsync();

            Assert.AreEqual(entities, typeof(IEnumerable<Comment>));
            _mockContext.Verify(x => x.Comments.ToListAsync(It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public async Task GetAllWithDetails_WithMock_ShouldGetCommentsWithDetails()
        {
            _mockContext.Setup(c => c.Comments).Returns(_mockDbSet.Object);
            ICommentRepository repo = new CommentRepository(_mockContext.Object);

            var entities = await repo.GetAllAsync();

            Assert.AreEqual(entities, typeof(IEnumerable<Comment>));
            _mockContext.Verify(x => x.Comments.Include(c => c.Author)
                .Include(c => c.Response)
                .ThenInclude(r => r.Topic)
                .ToListAsync(It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public async Task GetById_WithMock_ShouldGetComment()
        {
            _mockContext.Setup(c => c.Comments).Returns(_mockDbSet.Object);
            ICommentRepository repo = new CommentRepository(_mockContext.Object);
            int id = 1;

            var entity = await repo.GetByIdAsync(id);

            Assert.AreEqual(entity, typeof(Comment));
            _mockContext.Verify(x => x.Comments.FindAsync(id));
        }

        [TestMethod]
        public async Task GetByIdWithDetails_WithMock_ShouldGetCommentWithDetails()
        {
            _mockContext.Setup(c => c.Comments).Returns(_mockDbSet.Object);
            ICommentRepository repo = new CommentRepository(_mockContext.Object);
            int id = 1;

            var entity = await repo.GetByIdAsync(id);

            Assert.AreEqual(entity, typeof(Comment));
            _mockContext.Verify(x => x.Comments.Include(c => c.Author)
                .Include(c => c.Response)
                .ThenInclude(r => r.Topic)
                .FirstAsync(c => c.Id == id, It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Update_WithMock_ShouldUpdateComment()
        {
            _mockContext.Setup(c => c.Comments).Returns(_mockDbSet.Object);
            ICommentRepository repo = new CommentRepository(_mockContext.Object);
            Comment comment = new Comment();

            repo.Update(comment);

            _mockContext.Verify(x => x.Comments.Update(comment), Times.Once);
        }
    }
}
