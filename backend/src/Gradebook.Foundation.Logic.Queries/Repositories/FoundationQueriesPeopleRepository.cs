using Dapper;
using DbExtensions;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Common.Foundation.Models;
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
                WHERE SchoolRole = {(int)SchoolRoleEnum.Student}
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
                WHERE SchoolRole = {(int)SchoolRoleEnum.Teacher}
                    AND Guid = @guid
                    AND IsDeleted = 0
            ", new
        {
            guid
        });
    }
    public async Task<IPagedList<PersonDto>> GetPeopleByGuids(IEnumerable<Guid> guids, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<PersonDto>(@"
                SELECT Guid, Name, Surname, SchoolRole, Birthday, UserGuid, SchoolGuid, CurrentClassGuid AS 'ActiveClassGuid'
                FROM Person
                WHERE Guid IN @guids
                    AND IsDeleted = 0
                ORDER BY Name, Surname
            ", new { guids }, pager);
    }
    public async Task<IPagedList<PersonDto>> SearchPeople(PeoplePickerData pickerData, Pager pager)
    {
        var builder = new SqlBuilder();
        builder.SELECT("Guid, Name, Surname, SchoolRole, Birthday, UserGuid, SchoolGuid, CurrentClassGuid AS 'ActiveClassGuid'");
        builder.FROM("Person");
        builder.WHERE("SchoolGuid = @SchoolGuid");
        if (!pickerData.IncludeDeleted)
            builder.WHERE("IsDeleted = 0");
        if (!string.IsNullOrEmpty(pickerData.Query))
        {
            pickerData.Query = $"%{pickerData.Query}%";
            builder.WHERE("CONCAT(Name, ' ', Surname) like @Query");
        }
        if (pickerData.ActiveClassGuid.HasValue)
            builder.WHERE("CurrentClassGuid = @ActiveClassGuid");
        if (pickerData.BirthdaySince.HasValue)
            builder.WHERE("Birthday > @BirthdaySince");
        if (pickerData.BirthdayUntil.HasValue)
            builder.WHERE("Birthday < @BirthdayUntil");
        if (pickerData.SchoolRole != 0)
            builder.WHERE("SchoolRole = @SchoolRole");
        if (pickerData.OnlyInactive)
            builder.WHERE("UserGuid is null");
        builder.ORDER_BY("CONCAT(Name, ' ', Surname) ");

        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<PersonDto>(builder.ToString(), pickerData, pager);
    }
    public async Task<AdminDto> GetAdminByGuid(Guid guid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<AdminDto>($@"
                SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid, SchoolGuid
                FROM Person
                WHERE SchoolRole = {(int)SchoolRoleEnum.Admin}
                    AND Guid = @guid
                    AND IsDeleted = 0
            ", new
        {
            guid
        });
    }
}
