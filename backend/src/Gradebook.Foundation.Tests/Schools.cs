using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Logic.Queries;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Gradebook.Foundation.Tests
{
    [Category("Unit")]
    public class Schools
    {
        private readonly Mock<IFoundationCommandsRepository> foundationCommandsRepository = new();
        private readonly Mock<IFoundationQueriesRepository> foundationQueriesRepository = new();
        private readonly Mock<IIdentityLogic> identityLogic = new Mock<IIdentityLogic>();
        readonly ServiceCollection serviceCollection = new();
        private IFoundationCommands? foundationCommands;
        private IFoundationQueries? foundationQueries;
        [SetUp]
        public void Setup()
        {
            serviceCollection.AddScoped(_ => identityLogic.Object);
            serviceCollection.AddScoped(_ => foundationQueries!);
            foundationCommands = new FoundationCommands(foundationCommandsRepository.Object, serviceCollection.BuildServiceProvider());
            foundationQueries = new FoundationQueries(foundationQueriesRepository.Object, serviceCollection.BuildServiceProvider());
        }
        [Test]
        public async Task ShouldNotShowStudentsInSchool_SchoolNotMatchedToStudent()
        {
            identityLogic
                .Setup(e => e.CurrentUserId())
                .ReturnsAsync(new ResponseWithStatus<string, bool>("fakeUserGuid", true));
            foundationQueriesRepository
                .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(default(Guid));
            var resp = await foundationQueries!.GetAllAccessibleStudents(Guid.NewGuid());
            Assert.That(resp.Status, Is.False);
            Assert.That(resp.StatusCode, Is.EqualTo(404));
        }
        [Test]
        public async Task ShouldNotShowTeachersInSchool_SchoolNotMatchedToStudent()
        {
            identityLogic
                .Setup(e => e.CurrentUserId())
                .ReturnsAsync(new ResponseWithStatus<string, bool>("fakeUserGuid", true));
            foundationQueriesRepository
                .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(default(Guid));
            var resp = await foundationQueries!.GetAllAccessibleTeachers(Guid.NewGuid());
            Assert.That(resp.Status, Is.False);
            Assert.That(resp.StatusCode, Is.EqualTo(404));
        }
    }
}
