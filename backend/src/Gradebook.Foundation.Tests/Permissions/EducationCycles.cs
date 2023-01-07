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
using Gradebook.Foundation.Logic.Queries.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Gradebook.Foundation.Tests.Permissions;


[Category("Unit")]
public class EducationCycles
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
    public async Task CannotSeeEducationCycles()
    {
        foundationPermissionsLogic
            .Setup(e => e.CanSeeEducationCycles(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await foundationQueries!.GetEducationCyclesInSchool(Guid.NewGuid(), 0, "");

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
            .Setup(e => e.GetEducationCyclesInSchool(It.IsAny<Guid>(), It.IsAny<Pager>(), It.IsAny<string>()))
            .ReturnsAsync(new PagedList<EducationCycleDto>());

        var result = await foundationQueries!.GetEducationCyclesInSchool(Guid.NewGuid(), 0, "");

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
    [Test]
    public async Task ShouldNotAllowToStartEducationCycleStepInstance()
    {
        foundationPermissionsLogic
          .Setup(e => e.CanManageClass(It.IsAny<Guid>(), null))
          .ReturnsAsync(false);
        var resultStart = await foundationCommands!.StartEducationCycleStepInstance(Guid.NewGuid());
        var resultForward = await foundationCommands!.ForwardEducationCycleStepInstance(Guid.NewGuid());
        var resultStop = await foundationCommands!.StopEducationCycleStepInstance(Guid.NewGuid());

        Assert.That(resultStart.StatusCode, Is.EqualTo(403));
        Assert.That(resultForward.StatusCode, Is.EqualTo(403));
        Assert.That(resultStop.StatusCode, Is.EqualTo(403));
    }
    [Test]
    public async Task ShouldAllowToStartEducationCycleStepInstance()
    {
        var currentGuid = Guid.NewGuid();
        var nextGuid = Guid.NewGuid();

        foundationPermissionsLogic
          .Setup(e => e.CanManageClass(It.IsAny<Guid>(), null))
          .ReturnsAsync(true);
        foundationCommandsRepository
            .Setup(e => e.StartEducationCycleStepInstance(It.IsAny<Guid>()))
            .ReturnsAsync(new StatusResponse(true));
        foundationCommandsRepository
            .Setup(e => e.StopEducationCycleStepInstance(It.IsAny<Guid>()))
            .ReturnsAsync(new StatusResponse(true));
        foundationQueriesRepository
             .Setup(e => e.GetAllEducationCycleStepInstancesForClass(It.IsAny<Guid>()))
             .ReturnsAsync(new EducationCycleStepInstanceDto[] {
                    new EducationCycleStepInstanceDto(){
                        StartedDate = new DateTime(),
                        FinishedDate = new DateTime(),
                        Order = 0
                    }, new EducationCycleStepInstanceDto(){
                        StartedDate= new DateTime(),
                        Guid = currentGuid,
                        Order = 1
                    },
                    new EducationCycleStepInstanceDto(){
                        Guid = nextGuid,
                        Order = 2
                    },
                });
        var resultStart = await foundationCommands!.StartEducationCycleStepInstance(Guid.NewGuid());
        var resultForward = await foundationCommands!.ForwardEducationCycleStepInstance(Guid.NewGuid());
        foundationCommandsRepository.Verify(e => e.StopEducationCycleStepInstance(currentGuid), Times.Exactly(1));
        var resultStop = await foundationCommands!.StopEducationCycleStepInstance(Guid.NewGuid());

        Assert.That(resultStart.StatusCode, Is.EqualTo(200));
        Assert.That(resultForward.StatusCode, Is.EqualTo(200));
        Assert.That(resultStop.StatusCode, Is.EqualTo(200));
        foundationCommandsRepository.Verify(e => e.StopEducationCycleStepInstance(currentGuid), Times.Exactly(2));
        foundationCommandsRepository.Verify(e => e.StartEducationCycleStepInstance(nextGuid), Times.Exactly(1));
        foundationCommandsRepository.Verify(e => e.StartEducationCycleStepInstance(currentGuid), Times.Exactly(1));
    }
}