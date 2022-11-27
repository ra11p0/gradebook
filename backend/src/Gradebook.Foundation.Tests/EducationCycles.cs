using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Logic.Queries;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Tests
{
    internal class EducationCycles
    {
        private readonly Mock<IFoundationCommandsRepository> foundationCommandsRepository = new();
        private readonly Mock<IFoundationQueriesRepository> foundationQueriesRepository = new();
        private readonly Mock<IIdentityLogic> identityLogic = new();
        private readonly Mock<IFoundationPermissionsLogic> foundationPermissionsLogic = new();
        readonly ServiceCollection serviceCollection = new();
        private IFoundationCommands? foundationCommands;
        private IFoundationQueries? foundationQueries;
        [SetUp]
        public void Setup()
        {
            serviceCollection.AddScoped(_ => identityLogic.Object);
            serviceCollection.AddScoped(_ => foundationPermissionsLogic.Object);
            serviceCollection.AddScoped(_ => foundationQueries!);
            foundationCommands = new FoundationCommands(foundationCommandsRepository.Object, serviceCollection.BuildServiceProvider());
            foundationQueries = new FoundationQueries(foundationQueriesRepository.Object, serviceCollection.BuildServiceProvider());
            foundationCommandsRepository.Invocations.Clear();
            foundationQueriesRepository.Invocations.Clear();
            identityLogic.Invocations.Clear();
            foundationPermissionsLogic.Invocations.Clear();
        }

        [Test]
        public async Task ShouldReturnNotFound()
        {
            foundationQueriesRepository
                .Setup(e => e.GetEducationCycle(It.IsAny<Guid>()))
                .ReturnsAsync(default(EducationCycleExtendedDto));
            identityLogic
                .Setup(e => e.CurrentUserId())
                .ReturnsAsync(new Common.ResponseWithStatus<string, bool>(default, true));
            foundationPermissionsLogic
                .Setup(e => e.CanSeeEducationCycles(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var result = await foundationQueries!.GetEducationCycle(new Guid());

            Assert.That(result.Status, Is.False);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }
        [Test]
        public async Task ShouldReturnEducationCycle()
        {
            foundationQueriesRepository
                .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(Guid.NewGuid());
            foundationQueriesRepository
                .Setup(e => e.GetEducationCycle(It.IsAny<Guid>()))
                .ReturnsAsync(new EducationCycleExtendedDto() { });
            identityLogic
                .Setup(e => e.CurrentUserId())
                .ReturnsAsync(new Common.ResponseWithStatus<string, bool>(default, true));
            foundationPermissionsLogic
                .Setup(e => e.CanSeeEducationCycles(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var result = await foundationQueries!.GetEducationCycle(new Guid());

            Assert.That(result.Status, Is.True);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
        [Test]
        public async Task ShouldReturnEducationCyclesInSchool()
        {
            foundationQueriesRepository
                .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(Guid.NewGuid());
            foundationQueriesRepository
                .Setup(e => e.GetEducationCyclesInSchool(It.IsAny<Guid>(), It.IsAny<Pager>()))
                .ReturnsAsync(new PagedList<EducationCycleDto>() { });
            identityLogic
                .Setup(e => e.CurrentUserId())
                .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
            foundationPermissionsLogic
                .Setup(e => e.CanSeeEducationCycles(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var result = await foundationQueries!.GetEducationCyclesInSchool(new Guid(), 0);

            Assert.That(result.Status, Is.True);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
    }
}
