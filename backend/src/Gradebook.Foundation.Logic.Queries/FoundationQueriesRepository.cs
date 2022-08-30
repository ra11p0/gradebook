using Dapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Database;

namespace Gradebook.Foundation.Logic.Queries;

public class FoundationQueriesRepository : BaseRepository<FoundationDatabaseContext>, IFoundationQueriesRepository
{
    public FoundationQueriesRepository(FoundationDatabaseContext context) : base(context)
    {
    }

    public async Task<IEnumerable<StudentDto>> GetAllAccessibleStudents(Guid relatedPersonGuid)
    {
        using(var cn = await GetOpenConnectionAsync()){
            return await cn.QueryAsync<StudentDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, ClassGuid, GroupGuid, CreatorGuid, Guid
                FROM Person
                LEFT JOIN PersonSchool AS PS
                    ON Guid = PS.PeopleGuid
                WHERE Discriminator = 'Student'
                    AND 
                    (
                        CreatorGuid = @relatedPersonGuid
                        OR PS.SchoolsGuid IN 
                            (
                                SELECT SchoolsGuid
                                FROM PersonSchool
                                WHERE PeopleGuid = @relatedPersonGuid
                            )
                    )
            ", new {
                relatedPersonGuid
            });
        }
    }

    public async Task<IEnumerable<InvitationDto>> GetInvitations(Guid personGuid)
    {
        using (var cn = await GetOpenConnectionAsync()){
            return await cn.QueryAsync<InvitationDto>(@"
                SELECT Guid, CreatedDate, ExprationDate, InvitationCode, IsUsed,
                    CreatorGuid, UsedDate, InvitedPersonGuid, SchoolRole
                FROM SystemInvitations
                WHERE CreatorGuid = @personGuid
            ", new {
                personGuid
            });
        }
    }

    public async Task<IEnumerable<PersonDto>> GetPeopleInSchool(Guid schoolGuid)
    {
        using (var cn = await GetOpenConnectionAsync()){
            return await cn.QueryAsync<PersonDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, Guid
                FROM Person
                JOIN PersonSchool AS PS
                    ON PS.PeopleGuid = Guid
                WHERE PS.SchoolsGuid = @schoolGuid
            ", new {
                schoolGuid
            });
        }
    }

    public async Task<Guid?> GetPersonGuidForUser(string userId)
    {
        using (var cn = await GetOpenConnectionAsync()){
            return await cn.QueryFirstOrDefaultAsync<Guid>(@"
                SELECT Guid 
                FROM Person
                WHERE UserGuid = @userId
            ",
            new{
                userId
            });
        }
    }

    public async Task<IEnumerable<SchoolDto>> GetSchoolsForPerson(Guid personGuid)
    {
        using (var cn = await GetOpenConnectionAsync()){
            return await cn.QueryAsync<SchoolDto>(@"
                SELECT Guid, Name, Address1, Address2, City, PostalCode
                FROM Schools
                JOIN PersonSchool AS PS
                    ON PS.SchoolsGuid = Guid
                WHERE PS.PeopleGuid = @personGuid
            ",
            new{
                personGuid
            });
        }
    }
}
