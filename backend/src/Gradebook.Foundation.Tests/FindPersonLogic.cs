using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Logic.Queries;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MySqlX.XDevAPI.Common;

namespace Gradebook.Foundation.Tests;

[Category("Unit")]
public class FindPersonLogic
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
    }
    [Test]
    public async Task CorrectlyRecognisesPersonForUserWithoutSchool()
    {
        var currentUserId = "currentUserId";
        var relatedPerson = new PersonDto()
        {
            SchoolGuid = Guid.NewGuid(),
            Guid = Guid.NewGuid()
        };
        var searchedPerson = new PersonDto()
        {
            SchoolGuid = relatedPerson.SchoolGuid,
            Guid = Guid.NewGuid()
        };

        identityLogic.Setup(e => e.CurrentUserId()).ReturnsAsync(new Common.ResponseWithStatus<string, bool>(currentUserId, true));
        foundationQueriesRepository.Setup(e => e.GetSchoolsForUser(currentUserId)).ReturnsAsync((new SchoolDto[] {
            new SchoolDto(){
                Guid = searchedPerson.SchoolGuid.Value
            }
        }).AsEnumerable());
        foundationQueriesRepository.Setup(e => e.GetPersonByGuid(relatedPerson.Guid)).ReturnsAsync(relatedPerson);
        foundationQueriesRepository.Setup(e => e.GetPersonByGuid(searchedPerson.Guid)).ReturnsAsync(searchedPerson);
        foundationQueriesRepository.Setup(e => e.GetPersonGuidForUser(currentUserId, searchedPerson.SchoolGuid.Value)).ReturnsAsync(searchedPerson.Guid);

        var response = await foundationQueries!.RecogniseCurrentPersonByRelatedPerson(relatedPerson.Guid);
        Assert.That(response.Response, Is.EqualTo(searchedPerson.Guid));
        Assert.That(response.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public async Task ShouldNotRecogniseRelatedPerson()
    {
        var currentUserId = "currentUserId";
        var relatedPerson = new PersonDto()
        {
            SchoolGuid = Guid.NewGuid(),
            Guid = Guid.NewGuid()
        };
        var searchedPerson = new PersonDto()
        {
            SchoolGuid = Guid.NewGuid(),
            Guid = Guid.NewGuid()
        };

        identityLogic.Setup(e => e.CurrentUserId()).ReturnsAsync(new Common.ResponseWithStatus<string, bool>(currentUserId, true));
        foundationQueriesRepository.Setup(e => e.GetSchoolsForUser(currentUserId)).ReturnsAsync((new SchoolDto[] {
            new SchoolDto(){
                Guid = searchedPerson.SchoolGuid.Value
            }
        }).AsEnumerable());
        foundationQueriesRepository.Setup(e => e.GetPersonByGuid(relatedPerson.Guid)).ReturnsAsync(relatedPerson);
        foundationQueriesRepository.Setup(e => e.GetPersonByGuid(searchedPerson.Guid)).ReturnsAsync(searchedPerson);
        foundationQueriesRepository.Setup(e => e.GetPersonGuidForUser(currentUserId, searchedPerson.SchoolGuid.Value)).ReturnsAsync(searchedPerson.Guid);

        var response = await foundationQueries!.RecogniseCurrentPersonByRelatedPerson(relatedPerson.Guid);
        Assert.That(response.Status is false);
        Assert.That(response.StatusCode, Is.EqualTo(404));
    }
}
