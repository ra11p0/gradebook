using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Logic.Commands.Repositories;
using Gradebook.Foundation.Logic.Queries;
using Gradebook.Foundation.Logic.Queries.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Gradebook.Foundation.Tests.Permissions;


[Category("Unit")]
public class Subjects
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
    public async Task CannotCreateNewSubject()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationPermissionsLogic
            .Setup(e => e.CanCreateNewSubject(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.AddSubject(
            Guid.NewGuid(),
            new NewSubjectCommand() { Name = "fakeName" });

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CanCreateNewSubject()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationCommandsRepository
             .Setup(e => e.AddSubject(It.IsAny<Guid>(), It.IsAny<NewSubjectCommand>()))
             .ReturnsAsync(new ResponseWithStatus<Guid>(true));
        foundationPermissionsLogic
            .Setup(e => e.CanCreateNewSubject(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var result = await foundationCommands!.AddSubject(
            Guid.NewGuid(),
            new NewSubjectCommand()
            {
                Name = "fakeName"
            });

        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public async Task CannotEditAnyTeachersInSubject()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationQueriesRepository
            .Setup(e => e.GetSubject(It.IsAny<Guid>()))
            .ReturnsAsync(new SubjectDto());
        foundationPermissionsLogic
            .Setup(e => e.CanManageSubject(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.EditTeachersInSubject(
            Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() }
        );

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CanEditTeachersInSubject()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationQueriesRepository
            .Setup(e => e.GetSubject(It.IsAny<Guid>()))
            .ReturnsAsync(new SubjectDto());
        foundationQueriesRepository
            .Setup(e => e.GetTeachersForSubject(It.IsAny<Guid>(), It.IsAny<Pager>(), It.IsAny<string>()))
            .ReturnsAsync(new PagedList<TeacherDto>() { new TeacherDto() });
        foundationCommandsRepository
            .Setup(e => e.AddTeachersToSubject(It.IsAny<Guid>(), It.IsAny<List<Guid>>()))
            .ReturnsAsync(new StatusResponse(true));
        foundationCommandsRepository
            .Setup(e => e.RemoveTeachersFromSubject(It.IsAny<Guid>(), It.IsAny<List<Guid>>()))
            .ReturnsAsync(new StatusResponse(true));
        foundationPermissionsLogic
            .Setup(e => e.CanManageSubject(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var result = await foundationCommands!.EditTeachersInSubject(
            Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() }
        );

        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
}
