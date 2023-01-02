using Dapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Logic.Queries.Repositories.Interfaces;

namespace Gradebook.Foundation.Logic.Queries.Repositories;

public partial class FoundationQueriesRepository : IFoundationQueriesPeopleRepository
{
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
    public async Task<PersonDto> GetPersonByGuid(Guid guid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<PersonDto>(@"
                SELECT Guid, Name, Surname, SchoolRole, Birthday, UserGuid, SchoolGuid, CurrentClassGuid AS 'ActiveClassGuid'
                FROM Person
                WHERE Guid = @guid
                    AND IsDeleted = 0
            ",
    new
    {
        guid
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
    public Task<IPagedList<PersonDto>> GetPeopleByGuids(IEnumerable<Guid> guids, Pager pager)
    {
        throw new NotImplementedException();
    }
}
