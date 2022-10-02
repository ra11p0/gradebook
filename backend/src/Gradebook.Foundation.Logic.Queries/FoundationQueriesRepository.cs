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
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<StudentDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday CreatorGuid, Guid, UserGuid, SchoolGuid
                FROM Person
                WHERE Discriminator = 'Student'
                    AND IsDeleted = 0
                    AND 
                    (
                        CreatorGuid = @relatedPersonGuid
                        OR PS.SchoolsGuid IN
                            (
                                SELECT SchoolGuid
                                FROM Person
                                WHERE Guid = @relatedPersonGuid
                            )
                    )
            ", new
        {
            relatedPersonGuid
        });
    }

    public async Task<IEnumerable<TeacherDto>> GetAllAccessibleTeachers(Guid relatedPersonGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<TeacherDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, SchoolGuid
                FROM Person
                WHERE Discriminator = 'Teacher'
                    AND IsDeleted = 0
                    AND 
                    (
                        CreatorGuid = @relatedPersonGuid
                        OR SchoolGuid IN 
                            (
                                SELECT SchoolGuid
                                FROM PersonSchool
                                WHERE Guid = @relatedPersonGuid
                            )
                    )
            ", new
        {
            relatedPersonGuid
        });
    }

    public async Task<IEnumerable<StudentDto>> GetAllInactiveAccessibleStudents(Guid schoolGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<StudentDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid, SchoolGuid
                FROM Person
                WHERE Discriminator = 'Student'
                    AND SchoolGuid = @schoolGuid
                    AND UserGuid IS NULL
                    AND IsDeleted = 0
            ", new
        {
            schoolGuid
        });
    }

    public async Task<ClassDto> GetClassByGuid(Guid guid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<ClassDto>(@"
                SELECT Name, CreatedDate, Description, Guid
                FROM Classes
                WHERE Guid like @guid
                    AND IsDeleted = 0
            ", new
        {
            guid
        });
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
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<GroupDto>(@"
                SELECT Name
                FROM Groups
                WHERE Guid like @guid
                    AND IsDeleted = 0
            ", new
        {
            guid
        });
    }

    public async Task<InvitationDto> GetInvitationByActivationCode(string activationCode)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<InvitationDto>(@"
                SELECT SI.Guid, SI.CreatedDate, ExprationDate, InvitationCode, IsUsed,
                    SI.CreatorGuid, UsedDate, InvitedPersonGuid, SI.SchoolRole
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
                    SI.CreatorGuid, UsedDate, InvitedPersonGuid, SI.SchoolRole
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
                    SI.CreatorGuid, UsedDate, InvitedPersonGuid, SI.SchoolRole, P.SchoolGuid
                FROM SystemInvitations AS SI
                JOIN Person AS P
                    ON SI.InvitedPersonGuid = P.Guid
                WHERE P.SchoolGuid = @schoolGuid
                    AND SI.IsDeleted = 0
                    AND P.IsDeleted = 0
                ORDER BY CreatedDate DESC
        ", new { schoolGuid }, pager);
    }

    public async Task<IEnumerable<PersonDto>> GetPeopleInSchool(Guid schoolGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<PersonDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, Guid, SchoolGuid
                FROM Person
                WHERE SchoolGuid = @schoolGuid
                    AND IsDeleted = 0
            ", new
        {
            schoolGuid
        });
    }

    public async Task<IPagedList<TeacherDto>> GetTeachersInSchool(Guid schoolGuid, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<TeacherDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid, SchoolGuid
                FROM Person
                WHERE SchoolGuid = @schoolGuid 
                    AND Discriminator = 'Teacher'
                    AND IsDeleted = 0
                ORDER BY Name, Surname
            ", new
        {
            schoolGuid
        }, pager);
    }

    public async Task<IPagedList<PersonDto>> GetPeopleInSchool(Guid schoolGuid, string discriminator, string query, Pager pager)
    {
        var discriminatorQuery = String.IsNullOrEmpty(discriminator) ? null : @"
            Discriminator = @discriminator
        ";

        var queryQuery = String.IsNullOrEmpty(query) ? null : @"
            (CONCAT(Name, ' ', Surname) like @query) AND SchoolGuid = @schoolGuid
        ";
        var dataQuery = $@"
            SELECT Name, Surname, SchoolRole, Birthday, Guid, SchoolGuid
            FROM Person
            {(discriminatorQuery != null || queryQuery != null ? "WHERE" : null)}
            {discriminatorQuery}
            {(discriminatorQuery != null && queryQuery != null ? "AND" : null)}
            {queryQuery}
            ORDER BY Name, Surname
        ";
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<PersonDto>(dataQuery, new
        {
            schoolGuid,
            discriminator,
            query = $"%{query}%",
        }, pager);
    }

    public async Task<PersonDto> GetPersonByGuid(Guid guid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<PersonDto>(@"
                SELECT Guid, Name, Surname, SchoolRole, Birthday, UserGuid, SchoolGuid
                FROM Person
                WHERE Guid = @guid
                    AND IsDeleted = 0
            ",
    new
    {
        guid
    });
    }

    public async Task<Guid?> GetPersonGuidForUser(string userId, Guid schoolGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<Guid>(@"
                SELECT Guid 
                FROM Person
                WHERE UserGuid = @userId
                    AND IsDeleted = 0
                    AND SchoolGuid = @schoolGuid
            ",
        new
        {
            schoolGuid,
            userId
        });
    }

    public async Task<SchoolDto> GetSchoolByGuid(Guid guid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<SchoolDto>(@"
                SELECT Guid, Name, AddressLine1, AddressLine2, City, PostalCode
                FROM Schools
                WHERE Guid = @guid 
                    AND IsDeleted = 0
            ",
        new
        {
            guid
        });
    }

    public async Task<IEnumerable<SchoolDto>> GetSchoolsForUser(string userGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<SchoolDto>(@"
                SELECT Guid, Name, AddressLine1, AddressLine2, City, PostalCode
                FROM Schools
                WHERE Guid IN (
                    SELECT SchoolGuid FROM Person WHERE UserGuid = @userGuid 
                )
                    AND IsDeleted = 0
            ",
        new
        {
            userGuid
        });
    }

    public async Task<StudentDto> GetStudentByGuid(Guid guid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<StudentDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid, SchoolGuid
                FROM Person
                WHERE Discriminator = 'Student'
                    AND Guid = @guid
                    AND IsDeleted = 0
            ", new
        {
            guid
        });
    }

    public async Task<IPagedList<StudentDto>> GetStudentsInClass(Guid classGuid, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<StudentDto>(@"
            SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid, SchoolGuid
            FROM Person 
            WHERE Discriminator = 'Student'
                AND Guid IN (
                SELECT StudentsGuid FROM ClassStudent WHERE ClassesGuid = @classGuid
            )
        ", new
        {
            classGuid
        }, pager);
    }

    public async Task<IPagedList<StudentDto>> GetStudentsInSchool(Guid schoolGuid, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<StudentDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid, SchoolGuid
                FROM Person
                WHERE SchoolGuid = @schoolGuid 
                    AND Discriminator = 'Student'
                    AND IsDeleted = 0
                ORDER BY Name, Surname
            ", new
        {
            schoolGuid
        }, pager);
    }

    public async Task<TeacherDto> GetTeacherByGuid(Guid guid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<TeacherDto>(@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid, SchoolGuid
                FROM Person
                WHERE Discriminator = 'Teacher'
                    AND Guid = @guid
                    AND IsDeleted = 0
            ", new
        {
            guid
        });
    }

    public async Task<IPagedList<TeacherDto>> GetTeachersInClass(Guid classGuid, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<TeacherDto>(@"
            SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid, SchoolGuid
            FROM Person 
            WHERE Discriminator = 'Teacher'
                AND Guid IN (
                SELECT OwnersTeachersGuid FROM ClassTeacher WHERE OwnedClassesGuid = @classGuid
            )
        ", new
        {
            classGuid
        }, pager);
    }

    public async Task<bool> IsUserActive(string userGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<bool>(@"
                SELECT EXISTS (
                    SELECT * 
                    FROM Person 
                    WHERE UserGuid = @userGuid
                )
            ", new
        {
            userGuid
        });
    }
}
