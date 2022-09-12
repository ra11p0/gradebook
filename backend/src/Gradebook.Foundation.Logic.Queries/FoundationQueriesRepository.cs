using Dapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
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
        using (var cn = await GetOpenConnectionAsync())
        {
            return await cn.QueryAsync<StudentDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday CreatorGuid, Guid, UserGuid
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
            ", new
            {
                relatedPersonGuid
            });
        }
    }

    public async Task<IEnumerable<TeacherDto>> GetAllAccessibleTeachers(Guid relatedPersonGuid)
    {
        using (var cn = await GetOpenConnectionAsync())
        {
            return await cn.QueryAsync<TeacherDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid
                FROM Person
                LEFT JOIN PersonSchool AS PS
                    ON Guid = PS.PeopleGuid
                WHERE Discriminator = 'Teacher'
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
            ", new
            {
                relatedPersonGuid
            });
        }
    }

    public async Task<IEnumerable<StudentDto>> GetAllInactiveAccessibleStudents(Guid schoolGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<StudentDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid
                FROM Person
                LEFT JOIN PersonSchool AS PS
                    ON Guid = PS.PeopleGuid
                WHERE Discriminator = 'Student'
                    AND PS.SchoolsGuid = @schoolGuid
                    AND UserGuid IS NULL
            ", new
        {
            schoolGuid
        });
    }

    public async Task<ClassDto> GetClassByGuid(Guid guid)
    {
        using (var cn = await GetOpenConnectionAsync())
        {
            return await cn.QueryFirstOrDefaultAsync<ClassDto>(@"
                SELECT Name, CreatedDate, Description
                FROM Classes
                WHERE Guid like @guid
            ", new
            {
                guid
            });
        }
    }

    public async Task<IPagedList<ClassDto>> GetClassesInSchool(Guid schoolGuid, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<ClassDto>(@"
            SELECT Name, Description, CreatedDate, Guid
            FROM Classes
            WHERE IsDeleted = 0 
                AND SchoolGuid = @schoolGuid
            ORDER BY CreatedDate DESC
         ", new { schoolGuid }, pager);
    }

    public async Task<GroupDto> GetGroupByGuid(Guid guid)
    {
        using (var cn = await GetOpenConnectionAsync())
        {
            return await cn.QueryFirstOrDefaultAsync<GroupDto>(@"
                SELECT Name
                FROM Groups
                WHERE Guid like @guid
            ", new
            {
                guid
            });
        }
    }

    public async Task<InvitationDto> GetInvitationByActivationCode(string activationCode)
    {
        using (var cn = await GetOpenConnectionAsync())
        {
            return await cn.QueryFirstOrDefaultAsync<InvitationDto>(@"
                SELECT CreatedDate, ExprationDate, IsUsed, CreatorGuid, UsedDate, InvitedPersonGuid, SchoolRole, Guid
                FROM SystemInvitations
                WHERE InvitationCode like @activationCode
            ", new
            {
                activationCode = activationCode.Normalize()
            });
        }
    }

    public async Task<IEnumerable<InvitationDto>> GetInvitations(Guid personGuid)
    {
        using (var cn = await GetOpenConnectionAsync())
        {
            return await cn.QueryAsync<InvitationDto>(@"
                SELECT Guid, CreatedDate, ExprationDate, InvitationCode, IsUsed,
                    CreatorGuid, UsedDate, InvitedPersonGuid, SchoolRole
                FROM SystemInvitations
                WHERE CreatorGuid = @personGuid
            ", new
            {
                personGuid
            });
        }
    }

    public async Task<IPagedList<InvitationDto>> GetInvitationsToSchool(Guid schoolGuid, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<InvitationDto>(@"
                SELECT Guid, CreatedDate, ExprationDate, InvitationCode, IsUsed,
                    CreatorGuid, UsedDate, InvitedPersonGuid, SchoolRole, SchoolGuid
                FROM SystemInvitations
                WHERE SchoolGuid = @schoolGuid
                ORDER BY CreatedDate DESC
        ", new { schoolGuid }, pager);
    }

    public async Task<IEnumerable<PersonDto>> GetPeopleInSchool(Guid schoolGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<PersonDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, Guid
                FROM Person
                JOIN PersonSchool AS PS
                    ON PS.PeopleGuid = Guid
                WHERE PS.SchoolsGuid = @schoolGuid
            ", new
        {
            schoolGuid
        });
    }

    public async Task<PersonDto> GetPersonByGuid(Guid guid)
    {
        using (var cn = await GetOpenConnectionAsync())
        {
            return await cn.QueryFirstOrDefaultAsync<PersonDto>(@"
                SELECT Guid, Name, Surname, SchoolRole, Birthday, UserGuid
                FROM Person
                WHERE Guid = @guid
            ",
        new
        {
            guid
        });
        }
    }

    public async Task<Guid?> GetPersonGuidForUser(string userId)
    {
        using (var cn = await GetOpenConnectionAsync())
        {
            return await cn.QueryFirstOrDefaultAsync<Guid>(@"
                SELECT Guid 
                FROM Person
                WHERE UserGuid = @userId
            ",
            new
            {

                userId
            });
        }
    }

    public async Task<SchoolDto> GetSchoolByGuid(Guid guid)
    {
        using (var cn = await GetOpenConnectionAsync())
        {
            return await cn.QueryFirstOrDefaultAsync<SchoolDto>(@"
                SELECT Guid, Name, AddressLine1, AddressLine2, City, PostalCode
                FROM Schools
                WHERE Guid = @guid AND IsDeleted = 0
            ",
            new
            {
                guid
            });
        }
    }

    public async Task<IEnumerable<SchoolDto>> GetSchoolsForPerson(Guid personGuid)
    {
        using (var cn = await GetOpenConnectionAsync())
        {
            return await cn.QueryAsync<SchoolDto>(@"
                SELECT Guid, Name, AddressLine1, AddressLine2, City, PostalCode
                FROM Schools
                JOIN PersonSchool AS PS
                    ON PS.SchoolsGuid = Guid
                WHERE PS.PeopleGuid = @personGuid AND IsDeleted = 0
            ",
            new
            {
                personGuid
            });
        }
    }

    public async Task<StudentDto> GetStudentByGuid(Guid guid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<StudentDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid
                FROM Person
                WHERE Discriminator = 'Student'
                    AND Guid = @guid
            ", new
        {
            guid
        });
    }

    public async Task<IPagedList<StudentDto>> GetStudentsInSchool(Guid schoolGuid, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<StudentDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid
                FROM Person
                JOIN PersonSchool AS PS
                    ON PS.PeopleGuid = Guid
                WHERE PS.SchoolsGuid = @schoolGuid AND Discriminator = 'Student'
                ORDER BY Name, Surname
            ", new
        {
            schoolGuid
        }, pager);
    }

    public async Task<TeacherDto> GetTeacherByGuid(Guid guid)
    {
        using (var cn = await GetOpenConnectionAsync())
        {
            return await cn.QueryFirstOrDefaultAsync<TeacherDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid
                FROM Person
                WHERE Discriminator = 'Teacher'
                    AND Guid = @guid
            ", new
            {
                guid
            });
        }
    }
}
