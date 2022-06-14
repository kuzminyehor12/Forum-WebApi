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
        public void Update_WithMock_ShouldUpdateUser()
        {
            _mockContext.Setup(u => u.RegisteredUsers).Returns(_mockDbSet.Object);
            IUserRepository repo = new UserRepository(_mockContext.Object);
            User user = new User();

            repo.Update(user);

            _mockContext.Verify(x => x.RegisteredUsers.Update(user), Times.Once);

        }
    }
}
