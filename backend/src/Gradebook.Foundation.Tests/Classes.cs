using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Logic.Commands.Repositories;
using Gradebook.Foundation.Logic.Queries;
using Gradebook.Foundation.Logic.Queries.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Gradebook.Foundation.Tests;

[Category("Unit")]
public class Classes
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
    public async Task ShouldNotAddStudentToClass_AlreadyInOne()
    {
        var studentGuid = Guid.NewGuid();
        var classGuid = Guid.NewGuid();
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationQueriesRepository
            .Setup(e => e.IsStudentInAnyClass(It.Is<Guid>(e => e == studentGuid)))
            .ReturnsAsync(true);
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new Common.ResponseWithStatus<string, bool>(default, true));
        foundationPermissionsLogic
            .Setup(e => e.CanInviteToSchool(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.AddStudentsToClass(classGuid, new List<Guid> { studentGuid });

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(400));
    }
}
