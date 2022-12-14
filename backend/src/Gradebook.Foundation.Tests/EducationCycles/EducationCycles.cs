using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Gradebook.Foundation.Database;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Logic.Queries.Repositories;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Logic.Queries;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Logic.Commands.Repositories;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

namespace Gradebook.Foundation.Tests.EducationCycles
{
    [Category("Unit")]
    internal class EducationCycles
    {
        private ServiceCollection _serviceCollection
        {
            get
            {
                var services = new ServiceCollection();
                services.AddScoped<IIdentityLogic>((e) => _identityLogic.Object);
                services.AddScoped<IFoundationQueriesRepository>((e) => _foundationQueriesRepository.Object);
                services.AddScoped<IFoundationPermissionsLogic>((e) => _foundationPermissionsLogic.Object);
                services.AddScoped<IFoundationQueries, FoundationQueries>();
                services.AddScoped<IFoundationCommands, FoundationCommands>();
                services.AddScoped<IFoundationCommandsRepository, FoundationCommandsRepository>();
                services.AddAutoMapper(o =>
                    {
                        o.AddProfile<FoundationMappings>();
                        o.AddProfile<FoundationCommandsMappings>();
                        o.AddProfile<FoundationQueriesMappings>();
                    });
                services.AddSingleton<FoundationDatabaseContext>(o =>
                    {
                        var opt = new DbContextOptionsBuilder<FoundationDatabaseContext>()
                            .UseInMemoryDatabase("fakeDb")
                            .ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                        return new FoundationDatabaseContext(opt.Options);
                    });
                services.AddScoped<Context>(e => _context);
                return services;
            }
        }
        private IServiceProvider _serviceProvider => _serviceCollection.BuildServiceProvider();
        private Mock<IIdentityLogic> _identityLogic { get; set; } = new();
        private Mock<IFoundationQueriesRepository> _foundationQueriesRepository { get; set; } = new();
        private Mock<IFoundationPermissionsLogic> _foundationPermissionsLogic { get; set; } = new();
        private IFoundationQueries _foundationQueries => _serviceProvider.GetResolver<IFoundationQueries>().Service;
        private IFoundationCommands _foundationCommands => _serviceProvider.GetResolver<IFoundationCommands>().Service;
        private ServiceResolver<FoundationDatabaseContext> _db => _serviceProvider.GetResolver<FoundationDatabaseContext>();
        private Context _context = new();
        [SetUp]
        public void SetUp()
        {
            _db.Service.Database.EnsureDeleted();
        }

        [Test]
        public async Task ShouldReturnNotFound()
        {
            _foundationQueriesRepository
                .Setup(e => e.GetEducationCycle(It.IsAny<Guid>()))
                .ReturnsAsync(default(EducationCycleExtendedDto));
            _identityLogic
                .Setup(e => e.CurrentUserId())
                .ReturnsAsync(new Common.ResponseWithStatus<string, bool>(default, true));
            _foundationPermissionsLogic
                .Setup(e => e.CanSeeEducationCycles(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var result = await _foundationQueries.GetEducationCycle(new Guid());

            Assert.That(result.Status, Is.False);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }
        [Test]
        public async Task ShouldReturnEducationCycle()
        {
            _foundationQueriesRepository
                .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(Guid.NewGuid());
            _foundationQueriesRepository
                .Setup(e => e.GetEducationCycle(It.IsAny<Guid>()))
                .ReturnsAsync(new EducationCycleExtendedDto() { });
            _identityLogic
                .Setup(e => e.CurrentUserId())
                .ReturnsAsync(new Common.ResponseWithStatus<string, bool>(default, true));
            _foundationPermissionsLogic
                .Setup(e => e.CanSeeEducationCycles(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var result = await _foundationQueries.GetEducationCycle(new Guid());

            Assert.That(result.Status, Is.True);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
        [Test]
        public async Task ShouldReturnEducationCyclesInSchool()
        {
            _foundationQueriesRepository
                .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(Guid.NewGuid());
            _foundationQueriesRepository
                 .Setup(e => e.GetEducationCyclesInSchool(It.IsAny<Guid>(), It.IsAny<Pager>(), It.IsAny<string>()))
                 .ReturnsAsync(new PagedList<EducationCycleDto>() { });
            _identityLogic
                .Setup(e => e.CurrentUserId())
                .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
            _foundationPermissionsLogic
                .Setup(e => e.CanSeeEducationCycles(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var result = await _foundationQueries.GetEducationCyclesInSchool(new Guid(), 0, "");

            Assert.That(result.Status, Is.True);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
        [Test]
        public async Task ShouldCreateNewEducationCycle()
        {
            EducationCycleCommand? command = new EducationCycleCommand()
            {
                SchoolGuid = Guid.NewGuid(),
                Name = "Fake name",
                Stages = new List<EducationCycleStepCommand> {
                    new EducationCycleStepCommand()
                    {
                        Name="Fake step name",
                        Subjects = new List<EducationCycleStepSubjectCommand>(){
                            new EducationCycleStepSubjectCommand(){
                            SubjectGuid= Guid.NewGuid(),
                            HoursNo = 40
                            }
                        }
                    }
                }
            };
            _foundationQueriesRepository
                .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(Guid.NewGuid());
            _identityLogic
                .Setup(e => e.CurrentUserId())
                .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
            _foundationPermissionsLogic
                .Setup(e => e.CanCreateEducationCycle(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var resp = await _foundationCommands.AddNewEducationCycle(command);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(_db.Service.EducationCycles!.FirstOrDefault(e => e.Name == "Fake name"), Is.Not.Null);
            Assert.That(_db.Service.EducationCycleSteps!.FirstOrDefault(e => e.Name == "Fake step name"), Is.Not.Null);
            Assert.That(_db.Service.EducationCycleStepSubjects!.FirstOrDefault(e => e.HoursInStep == 40), Is.Not.Null);
        }
        [Test]
        public async Task ShouldCreateNewEducationCycleInstance()
        {
            EducationCycleCommand? educationCycleCommand = new EducationCycleCommand()
            {
                SchoolGuid = Guid.NewGuid(),
                Name = "Fake name",
                Stages = new List<EducationCycleStepCommand> {
                    new EducationCycleStepCommand()
                    {
                        Name="Fake step name",
                        Subjects = new List<EducationCycleStepSubjectCommand>(){
                            new EducationCycleStepSubjectCommand(){
                            SubjectGuid= Guid.NewGuid(),
                            HoursNo = 40
                            }
                        }
                    }
                }
            };

            var classCommand = new NewClassCommand();

            _foundationQueriesRepository
                .Setup(e => e.GetPersonGuidForUser(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(Guid.NewGuid());
            _identityLogic
                .Setup(e => e.CurrentUserId())
                .ReturnsAsync(new ResponseWithStatus<string, bool>(default, true));
            _foundationPermissionsLogic
                .Setup(e => e.CanManageClass(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(true);
            _foundationPermissionsLogic
                .Setup(e => e.CanCreateNewClass(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            _foundationPermissionsLogic
                .Setup(e => e.CanCreateEducationCycle(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            var classResp = await _foundationCommands.AddNewClass(classCommand);
            _foundationQueriesRepository
                .Setup(e => e.GetClassByGuid(It.IsAny<Guid>()))
                .ReturnsAsync(new ClassDto() { Guid = classResp.Response });
            var resp1 = await _foundationCommands.AddNewEducationCycle(educationCycleCommand);

            var command = new EducationCycleConfigurationCommand()
            {
                EducationCycleGuid = _db.Service.EducationCycles!.First().Guid,
                DateSince = new DateTime(2022, 12, 01),
                DateUntil = new DateTime(2022, 12, 30),
                Stages = new List<EducationCycleConfigurationStageCommand>(){
                    new EducationCycleConfigurationStageCommand(){
                        EducationCycleStageGuid = _db.Service.EducationCycleSteps!.First().Guid,
                        Order = 0,
                        DateSince = new DateTime(2022, 12, 01),
                        DateUntil = new DateTime(2022, 12, 10),
                        Subjects = new List<EducationCycleConfigurationSubjectCommand>(){
                            new EducationCycleConfigurationSubjectCommand(){
                                EducationCycleStageSubjectGuid= _db.Service.EducationCycleStepSubjects!.First().Guid
                            }
                        }
                    },
                }
            };

            var resp2 = await _foundationCommands.ConfigureEducationCycleForClass(classResp.Response, command);

            Assert.That(resp2.StatusCode, Is.EqualTo(200));
            Assert.That(_db.Service.EducationCycleInstances!.Include(e => e.EducationCycleStepInstances).First().EducationCycleStepInstances!.Any());
            Assert.That(_db.Service.EducationCycleStepInstances!.Include(e => e.EducationCycleStepSubjectInstances).First().EducationCycleStepSubjectInstances!.Any());
            Assert.That(_db.Service.EducationCycleInstances!.Include(e => e.EducationCycle).First().EducationCycle, Is.Not.Null);
            Assert.That(_db.Service.EducationCycleStepInstances!.Include(e => e.EducationCycleStep).First().EducationCycleStep, Is.Not.Null);
            Assert.That(_db.Service.EducationCycleStepSubjectInstances!.Include(e => e.EducationCycleStepSubject).First().EducationCycleStepSubject, Is.Not.Null);
        }
    }
}
