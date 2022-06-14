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
    public class TagRepositoryTests
    {
        private readonly Mock<ForumDataContext> _mockContext = new Mock<ForumDataContext>();
        private Mock<DbSet<Tag>> _mockDbSet = new Mock<DbSet<Tag>>();

        [TestMethod]
        public async Task AddAsync_WithMock_ShouldAddTag()
        {
            _mockContext.Setup(t => t.Tags).Returns(_mockDbSet.Object);
            ITagRepository repo = new TagRepository(_mockContext.Object);
            Tag tag = new Tag();

            await repo.AddAsync(tag);

            _mockContext.Verify(x => x.Tags.AddAsync(tag, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WithMock_ShouldRemoveTag()
        {
            _mockContext.Setup(t => t.Tags).Returns(_mockDbSet.Object);
            ITagRepository repo = new TagRepository(_mockContext.Object);
            Tag tag = new Tag();

            repo.Delete(tag);

            _mockContext.Verify(x => x.Tags.Remove(tag), Times.Once);
        }

        [TestMethod]
        public async Task DeleteById_WithMock_ShouldRemoveTag()
        {
            _mockContext.Setup(t => t.Tags).Returns(_mockDbSet.Object);
            ITagRepository repo = new TagRepository(_mockContext.Object);
            int id = 1;

            await repo.DeleteByIdAsync(id);

            _mockContext.Verify(x => x.Tags.Find(id));
            _mockContext.Verify(x => x.Tags.Remove(It.IsAny<Tag>()), Times.Once);
        }

        [TestMethod]
        public void Update_WithMock_ShouldUpdateTag()
        {
            _mockContext.Setup(t => t.Tags).Returns(_mockDbSet.Object);
            ITagRepository repo = new TagRepository(_mockContext.Object);
            Tag tag = new Tag();

            repo.Update(tag);

            _mockContext.Verify(x => x.Tags.Update(tag), Times.Once);
        }
    }
}
