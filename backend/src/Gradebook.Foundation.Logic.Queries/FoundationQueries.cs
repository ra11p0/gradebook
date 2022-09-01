using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;

namespace Gradebook.Foundation.Logic.Queries;

public class FoundationQueries : BaseLogic<IFoundationQueriesRepository>, IFoundationQueries
{
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    public FoundationQueries(IFoundationQueriesRepository repository, IServiceProvider serviceProvider) : base(repository)
    {
        _identityLogic = serviceProvider.GetResolver<IIdentityLogic>();
    }

    public async Task<ResponseWithStatus<IEnumerable<StudentDto>, bool>> GetAllAccessibleStudents()
    {
        var relatedPersonGuid = await GetCurrentPersonGuid();
        if(!relatedPersonGuid.Status) return new ResponseWithStatus<IEnumerable<StudentDto>, bool>(null, false, "Cannot recognise current person");
        var students = await Repository.GetAllAccessibleStudents(relatedPersonGuid.Response);
        return new ResponseWithStatus<IEnumerable<StudentDto>, bool>(students, true);
    }

    public async Task<ResponseWithStatus<IEnumerable<TeacherDto>, bool>> GetAllAccessibleTeachers()
    {
        var relatedPersonGuid = await GetCurrentPersonGuid();
        if(!relatedPersonGuid.Status) return new ResponseWithStatus<IEnumerable<TeacherDto>, bool>(null, false, "Cannot recognise current person");
        var teachers = await Repository.GetAllAccessibleTeachers(relatedPersonGuid.Response);
        return new ResponseWithStatus<IEnumerable<TeacherDto>, bool>(teachers, true);
    }

    public async Task<ResponseWithStatus<Guid, bool>> GetCurrentPersonGuid()
    {
        var userGuid = await _identityLogic.Service.CurrentUserId();
        if(!userGuid.Status) return new ResponseWithStatus<Guid, bool>(Guid.Empty, false, "Can't get current user guid");
        var personGuid = await GetPersonGuidForUser(userGuid.Response!);
        return personGuid;
    }

    public async Task<ResponseWithStatus<InvitationDto, bool>> GetInvitationByActivationCode(string activationCode)
    {
        var invitation = await Repository.GetInvitationByActivationCode(activationCode);
        if(invitation is null) return new ResponseWithStatus<InvitationDto, bool>(invitation, false, "Invitation does not exist");
        return new ResponseWithStatus<InvitationDto, bool>(invitation, true);
    }

    public async Task<ResponseWithStatus<IEnumerable<InvitationDto>, bool>> GetInvitations(Guid personGuid)
    {
        var resp = await Repository.GetInvitations(personGuid);
        return new ResponseWithStatus<IEnumerable<InvitationDto>, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<IEnumerable<InvitationDto>, bool>> GetInvitations()
    {
        var currentPersonGuid = await GetCurrentPersonGuid();
        if(!currentPersonGuid.Status) return new ResponseWithStatus<IEnumerable<InvitationDto>, bool>(default, false, currentPersonGuid.Message);
        return await GetInvitations(currentPersonGuid.Response);
    }

    public async Task<ResponseWithStatus<IEnumerable<PersonDto>, bool>> GetPeopleInSchool(Guid schoolGuid)
    {
        var resp = await Repository.GetPeopleInSchool(schoolGuid);
        return new ResponseWithStatus<IEnumerable<PersonDto>, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<PersonDto, bool>> GetPersonByGuid(Guid guid)
    {
        var resp = await Repository.GetPersonByGuid(guid);
        if(resp is null) return new ResponseWithStatus<PersonDto, bool>(null, false, "Person does not exist");
        return new ResponseWithStatus<PersonDto, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<Guid, bool>> GetPersonGuidForUser(string userId)
    {
        var resp = await Repository.GetPersonGuidForUser(userId);
        return new ResponseWithStatus<Guid, bool>(resp.Value, resp != Guid.Empty);
    }

    public async Task<ResponseWithStatus<IEnumerable<SchoolDto>, bool>> GetSchoolsForPerson(Guid personGuid)
    {
        var resp = await Repository.GetSchoolsForPerson(personGuid);
        return new ResponseWithStatus<IEnumerable<SchoolDto>, bool>(resp, true);
    }

    public Task<InvitationDto> GetStudentInvitationByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }
}
