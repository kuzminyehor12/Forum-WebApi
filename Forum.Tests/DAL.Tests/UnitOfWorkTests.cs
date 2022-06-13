using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Forum.Tests.DAL.Tests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        private readonly Mock<ForumDataContext> _mockContext = new Mock<ForumDataContext>();

        [TestMethod]
        public async Task SaveChanges_WithMock_SaveChanges()
        {
            IUnitOfWork uow = new UnitOfWork(_mockContext.Object);

            await uow.SaveAsync();

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
