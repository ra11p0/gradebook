using Dapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Logic.Queries.Repositories.Interfaces;

namespace Gradebook.Foundation.Logic.Queries.Repositories;

public partial class FoundationQueriesRepository : IFoundationQueriesInvitationsRepository
{
    public async Task<InvitationDto> GetInvitationByActivationCode(string activationCode)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<InvitationDto>(@"
                SELECT SI.Guid, SI.CreatedDate, ExprationDate, InvitationCode, IsUsed,
                    SI.CreatorGuid, UsedDate, InvitedPersonGuid, P.SchoolRole
                FROM SystemInvitations AS SI
                JOIN Person AS P
                    ON SI.InvitedPersonGuid = P.Guid
                WHERE InvitationCode like @activationCode
                    AND SI.IsDeleted = 0
                    AND P.IsDeleted = 0
            ", new
        {
            activationCode = activationCode.Normalize()
        });
    }

    public async Task<IEnumerable<InvitationDto>> GetInvitations(Guid personGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<InvitationDto>(@"
                SELECT SI.Guid, SI.CreatedDate, ExprationDate, InvitationCode, IsUsed,
                    SI.CreatorGuid, UsedDate, InvitedPersonGuid, P.SchoolRole
                FROM SystemInvitations AS SI
                JOIN Person AS P
                    ON SI.InvitedPersonGuid = P.Guid
                WHERE CreatorGuid = @personGuid
                    AND SI.IsDeleted = 0
                    AND P.IsDeleted = 0
            ", new
        {
            personGuid
        });
    }

    public async Task<IPagedList<InvitationDto>> GetInvitationsToSchool(Guid schoolGuid, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<InvitationDto>(@"
                SELECT SI.Guid, SI.CreatedDate, ExprationDate, InvitationCode, IsUsed,
                    SI.CreatorGuid, UsedDate, InvitedPersonGuid, P.SchoolGuid, P.SchoolRole
                FROM SystemInvitations AS SI
                JOIN Person AS P
                    ON SI.InvitedPersonGuid = P.Guid
                WHERE P.SchoolGuid = @schoolGuid
                    AND SI.IsDeleted = 0
                    AND P.IsDeleted = 0
                ORDER BY CreatedDate DESC
        ", new { schoolGuid }, pager);
    }

}
