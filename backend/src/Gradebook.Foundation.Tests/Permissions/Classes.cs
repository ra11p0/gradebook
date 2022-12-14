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
    [Test]
    public async Task CannotAssignEducationCycleInClass()
    {
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), null))
            .ReturnsAsync(false);

        var result = await foundationCommands!.EditClassesAssignedToEducationCycle(new Guid[] { Guid.NewGuid() }, Guid.NewGuid());

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task CanAssignEducationCycleInClass()
    {
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), null))
            .ReturnsAsync(true);
        foundationQueriesRepository
            .Setup(e => e.GetClassesGuidsForEducationCycle(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Pager>()))
            .ReturnsAsync(new PagedList<Guid>() { Guid.NewGuid() });

        var result = await foundationCommands!.EditClassesAssignedToEducationCycle(new Guid[] { Guid.NewGuid() }, Guid.NewGuid());

        foundationCommandsRepository.Verify(e => e.DeleteActiveEducationCycleFromClasses(It.IsAny<IEnumerable<Guid>>()), Times.Exactly(1));
        foundationCommandsRepository.Verify(e => e.SetActiveEducationCycleToClasses(It.IsAny<IEnumerable<Guid>>(), It.IsAny<Guid>()), Times.Exactly(1));
        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public async Task ShouldConfigureEducationCycle()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            EducationCycleGuid = Guid.NewGuid(),
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                    new EducationCycleConfigurationStageCommand(){
                        EducationCycleStageGuid =  Guid.NewGuid(),
                        Order = 0,
                        DateSince = new DateTime(2022, 12, 01),
                        DateUntil = new DateTime(2022, 12, 10),
                        Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                            new EducationCycleConfigurationSubjectCommand(){
                                EducationCycleStageSubjectGuid= Guid.NewGuid()
                            }
                        }
                    },
                }
        };
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>("fakeUid", true));
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);
        foundationQueriesRepository
            .Setup(e => e.GetClassesGuidsForEducationCycle(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Pager>()))
            .ReturnsAsync(new PagedList<Guid>() { Guid.NewGuid() });
        foundationQueriesRepository
            .Setup(e => e.GetClassByGuid(It.IsAny<Guid>()))
            .ReturnsAsync(new ClassDto());
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());
        foundationCommandsRepository
            .Setup(e => e.ConfigureEducationCycleForClass(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<EducationCycleConfigurationCommand>()))
            .ReturnsAsync(new ResponseWithStatus<Guid>(Guid.NewGuid()));
        foundationCommandsRepository
            .Setup(e => e.ConfigureEducationCycleStageInstanceForEducationCycleInstance(It.IsAny<Guid>(), It.IsAny<EducationCycleConfigurationStageCommand>()))
            .ReturnsAsync(new ResponseWithStatus<Guid>(Guid.NewGuid()));
        foundationCommandsRepository
            .Setup(e => e.ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(It.IsAny<Guid>(), It.IsAny<IEnumerable<EducationCycleConfigurationSubjectCommand>>()))
            .ReturnsAsync(new StatusResponse(200));

        var result = await foundationCommands!.ConfigureEducationCycleForClass(Guid.NewGuid(), command);

        Assert.That(result.Status, Is.True);
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public async Task ShouldNotConfigureEducationCycle()
    {
        var command = new EducationCycleConfigurationCommand()
        {
            EducationCycleGuid = Guid.NewGuid(),
            DateSince = new DateTime(2022, 12, 01),
            DateUntil = new DateTime(2022, 12, 30),
            Stages = new List<EducationCycleConfigurationStageCommand>(){
                    new EducationCycleConfigurationStageCommand(){
                        EducationCycleStageGuid =  Guid.NewGuid(),
                        Order = 0,
                        DateSince = new DateTime(2022, 12, 01),
                        DateUntil = new DateTime(2022, 12, 10),
                        Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                            new EducationCycleConfigurationSubjectCommand(){
                                EducationCycleStageSubjectGuid= Guid.NewGuid()
                            }
                        }
                    },
                }
        };
        identityLogic
            .Setup(e => e.CurrentUserId())
            .ReturnsAsync(new ResponseWithStatus<string, bool>("fakeUid", true));
        foundationPermissionsLogic
            .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);
        foundationQueriesRepository
            .Setup(e => e.GetClassesGuidsForEducationCycle(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Pager>()))
            .ReturnsAsync(new PagedList<Guid>() { Guid.NewGuid() });
        foundationQueriesRepository
            .Setup(e => e.GetClassByGuid(It.IsAny<Guid>()))
            .ReturnsAsync(new ClassDto());
        foundationQueriesRepository
            .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(Guid.NewGuid());

        var result = await foundationCommands!.ConfigureEducationCycleForClass(Guid.NewGuid(), command);

        Assert.That(result.Status, Is.False);
        Assert.That(result.StatusCode, Is.EqualTo(403));
    }
}
