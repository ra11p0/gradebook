using Dapper;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries.Repositories;

public partial class FoundationQueriesRepository
{
    public async Task<ClassDto?> GetClassByGuid(Guid guid)
        => (await GetClassesByGuids(guid.AsEnumerable())).FirstOrDefault();
    public async Task<IEnumerable<ClassDto>> GetClassesByGuids(IEnumerable<Guid> guids)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<ClassDto>(@"
                SELECT Name, CreatedDate, Description, Guid, SchoolGuid
                FROM Classes
                WHERE Guid IN (@guids)
                    AND IsDeleted = 0
            ", new
        {
            guids
        });
    }
}
