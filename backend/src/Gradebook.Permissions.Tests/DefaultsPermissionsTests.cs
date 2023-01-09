using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Permissions.Enums;
using Gradebook.Foundation.Common.Permissions.Queries;
using Gradebook.Foundation.Logic.Queries;
using Gradebook.Foundation.Logic.Queries.Repositories.Interfaces;
using Gradebook.Permissions.Logic.Queries;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Gradebook.Permissions.Tests;

[Category("Unit")]
public class DefaultPermissionsTests
{
    readonly Mock<IPermissionsQueriesRepository> permissionsQueriesRepository = new();
    readonly Mock<IFoundationQueriesRepository> foundationQueriesRepository = new();
    readonly ServiceCollection serviceCollection = new();
    private IPermissionsQueries? permissionsQueries;
    [SetUp]
    public void Setup()
    {
        serviceCollection.AddScoped((e) => foundationQueriesRepository.Object);
        serviceCollection.AddScoped((e) => permissionsQueriesRepository.Object);
        serviceCollection.AddScoped<IFoundationQueries>((e) => new FoundationQueries(foundationQueriesRepository.Object, e));
        permissionsQueries = new PermissionsQueries(permissionsQueriesRepository.Object, serviceCollection.BuildServiceProvider());
    }

    [Test]
    public async Task ShouldThrowCouldNotGetPerson()
    {
        foundationQueriesRepository
        .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
        .Returns(Task.FromResult<PersonDto?>(null)!);

        try
        {
            var permissions = await permissionsQueries!.GetPermissionsForPerson(new Guid());
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
        foundationQueriesRepository
        .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new PersonDto() { SchoolRole = Foundation.Common.Foundation.Enums.SchoolRoleEnum.Student }));

        permissionsQueriesRepository
        .Setup(e => e.GetPermissionsForPerson(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new Dictionary<PermissionEnum, PermissionLevelEnum>()));

        var permissions = await permissionsQueries!.GetPermissionsForPerson(new Guid());

        Assert.That(permissions, Is.EqualTo(permissionsQueries.GetDefaultPermissionLevels(Foundation.Common.Foundation.Enums.SchoolRoleEnum.Student)));
    }
    [Test]
    public async Task ShouldNotReturnDefaultPermissionsStudent()
    {
        foundationQueriesRepository
        .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new PersonDto() { SchoolRole = Foundation.Common.Foundation.Enums.SchoolRoleEnum.Student }));

        permissionsQueriesRepository
        .Setup(e => e.GetPermissionsForPerson(It.IsAny<Guid>()))
        .Returns(Task.FromResult((new KeyValuePair<PermissionEnum, PermissionLevelEnum>[] { new KeyValuePair<PermissionEnum, PermissionLevelEnum>(PermissionEnum.Invitations, PermissionLevelEnum.Invitations_CanInvite) }).ToDictionary(e => e.Key, e => e.Value)));

        var permissions = await permissionsQueries!.GetPermissionsForPerson(new Guid());

        Assert.That(permissions, Is.Not.EqualTo(permissionsQueries.GetDefaultPermissionLevels(Foundation.Common.Foundation.Enums.SchoolRoleEnum.Student)));
    }
    [Test]
    public async Task ShouldReturnDefaultPermissionsAdmin()
    {
        foundationQueriesRepository
        .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new PersonDto() { SchoolRole = Foundation.Common.Foundation.Enums.SchoolRoleEnum.Admin }));

        permissionsQueriesRepository
        .Setup(e => e.GetPermissionsForPerson(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new Dictionary<PermissionEnum, PermissionLevelEnum>()));

        var permissions = await permissionsQueries!.GetPermissionsForPerson(new Guid());

        Assert.That(permissions, Is.EqualTo(permissionsQueries.GetDefaultPermissionLevels(Foundation.Common.Foundation.Enums.SchoolRoleEnum.Admin)));
    }
    [Test]
    public async Task ShouldReturnAllAdminPermissionsHaveValue()
    {
        foundationQueriesRepository
        .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new PersonDto() { SchoolRole = Foundation.Common.Foundation.Enums.SchoolRoleEnum.Admin }));

        permissionsQueriesRepository
        .Setup(e => e.GetPermissionsForPerson(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new Dictionary<PermissionEnum, PermissionLevelEnum>()));

        var permissions = await permissionsQueries!.GetPermissionsForPerson(new Guid());

        Assert.That(permissions.Any(e => e.Value == 0), Is.False);
    }
    [Test]
    public async Task ShouldReturnAllStudentPermissionsHaveValue()
    {
        foundationQueriesRepository
        .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new PersonDto() { SchoolRole = Foundation.Common.Foundation.Enums.SchoolRoleEnum.Student }));

        permissionsQueriesRepository
        .Setup(e => e.GetPermissionsForPerson(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new Dictionary<PermissionEnum, PermissionLevelEnum>()));

        var permissions = await permissionsQueries!.GetPermissionsForPerson(new Guid());

        Assert.That(permissions.Any(e => e.Value == 0), Is.False);
    }
    [Test]
    public async Task ShouldReturnAllTeacherPermissionsHaveValue()
    {
        foundationQueriesRepository
        .Setup(e => e.GetPersonByGuid(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new PersonDto() { SchoolRole = Foundation.Common.Foundation.Enums.SchoolRoleEnum.Teacher }));

        permissionsQueriesRepository
        .Setup(e => e.GetPermissionsForPerson(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new Dictionary<PermissionEnum, PermissionLevelEnum>()));

        var permissions = await permissionsQueries!.GetPermissionsForPerson(new Guid());

        Assert.That(permissions.Any(e => e.Value == 0), Is.False);
    }
}