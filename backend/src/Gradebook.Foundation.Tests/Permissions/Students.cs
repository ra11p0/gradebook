using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Logic.Commands.Repositories;
using Gradebook.Foundation.Logic.Queries;
using Gradebook.Foundation.Logic.Queries.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Gradebook.Foundation.Tests.Permissions;

[Category("Unit")]
public class Students
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
    public async Task CannotCreateNewStudent()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationPermissionsLogic
            .Setup(e => e.CanCreateNewStudents(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.AddNewStudent(new NewStudentCommand(), Guid.NewGuid());

        foundationCommandsRepository.Verify(e => e.AddPersonToSchool(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never());
        foundationCommandsRepository.Verify(e => e.AddNewStudent(It.IsAny<NewStudentCommand>()), Times.Never());
        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CanCreateNewStudent()
    {
        identityLogic.Setup(e => e.CurrentUserId()).ReturnsAsync(new Common.ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationCommandsRepository
            .Setup(e => e.AddNewStudent(It.IsAny<NewStudentCommand>()))
            .ReturnsAsync(new ResponseWithStatus<Guid>(Guid.NewGuid()));
        foundationCommandsRepository
            .Setup(e => e.AddPersonToSchool(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new StatusResponse<bool>(true));
        foundationPermissionsLogic.Setup(e => e.CanCreateNewStudents(It.IsAny<Guid>())).ReturnsAsync(true);

        var result = await foundationCommands!.AddNewStudent(new NewStudentCommand(), Guid.NewGuid());

        foundationCommandsRepository.Verify(e => e.AddPersonToSchool(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.AtMostOnce());
        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public async Task CannotDeleteStudent()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationQueriesRepository
            .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
            .ReturnsAsync(new PersonDto()
            {
                SchoolRole = SchoolRoleEnum.Student,
                SchoolGuid = Guid.NewGuid()
            });
        foundationPermissionsLogic
            .Setup(e => e.CanDeleteStudents(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.DeletePerson(Guid.NewGuid());

        foundationCommandsRepository.Verify(e => e.DeletePerson(It.IsAny<Guid>()), Times.Never());
        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CanDeleteStudent()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new Common.ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationQueriesRepository
            .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
            .ReturnsAsync(new PersonDto()
            {
                SchoolRole = SchoolRoleEnum.Student,
                SchoolGuid = Guid.NewGuid()
            });
        foundationPermissionsLogic
            .Setup(e => e.CanDeleteStudents(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        foundationCommandsRepository
            .Setup(e => e.DeletePerson(It.IsAny<Guid>()))
            .ReturnsAsync(new StatusResponse(true));

        var result = await foundationCommands!.DeletePerson(Guid.NewGuid());

        foundationCommandsRepository.Verify(e => e.DeletePerson(It.IsAny<Guid>()), Times.AtMostOnce());
        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
}
