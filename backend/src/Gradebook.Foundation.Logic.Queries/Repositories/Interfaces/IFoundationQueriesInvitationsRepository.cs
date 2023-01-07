using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries.Repositories.Interfaces;

public interface IFoundationQueriesInvitationsRepository
{
    Task<IEnumerable<InvitationDto>> GetInvitations(Guid personGuid);
    Task<IPagedList<InvitationDto>> GetInvitationsToSchool(Guid schoolGuid, Pager pager);
    Task<InvitationDto> GetInvitationByActivationCode(string activationCode);
}
