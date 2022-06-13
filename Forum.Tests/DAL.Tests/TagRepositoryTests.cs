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
        public async Task GetAll_WithMock_ShouldGetTags()
        {
            _mockContext.Setup(t => t.Tags).Returns(_mockDbSet.Object);
            _mockDbSet.Setup(s => s.ToListAsync(It.IsAny<CancellationToken>())).Returns(It.IsAny<Task<List<Tag>>>());
            ITagRepository repo = new TagRepository(_mockContext.Object);

            var entities = await repo.GetAllAsync();

            Assert.AreEqual(entities, typeof(IEnumerable<Tag>));
            _mockContext.Verify(x => x.Tags.ToListAsync(It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public async Task GetAllWithDetails_WithMock_ShouldGetTagsWithDetails()
        {
            _mockContext.Setup(t => t.Tags).Returns(_mockDbSet.Object);
            ITagRepository repo = new TagRepository(_mockContext.Object);

            var entities = await repo.GetAllAsync();

            Assert.AreEqual(entities, typeof(IEnumerable<Tag>));
            _mockContext.Verify(x => x.Tags
                .Include(t => t.TopicTags)
                .ThenInclude(tt => tt.Topic)
                .ToListAsync(It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public async Task GetById_WithMock_ShouldGetTag()
        {
            _mockContext.Setup(t => t.Tags).Returns(_mockDbSet.Object);
            ITagRepository repo = new TagRepository(_mockContext.Object);
            int id = 1;

            var entity = await repo.GetByIdAsync(id);

            Assert.AreEqual(entity, typeof(Tag));
            _mockContext.Verify(x => x.Tags.FindAsync(id));
        }

        [TestMethod]
        public async Task GetByIdWithDetails_WithMock_ShouldGetTagWithDetails()
        {
            _mockContext.Setup(t => t.Tags).Returns(_mockDbSet.Object);
            ITagRepository repo = new TagRepository(_mockContext.Object);
            int id = 1;

            var entity = await repo.GetByIdAsync(id);

            Assert.AreEqual(entity, typeof(Tag));
            _mockContext.Verify(x => x.Tags
                .Include(t => t.TopicTags)
                .ThenInclude(tt => tt.Topic)
                .FirstAsync(c => c.Id == id, It.IsAny<CancellationToken>()));
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
