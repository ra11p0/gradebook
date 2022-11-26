using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Logic.Queries;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Gradebook.Foundation.Tests;

[Category("Unit")]
public class Subjects
{/*
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
    public async Task CannotCreateInvitation()
    {
        foundationQueriesRepository.Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(Guid.NewGuid());
        identityLogic.Setup(e => e.CurrentUserId()).ReturnsAsync(new Common.ResponseWithStatus<string, bool>(default, true));
        foundationPermissionsLogic.Setup(e => e.CanInviteToSchool(It.IsAny<Guid>())).ReturnsAsync(false);
        var result = await foundationCommands!.GenerateSystemInvitation(new Guid(), Common.Foundation.Enums.SchoolRoleEnum.Admin, new Guid());
        Assert.That(result.Status, Is.False);
    }*/
}
