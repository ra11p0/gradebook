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

    public async Task<ResponseWithStatus<Guid, bool>> GetCurrentPersonGuid()
    {
        var userGuid = await _identityLogic.Service.CurrentUserId();
        if(!userGuid.Status) return new ResponseWithStatus<Guid, bool>(Guid.Empty, false, "Can't get current user guid");
        var personGuid = await GetPersonGuidForUser(userGuid.Response!);
        return personGuid;
    }

    public async Task<ResponseWithStatus<IEnumerable<PersonDto>, bool>> GetPeopleInSchool(Guid schoolGuid)
    {
        var resp = await Repository.GetPeopleInSchool(schoolGuid);
        return new ResponseWithStatus<IEnumerable<PersonDto>, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<Guid, bool>> GetPersonGuidForUser(string userId)
    {
        var resp = await Repository.GetPersonGuidForUser(userId);
        return new ResponseWithStatus<Guid, bool>(resp.Value, !(resp is null));
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
