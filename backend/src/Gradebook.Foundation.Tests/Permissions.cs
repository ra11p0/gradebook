using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Logic.Queries;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Gradebook.Foundation.Tests;

[Category("Unit")]
public class Permissions
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
    #region Invitations
    [Test]
    public async Task CannotCreateInvitation()
    {
        foundationQueriesRepository.Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(Guid.NewGuid());
        identityLogic.Setup(e => e.CurrentUserId()).ReturnsAsync(new Common.ResponseWithStatus<string, bool>(default, true));
        foundationPermissionsLogic.Setup(e => e.CanInviteToSchool(It.IsAny<Guid>())).ReturnsAsync(false);
        var result = await foundationCommands!.GenerateSystemInvitation(new Guid(), Common.Foundation.Enums.SchoolRoleEnum.Admin, new Guid());
        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CannotCreateInvitations()
    {
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new Common.ResponseWithStatus<string, bool>(default, true));
        foundationPermissionsLogic
            .Setup(e => e.CanInviteToSchool(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.GenerateMultipleSystemInvitation(
                new Guid[] { new Guid() },
                SchoolRoleEnum.Admin,
                new Guid()
            );

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    #endregion

    #region Schools
    //  TODO: Schools permissions test

    #endregion

    #region Students
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
    #endregion

    #region Subjects
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
            .Setup(e => e.GetTeachersForSubject(It.IsAny<Guid>(), It.IsAny<Pager>()))
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

    #endregion

    #region Classes
    [Test]
    public async Task CannotCreateClass()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationPermissionsLogic
            .Setup(e => e.CanCreateNewClass(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.AddNewClass(new NewClassCommand());

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CanCreateClass()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationCommandsRepository
            .Setup(e => e.AddNewClass(It.IsAny<NewClassCommand>()))
            .ReturnsAsync(new ResponseWithStatus<Guid>(Guid.NewGuid()));
        foundationPermissionsLogic
            .Setup(e => e.CanCreateNewClass(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var result = await foundationCommands!.AddNewClass(new NewClassCommand());

        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public async Task CannotEditStudentsInClass()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.EditStudentsInClass(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CannotAddStudentsToClass()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.AddStudentsToClass(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CannotDeleteStudentsFromClass()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.DeleteStudentsFromClass(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CannotEditTeachersInClass()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.EditTeachersInClass(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CannotAddTeachersToClass()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.AddTeachersToClass(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CannotDeleteTeachersFromClass()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.AddTeachersToClass(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CanEditStudentsInClass()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationCommandsRepository
            .Setup(e => e.AddStudentsToClass(It.IsAny<Guid>(), It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync(new StatusResponse(true));
        foundationCommandsRepository
            .Setup(e => e.SetStudentActiveClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new StatusResponse(true));
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var result = await foundationCommands!.EditStudentsInClass(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });

        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public async Task CanEditTeachersInClass()
    {
        var schoolGuid = Guid.NewGuid();
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>("fakeUserId", true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
            .ReturnsAsync(new PersonDto() { SchoolGuid = schoolGuid });
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationQueriesRepository
            .Setup(e => e.GetSchoolsForUser(It.IsAny<string>()))
            .ReturnsAsync(new SchoolDto[] { new SchoolDto() { Guid = schoolGuid } });
        foundationCommandsRepository
            .Setup(e => e.AddTeachersToClass(It.IsAny<Guid>(), It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync(new StatusResponse(true));
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);
        var result = await foundationCommands!.EditTeachersInClass(Guid.NewGuid(), new List<Guid>() { Guid.NewGuid() });

        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public async Task CannotRemoveClass()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationQueriesRepository
            .Setup(e => e.GetClassByGuid(It.IsAny<Guid>()))
            .ReturnsAsync(new ClassDto() { SchoolGuid = Guid.NewGuid() });
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationCommands!.DeleteClass(Guid.NewGuid());

        foundationCommandsRepository.Verify(e => e.DeleteClass(It.IsAny<Guid>()), Times.Never());
        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CanRemoveClass()
    {
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationQueriesRepository
            .Setup(e => e.GetClassByGuid(It.IsAny<Guid>()))
            .ReturnsAsync(new ClassDto() { SchoolGuid = Guid.NewGuid() });
        foundationCommandsRepository
            .Setup(e => e.DeleteClass(It.IsAny<Guid>()))
            .ReturnsAsync(new StatusResponse(true));
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var result = await foundationCommands!.DeleteClass(Guid.NewGuid());

        foundationCommandsRepository.Verify(e => e.DeleteClass(It.IsAny<Guid>()), Times.AtMostOnce());
        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    #endregion

    #region Education cycles
    [Test]
    public async Task CannotSeeEducationCycles()
    {
        foundationPermissionsLogic
            .Setup(e => e.CanSeeEducationCycles(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationQueries!.GetEducationCyclesInSchool(Guid.NewGuid(), 0);

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CanSeeEducationCycles()
    {
        foundationPermissionsLogic
            .Setup(e => e.CanSeeEducationCycles(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        foundationQueriesRepository
            .Setup(e => e.GetEducationCyclesInSchool(It.IsAny<Guid>(), It.IsAny<Pager>()))
            .ReturnsAsync(new PagedList<EducationCycleDto>());

        var result = await foundationQueries!.GetEducationCyclesInSchool(Guid.NewGuid(), 0);

        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public async Task CanCreateEducationCycle()
    {
        foundationPermissionsLogic
            .Setup(e => e.CanCreateEducationCycle(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationCommandsRepository
            .Setup(e => e.AddNewEducationCycle(It.IsAny<EducationCycleCommand>()))
            .ReturnsAsync(new ResponseWithStatus<Guid>(Guid.NewGuid()));
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));


        var result = await foundationCommands!.AddNewEducationCycle(new EducationCycleCommand()
        {
            SchoolGuid = Guid.NewGuid(),
            Name = "Fake name",
            Stages = new List<EducationCycleStepCommand> {
            new EducationCycleStepCommand()
            {
                Name="Fake name",
                Subjects = new List<EducationCycleStepSubjectCommand>(){
                    new EducationCycleStepSubjectCommand(){
                    SubjectGuid= Guid.NewGuid(),
                    HoursNo = 40
                    }
                }
            }
            }
        });

        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public async Task CannotCreateEducationCycle()
    {
        foundationPermissionsLogic
            .Setup(e => e.CanCreateEducationCycle(It.IsAny<Guid>()))
            .ReturnsAsync(false);
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());

        var result = await foundationCommands!.AddNewEducationCycle(new EducationCycleCommand()
        {
            SchoolGuid = Guid.NewGuid(),
            Name = "Fake name",
            Stages = new List<EducationCycleStepCommand> {
            new EducationCycleStepCommand()
            {
                Name="Fake name",
                Subjects = new List<EducationCycleStepSubjectCommand>(){
                    new EducationCycleStepSubjectCommand(){
                    SubjectGuid= Guid.NewGuid(),
                    HoursNo = 40
                    }
                }
            }
            }
        });

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    #endregion
}
