using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Logic.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS0618

namespace Gradebook.Foundation.Tests
{
    public class CommonTests
    {
        #region Global time wrapper
        [Test]
        public void ShouldSetFakeTime()
        {
            var fakeTime = DateTime.UtcNow.AddHours(5);

            Time.SetFakeUtcNow(fakeTime);
            Assert.That(Time.UtcNow, Is.EqualTo(fakeTime));
            Assert.That(Time.UtcNow, Is.GreaterThan(DateTime.UtcNow));
        }
        [Test]
        public void ShouldSetCorrectTime()
        {
            Time.Reset();
            Assert.That(Time.UtcNow.Subtract(DateTime.UtcNow), Is.LessThan(TimeSpan.FromMilliseconds(10)));
        }
        [Test]
        public void ShouldSetFakeTimeThenShouldSetCorrectTime()
        {
            var fakeTime = DateTime.UtcNow.AddHours(5);
            Time.SetFakeUtcNow(fakeTime);
            Assert.That(Time.UtcNow, Is.EqualTo(fakeTime));
            Assert.That(Time.UtcNow, Is.GreaterThan(DateTime.UtcNow));
            Time.Reset();
            Assert.That(Time.UtcNow.Subtract(DateTime.UtcNow), Is.LessThan(TimeSpan.FromMilliseconds(10)));
        }
        #endregion

        #region Validatable
        internal class FakeValidationClass : Validatable<FakeValidationClass>
        {
            public string? Value { get; set; }
            protected override bool Validate(FakeValidationClass validatable)
            {
                return Value != "invalid";
            }
        }
        [Test]
        public void ShouldBeValidated()
        {
            var fakeClass = new FakeValidationClass()
            { 
                Value = "valid"
            };
            Assert.That(fakeClass.IsValid);
            fakeClass = new FakeValidationClass();
            Assert.That(fakeClass.IsValid);
            fakeClass = new FakeValidationClass()
            {
                Value = "invalid"
            };
            Assert.That(!fakeClass.IsValid);
        }

        #endregion

        #region Base repository
        private class FakeRepositoryClass : BaseRepository<DbContext>
        {
            public FakeRepositoryClass(DbContext context) : base(context)
            {
            }
        }

        [Test]
        public void ShouldPerformTransactionOnlyOnce()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
                optionsBuilder.UseMySql("server=fakeServer;", new MySqlServerVersion(new Version(8, 30, 0)));
            Mock<DbContext> mockedDbContext = new(optionsBuilder.Options);
            Mock<DatabaseFacade> mockedDatabaseFacade = new(mockedDbContext.Object);
            Mock<IDbContextTransaction> fakeTransaction = new();

            mockedDbContext.Setup(e => e.Database).Returns(mockedDatabaseFacade.Object);
            mockedDatabaseFacade.Setup(e => e.BeginTransaction()).Returns(fakeTransaction.Object);

            var fakeRepo = new FakeRepositoryClass(mockedDbContext.Object);

            fakeRepo.BeginTransaction();
            fakeRepo.BeginTransaction();
            fakeRepo.BeginTransaction();
            fakeRepo.CommitTransaction();
            fakeRepo.CommitTransaction();
            fakeRepo.BeginTransaction();
            fakeRepo.CommitTransaction();
            fakeRepo.CommitTransaction();

            fakeTransaction.Verify(e => e.Commit(), Times.Exactly(1));
        }
        [Test]
        public void ShouldNotPerformTransaction_MoreBeginsThanCommits()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseMySql("server=fakeServer;", new MySqlServerVersion(new Version(8, 30, 0)));
            Mock<DbContext> mockedDbContext = new(optionsBuilder.Options);
            Mock<DatabaseFacade> mockedDatabaseFacade = new(mockedDbContext.Object);
            Mock<IDbContextTransaction> fakeTransaction = new();

            mockedDbContext.Setup(e => e.Database).Returns(mockedDatabaseFacade.Object);
            mockedDatabaseFacade.Setup(e => e.BeginTransaction()).Returns(fakeTransaction.Object);

            var fakeRepo = new FakeRepositoryClass(mockedDbContext.Object);

            fakeRepo.BeginTransaction();
            fakeRepo.BeginTransaction();
            fakeRepo.BeginTransaction();
            fakeRepo.CommitTransaction();

            fakeTransaction.Verify(e => e.Commit(), Times.Never());
        }
        [Test]
        public void ShouldPerformTransactionOnlyOnce_MultipleBeginsOneCommit()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseMySql("server=fakeServer;", new MySqlServerVersion(new Version(8, 30, 0)));
            Mock<DbContext> mockedDbContext = new(optionsBuilder.Options);
            Mock<DatabaseFacade> mockedDatabaseFacade = new(mockedDbContext.Object);
            Mock<IDbContextTransaction> fakeTransaction = new();

            mockedDbContext.Setup(e => e.Database).Returns(mockedDatabaseFacade.Object);
            mockedDatabaseFacade.Setup(e => e.BeginTransaction()).Returns(fakeTransaction.Object);

            var fakeRepo = new FakeRepositoryClass(mockedDbContext.Object);

            fakeRepo.BeginTransaction();
            fakeRepo.CommitTransaction();
            fakeRepo.CommitTransaction();
            fakeRepo.CommitTransaction();
            fakeRepo.CommitTransaction();

            fakeTransaction.Verify(e => e.Commit(), Times.Exactly(1));
        }
        [Test]
        public void ShouldRollbackTransaction()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseMySql("server=fakeServer;", new MySqlServerVersion(new Version(8, 30, 0)));
            Mock<DbContext> mockedDbContext = new(optionsBuilder.Options);
            Mock<DatabaseFacade> mockedDatabaseFacade = new(mockedDbContext.Object);
            Mock<IDbContextTransaction> fakeTransaction = new();

            mockedDbContext.Setup(e => e.Database).Returns(mockedDatabaseFacade.Object);
            mockedDatabaseFacade.Setup(e => e.BeginTransaction()).Returns(fakeTransaction.Object);

            var fakeRepo = new FakeRepositoryClass(mockedDbContext.Object);

            fakeRepo.BeginTransaction();
            fakeRepo.RollbackTransaction();
            fakeRepo.CommitTransaction();

            fakeTransaction.Verify(e => e.Rollback(), Times.Exactly(1));
            fakeTransaction.Verify(e => e.Commit(), Times.Never());
        }
        [Test]
        public void ShouldRollbackTransaction_MultipleBegins()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseMySql("server=fakeServer;", new MySqlServerVersion(new Version(8, 30, 0)));
            Mock<DbContext> mockedDbContext = new(optionsBuilder.Options);
            Mock<DatabaseFacade> mockedDatabaseFacade = new(mockedDbContext.Object);
            Mock<IDbContextTransaction> fakeTransaction = new();

            mockedDbContext.Setup(e => e.Database).Returns(mockedDatabaseFacade.Object);
            mockedDatabaseFacade.Setup(e => e.BeginTransaction()).Returns(fakeTransaction.Object);

            var fakeRepo = new FakeRepositoryClass(mockedDbContext.Object);

            fakeRepo.BeginTransaction();
            fakeRepo.BeginTransaction();
            fakeRepo.RollbackTransaction();
            fakeRepo.CommitTransaction();

            fakeTransaction.Verify(e => e.Rollback(), Times.Exactly(1));
            fakeTransaction.Verify(e => e.Commit(), Times.Never());
        }
        [Test]
        public void ShouldRollbackTransaction_MultipleRollbacks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseMySql("server=fakeServer;", new MySqlServerVersion(new Version(8, 30, 0)));
            Mock<DbContext> mockedDbContext = new(optionsBuilder.Options);
            Mock<DatabaseFacade> mockedDatabaseFacade = new(mockedDbContext.Object);
            Mock<IDbContextTransaction> fakeTransaction = new();

            mockedDbContext.Setup(e => e.Database).Returns(mockedDatabaseFacade.Object);
            mockedDatabaseFacade.Setup(e => e.BeginTransaction()).Returns(fakeTransaction.Object);

            var fakeRepo = new FakeRepositoryClass(mockedDbContext.Object);

            fakeRepo.BeginTransaction();
            fakeRepo.BeginTransaction();
            fakeRepo.RollbackTransaction();
            fakeRepo.CommitTransaction();
            fakeRepo.RollbackTransaction();
            fakeRepo.RollbackTransaction();
            fakeRepo.RollbackTransaction();
            fakeRepo.RollbackTransaction();
            fakeRepo.CommitTransaction();
            fakeRepo.CommitTransaction();
            fakeRepo.CommitTransaction();

            fakeTransaction.Verify(e => e.Rollback(), Times.Exactly(1));
            fakeTransaction.Verify(e => e.Commit(), Times.Never());
        }
        [Test]
        public void NestedCommits()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseMySql("server=fakeServer;", new MySqlServerVersion(new Version(8, 30, 0)));
            Mock<DbContext> mockedDbContext = new(optionsBuilder.Options);
            Mock<DatabaseFacade> mockedDatabaseFacade = new(mockedDbContext.Object);
            Mock<IDbContextTransaction> fakeTransaction = new();

            mockedDbContext.Setup(e => e.Database).Returns(mockedDatabaseFacade.Object);
            mockedDatabaseFacade.Setup(e => e.BeginTransaction()).Returns(fakeTransaction.Object);

            var fakeRepo = new FakeRepositoryClass(mockedDbContext.Object);

            fakeRepo.BeginTransaction();
            fakeRepo.BeginTransaction();
            fakeRepo.BeginTransaction();
            fakeRepo.CommitTransaction();
            fakeRepo.CommitTransaction();
            fakeRepo.CommitTransaction();

            fakeTransaction.Verify(e => e.Commit(), Times.Exactly(1));
        }
        #endregion
    }
}
