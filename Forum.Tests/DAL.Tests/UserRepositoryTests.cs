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
    public class UserRepositoryTests
    {
        private readonly Mock<ForumDataContext> _mockContext = new Mock<ForumDataContext>();
        private Mock<DbSet<User>> _mockDbSet = new Mock<DbSet<User>>();

        [TestMethod]
        public async Task AddAsync_WithMock_ShouldAddUser()
        {
            _mockContext.Setup(u => u.RegisteredUsers).Returns(_mockDbSet.Object);
            IUserRepository repo = new UserRepository(_mockContext.Object);
            User user = new User();

            await repo.AddAsync(user);

            _mockContext.Verify(x => x.RegisteredUsers.AddAsync(user, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WithMock_ShouldRemoveUser()
        {
            _mockContext.Setup(u => u.RegisteredUsers).Returns(_mockDbSet.Object);
            IUserRepository repo = new UserRepository(_mockContext.Object);
            User user = new User();

            repo.Delete(user);

            _mockContext.Verify(x => x.RegisteredUsers.Remove(user), Times.Once);
        }

        [TestMethod]
        public async Task DeleteById_WithMock_ShouldRemoveUser()
        {
            _mockContext.Setup(u => u.RegisteredUsers).Returns(_mockDbSet.Object);
            IUserRepository repo = new UserRepository(_mockContext.Object);
            int id = 1;

            await repo.DeleteByIdAsync(id);

            _mockContext.Verify(x => x.RegisteredUsers.Find(id));
            _mockContext.Verify(x => x.RegisteredUsers.Remove(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAll_WithMock_ShouldGetUsers()
        {
            _mockContext.Setup(u => u.RegisteredUsers).Returns(_mockDbSet.Object);
            IUserRepository repo = new UserRepository(_mockContext.Object);

            var entities = await repo.GetAllAsync();

            Assert.IsInstanceOfType(entities, typeof(IEnumerable<User>));
            _mockContext.Verify(x => x.RegisteredUsers.ToListAsync(It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public async Task GetAllWithDetails_WithMock_ShouldGetUsersWithDetails()
        {
            _mockContext.Setup(u => u.RegisteredUsers).Returns(_mockDbSet.Object);
            IUserRepository repo = new UserRepository(_mockContext.Object);

            var entities = await repo.GetAllWithDetailsAsync();

            Assert.IsInstanceOfType(entities, typeof(IEnumerable<User>));
            _mockContext.Verify(x => x.RegisteredUsers.Include(u => u.CreatedTopics)
                   .Include(u => u.CreatedResponses)
                   .Include(u => u.CreatedComments)
                   .Include(u => u.LikedTopics)
                   .ThenInclude(lt => lt.Topic)
                   .Include(u => u.LikedResponses)
                   .ThenInclude(lr => lr.Response)
                   .Include(u => u.UserCredentials)
                   .ToListAsync(It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public async Task GetById_WithMock_ShouldGetUser()
        {
            _mockContext.Setup(u => u.RegisteredUsers).Returns(_mockDbSet.Object);
            IUserRepository repo = new UserRepository(_mockContext.Object);
            int id = 1;

            var entity = await repo.GetByIdAsync(id);

            Assert.IsInstanceOfType(entity, typeof(User));
            _mockContext.Verify(x => x.RegisteredUsers.FindAsync(id));
        }

        [TestMethod]
        public async Task GetByIdWithDetails_WithMock_ShouldGetUserWithDetails()
        {
            _mockContext.Setup(u => u.RegisteredUsers).Returns(_mockDbSet.Object);
            IUserRepository repo = new UserRepository(_mockContext.Object);
            int id = 1;

            var entity = await repo.GetByIdWithDetailsAsync(id);

            Assert.IsInstanceOfType(entity, typeof(User));
            _mockContext.Verify(x => x.RegisteredUsers.Include(u => u.CreatedTopics)
                   .Include(u => u.CreatedResponses)
                   .Include(u => u.CreatedComments)
                   .Include(u => u.LikedTopics)
                   .ThenInclude(lt => lt.Topic)
                   .Include(u => u.LikedResponses)
                   .ThenInclude(lr => lr.Response)
                   .Include(u => u.UserCredentials)
                   .FirstAsync(c => c.Id == id, It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public void Update_WithMock_ShouldUpdateUser()
        {
            _mockContext.Setup(u => u.RegisteredUsers).Returns(_mockDbSet.Object);
            IUserRepository repo = new UserRepository(_mockContext.Object);
            User user = new User();

            repo.Update(user);

            _mockContext.Verify(x => x.RegisteredUsers.Update(user), Times.Once);
            _mockContext.Verify(x => x.RegisteredUsers.Update(user), Times.Once);
        }
    }
}
