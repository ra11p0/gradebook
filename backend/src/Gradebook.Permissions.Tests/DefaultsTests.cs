using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Permissions.Enums;
using Gradebook.Foundation.Common.Permissions.Queries;
using Gradebook.Foundation.Logic.Queries;
using Gradebook.Permissions.Logic.Queries;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Gradebook.Permissions.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public async Task ShouldThrowCouldNotGetPerson()
    {
        Mock<IPermissionsQueriesRepository> permissionsQueriesRepository = new();
        Mock<IFoundationQueriesRepository> foundationQueriesRepository = new();
        var serviceCollection = new ServiceCollection();

        foundationQueriesRepository
        .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
        .Returns(Task.FromResult<PersonDto>(null));

        serviceCollection.AddScoped((e) => foundationQueriesRepository.Object);
        serviceCollection.AddScoped((e) => permissionsQueriesRepository.Object);
        serviceCollection.AddScoped<IFoundationQueries>((e) => new FoundationQueries(foundationQueriesRepository.Object, e));

        IPermissionsQueries permissionsQueries = new PermissionsQueries(permissionsQueriesRepository.Object, serviceCollection.BuildServiceProvider());

        try
        {
            var permissions = await permissionsQueries.GetPermissionsForPerson(new Guid());
        }
        catch (Exception ex)
        {
            Assert.That(ex.Message, Is.EqualTo("Could not get person!"));
            return;
        }
        Assert.Fail();
    }
    [Test]
    public async Task ShouldReturnDefaultPermissionsStudent()
    {
        Mock<IPermissionsQueriesRepository> permissionsQueriesRepository = new();
        Mock<IFoundationQueriesRepository> foundationQueriesRepository = new();
        var serviceCollection = new ServiceCollection();

        foundationQueriesRepository
        .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new PersonDto() { SchoolRole = Foundation.Common.Foundation.Enums.SchoolRoleEnum.Student }));

        permissionsQueriesRepository
        .Setup(e => e.GetPermissionsForPerson(It.IsAny<Guid>()))
        .Returns(Task.FromResult(Enumerable.Empty<Tuple<PermissionEnum, PermissionLevelEnum>>()));

        serviceCollection.AddScoped((e) => foundationQueriesRepository.Object);
        serviceCollection.AddScoped((e) => permissionsQueriesRepository.Object);
        serviceCollection.AddScoped<IFoundationQueries>((e) => new FoundationQueries(foundationQueriesRepository.Object, e));

        IPermissionsQueries permissionsQueries = new PermissionsQueries(permissionsQueriesRepository.Object, serviceCollection.BuildServiceProvider());

        var permissions = await permissionsQueries.GetPermissionsForPerson(new Guid());

        Assert.That(permissions, Is.EqualTo(DefaultPermissionLevels.GetDefaultPermissionLevels(Foundation.Common.Foundation.Enums.SchoolRoleEnum.Student)));
    }
    [Test]
    public async Task ShouldNotReturnDefaultPermissionsStudent()
    {
        Mock<IPermissionsQueriesRepository> permissionsQueriesRepository = new();
        Mock<IFoundationQueriesRepository> foundationQueriesRepository = new();
        var serviceCollection = new ServiceCollection();

        foundationQueriesRepository
        .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new PersonDto() { SchoolRole = Foundation.Common.Foundation.Enums.SchoolRoleEnum.Student }));

        permissionsQueriesRepository
        .Setup(e => e.GetPermissionsForPerson(It.IsAny<Guid>()))
        .Returns(Task.FromResult((new Tuple<PermissionEnum, PermissionLevelEnum>[] { new Tuple<PermissionEnum, PermissionLevelEnum>(PermissionEnum.Invitations, PermissionLevelEnum.Invitations_CanInvite) }).AsEnumerable()));

        serviceCollection.AddScoped((e) => foundationQueriesRepository.Object);
        serviceCollection.AddScoped((e) => permissionsQueriesRepository.Object);
        serviceCollection.AddScoped<IFoundationQueries>((e) => new FoundationQueries(foundationQueriesRepository.Object, e));

        IPermissionsQueries permissionsQueries = new PermissionsQueries(permissionsQueriesRepository.Object, serviceCollection.BuildServiceProvider());

        var permissions = await permissionsQueries.GetPermissionsForPerson(new Guid());

        Assert.That(permissions, Is.Not.EqualTo(DefaultPermissionLevels.GetDefaultPermissionLevels(Foundation.Common.Foundation.Enums.SchoolRoleEnum.Student)));
    }
    [Test]
    public async Task ShouldReturnDefaultPermissionsAdmin()
    {
        Mock<IPermissionsQueriesRepository> permissionsQueriesRepository = new();
        Mock<IFoundationQueriesRepository> foundationQueriesRepository = new();
        var serviceCollection = new ServiceCollection();

        foundationQueriesRepository
        .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new PersonDto() { SchoolRole = Foundation.Common.Foundation.Enums.SchoolRoleEnum.Admin }));

        permissionsQueriesRepository
        .Setup(e => e.GetPermissionsForPerson(It.IsAny<Guid>()))
        .Returns(Task.FromResult(Enumerable.Empty<Tuple<PermissionEnum, PermissionLevelEnum>>()));

        serviceCollection.AddScoped((e) => foundationQueriesRepository.Object);
        serviceCollection.AddScoped((e) => permissionsQueriesRepository.Object);
        serviceCollection.AddScoped<IFoundationQueries>((e) => new FoundationQueries(foundationQueriesRepository.Object, e));

        IPermissionsQueries permissionsQueries = new PermissionsQueries(permissionsQueriesRepository.Object, serviceCollection.BuildServiceProvider());

        var permissions = await permissionsQueries.GetPermissionsForPerson(new Guid());

        Assert.That(permissions, Is.EqualTo(DefaultPermissionLevels.GetDefaultPermissionLevels(Foundation.Common.Foundation.Enums.SchoolRoleEnum.Admin)));
    }
}