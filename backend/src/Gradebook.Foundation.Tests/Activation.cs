using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Logic.Commands;
using Microsoft.Extensions.DependencyInjection;
using Gradebook.Foundation.Common;
using Moq;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Logic.Queries;
using Gradebook.Foundation.Logic.Commands.Repositories;
using Gradebook.Foundation.Logic.Queries.Repositories.Interfaces;

namespace Gradebook.Foundation.Tests;

[Category("Unit")]
public class Activation
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
    public async Task ShouldActivatePersonAsStudent()
    {
        identityLogic.Setup(e => e.CurrentUserId())
        .ReturnsAsync(new ResponseWithStatus<string, bool>("userId", true));
        foundationQueriesRepository.Setup(e => e.GetInvitationByActivationCode(It.IsAny<string>()))
        .ReturnsAsync(new InvitationDto()
        {
            ExprationDate = Time.UtcNow.AddMinutes(10),
            InvitedPersonGuid = Guid.NewGuid(),
            SchoolRole = Common.Foundation.Enums.SchoolRoleEnum.Student
        });

        foundationCommandsRepository.Setup(e => e.UseInvitation(It.IsAny<UseInvitationCommand>()))
        .ReturnsAsync(new StatusResponse<bool>(true));

        foundationCommandsRepository.Setup(e => e.AssignUserToStudent(It.IsAny<string>(), It.IsAny<Guid>()))
        .ReturnsAsync(new StatusResponse<bool>(true));

        var activationCode = "QWERTY";
        var resp = await foundationCommands!.ActivatePerson(activationCode);
        Assert.That(resp.Status, Is.True);
        Assert.That(resp.StatusCode, Is.EqualTo(200));
        foundationCommandsRepository.Verify(e => e.AssignUserToStudent(It.IsAny<string>(), It.IsAny<Guid>()), Times.Exactly(1));
    }
    [Test]
    public async Task ShouldActivatePersonAsTeacher()
    {
        identityLogic.Setup(e => e.CurrentUserId())
        .ReturnsAsync(new ResponseWithStatus<string, bool>("userId", true));
        foundationQueriesRepository.Setup(e => e.GetInvitationByActivationCode(It.IsAny<string>()))
        .ReturnsAsync(new InvitationDto()
        {
            ExprationDate = Time.UtcNow.AddMinutes(10),
            InvitedPersonGuid = Guid.NewGuid(),
            SchoolRole = Common.Foundation.Enums.SchoolRoleEnum.Teacher
        });

        foundationCommandsRepository.Setup(e => e.UseInvitation(It.IsAny<UseInvitationCommand>()))
        .ReturnsAsync(new StatusResponse<bool>(true));

        foundationCommandsRepository.Setup(e => e.AssignUserToTeacher(It.IsAny<string>(), It.IsAny<Guid>()))
        .ReturnsAsync(new StatusResponse<bool>(true));

        var activationCode = "QWERTY";
        var resp = await foundationCommands!.ActivatePerson(activationCode);
        Assert.That(resp.Status, Is.True);
        Assert.That(resp.StatusCode, Is.EqualTo(200));
        foundationCommandsRepository.Verify(e => e.AssignUserToTeacher(It.IsAny<string>(), It.IsAny<Guid>()), Times.Exactly(1));
    }
    [Test]
    public async Task ShouldActivatePersonAsAdmin()
    {
        identityLogic.Setup(e => e.CurrentUserId())
        .ReturnsAsync(new ResponseWithStatus<string, bool>("userId", true));
        foundationQueriesRepository.Setup(e => e.GetInvitationByActivationCode(It.IsAny<string>()))
        .ReturnsAsync(new InvitationDto()
        {
            ExprationDate = Time.UtcNow.AddMinutes(10),
            InvitedPersonGuid = Guid.NewGuid(),
            SchoolRole = Common.Foundation.Enums.SchoolRoleEnum.Admin
        });

        foundationCommandsRepository.Setup(e => e.UseInvitation(It.IsAny<UseInvitationCommand>()))
        .ReturnsAsync(new StatusResponse<bool>(true));

        foundationCommandsRepository.Setup(e => e.AssignUserToAdministrator(It.IsAny<string>(), It.IsAny<Guid>()))
        .ReturnsAsync(new StatusResponse<bool>(true));

        var activationCode = "QWERTY";
        var resp = await foundationCommands!.ActivatePerson(activationCode);
        Assert.That(resp.Status, Is.True);
        Assert.That(resp.StatusCode, Is.EqualTo(200));
        foundationCommandsRepository.Verify(e => e.AssignUserToAdministrator(It.IsAny<string>(), It.IsAny<Guid>()), Times.Exactly(1));
    }
    [Test]
    public async Task ShouldNotActivatePerson_ExpiredInvitation()
    {
        identityLogic.Setup(e => e.CurrentUserId())
        .ReturnsAsync(new ResponseWithStatus<string, bool>("userId", true));
        foundationQueriesRepository.Setup(e => e.GetInvitationByActivationCode(It.IsAny<string>()))
        .ReturnsAsync(new InvitationDto()
        {
            ExprationDate = Time.UtcNow.AddMinutes(-10),
            InvitedPersonGuid = Guid.NewGuid(),
            SchoolRole = Common.Foundation.Enums.SchoolRoleEnum.Student
        });
        foundationCommandsRepository.Setup(e => e.UseInvitation(It.IsAny<UseInvitationCommand>()))
        .ReturnsAsync(new StatusResponse<bool>(true));
        foundationCommandsRepository.Setup(e => e.AssignUserToStudent(It.IsAny<string>(), It.IsAny<Guid>()))
        .ReturnsAsync(new StatusResponse<bool>(true));

        var activationCode = "QWERTY";
        var resp = await foundationCommands!.ActivatePerson(activationCode);
        Assert.That(resp.Status, Is.False);
        Assert.That(resp.StatusCode, Is.EqualTo(400));
    }
    [Test]
    public async Task ShouldNotActivatePerson_UsedInvitation()
    {
        identityLogic.Setup(e => e.CurrentUserId())
        .ReturnsAsync(new ResponseWithStatus<string, bool>("userId", true));
        foundationQueriesRepository.Setup(e => e.GetInvitationByActivationCode(It.IsAny<string>()))
        .ReturnsAsync(new InvitationDto()
        {
            IsUsed = true,
            ExprationDate = Time.UtcNow.AddMinutes(10),
            InvitedPersonGuid = Guid.NewGuid(),
            SchoolRole = Common.Foundation.Enums.SchoolRoleEnum.Student
        });
        foundationCommandsRepository.Setup(e => e.UseInvitation(It.IsAny<UseInvitationCommand>()))
        .ReturnsAsync(new StatusResponse<bool>(true));
        foundationCommandsRepository.Setup(e => e.AssignUserToStudent(It.IsAny<string>(), It.IsAny<Guid>()))
        .ReturnsAsync(new StatusResponse<bool>(true));

        var activationCode = "QWERTY";
        var resp = await foundationCommands!.ActivatePerson(activationCode);
        Assert.That(resp.Status, Is.False);
        Assert.That(resp.StatusCode, Is.EqualTo(400));
    }
    [Test]
    public async Task ShouldReturnInvitationsInSchool()
    {
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        var guid3 = Guid.NewGuid();
        PersonDto[] fakeInvitedPeople = new PersonDto[] {
             new PersonDto() { Guid = guid1 },
             new PersonDto() { Guid = guid2 },
             new PersonDto() { Guid = guid3 },
              };
        InvitationDto[] fakeInvitations = new InvitationDto[] {
            new InvitationDto() { InvitedPersonGuid = guid1},
            new InvitationDto() { InvitedPersonGuid = guid2} ,
            new InvitationDto() { InvitedPersonGuid = guid3}
            };
        foundationQueriesRepository.Setup(e => e.GetInvitationsToSchool(It.IsAny<Guid>(), It.IsAny<Pager>()))
            .ReturnsAsync(new PagedList<InvitationDto>(1, 3, 1, fakeInvitations.AsEnumerable()));
        foundationQueriesRepository.Setup(e => e.GetPersonByGuid(guid1))
            .ReturnsAsync(fakeInvitedPeople[0]);
        foundationQueriesRepository.Setup(e => e.GetPersonByGuid(guid2))
            .ReturnsAsync(fakeInvitedPeople[1]);
        foundationQueriesRepository.Setup(e => e.GetPersonByGuid(guid3))
            .ReturnsAsync(fakeInvitedPeople[2]);
        var result = await foundationQueries!.GetInvitationsToSchool(Guid.NewGuid(), 1);
        Assert.That(result.Response!.Select(e => e.InvitedPerson).ToArray(), Is.EquivalentTo(fakeInvitedPeople));
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public async Task ShouldReturnNotFoundActivationCode()
    {
        foundationQueriesRepository
            .Setup(e => e.GetInvitationByActivationCode(It.IsAny<string>()))
            .ReturnsAsync(default(InvitationDto));

        var info = await foundationQueries!.GetActivationCodeInfo("fakeCode", "student");
        Assert.That(info.Status, Is.False);
        Assert.That(info.StatusCode, Is.EqualTo(404));
    }

}