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
