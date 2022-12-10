using Dapper;
using DbExtensions;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Database;

namespace Gradebook.Foundation.Logic.Queries.Repositories;

public partial class FoundationQueriesRepository : BaseRepository<FoundationDatabaseContext>, IFoundationQueriesRepository
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

    public async Task<IPagedList<ClassDto>> GetClassesInSchool(Guid schoolGuid, Pager pager, string? query = "")
    {
        var builder = new SqlBuilder();
        builder.SELECT("Name, Description, CreatedDate, Guid");
        builder.FROM("Classes");
        builder.WHERE("IsDeleted = 0");
        builder.WHERE("SchoolGuid = @schoolGuid");
        if (!string.IsNullOrEmpty(query))
            builder.WHERE("Name like @query");
        builder.ORDER_BY("CreatedDate DESC");

        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<ClassDto>(builder.ToString(), new { schoolGuid, query = $"%{query}%" }, pager);
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
        var builder = new SqlBuilder();
        builder.SELECT("Name, Surname, SchoolRole, Birthday, Guid, SchoolGuid");
        builder.FROM("Person");
        builder.WHERE("SchoolGuid = @schoolGuid");
        if (!string.IsNullOrEmpty(query))
            builder.WHERE("CONCAT(Name, ' ', Surname) like @query");
        if (!string.IsNullOrEmpty(discriminator))
            builder.WHERE("Discriminator = @discriminator");
        builder.WHERE("IsDeleted = 0");
        builder.ORDER_BY("Name, Surname");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<PersonDto>(builder.ToString(), new
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
            ORDER BY Name, Surname
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

    public async Task<IEnumerable<StudentDto>> GetAllStudentsInClass(Guid classGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<StudentDto>(@"
            SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid, SchoolGuid
            FROM Person 
            WHERE Discriminator = 'Student'
                AND Guid IN (
                SELECT StudentsGuid FROM ClassStudent WHERE ClassesGuid = @classGuid
            )
        ", new
        {
            classGuid
        });
    }

    public async Task<IEnumerable<TeacherDto>> GetAllTeachersInClass(Guid classGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<TeacherDto>(@"
            SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid, SchoolGuid
            FROM Person 
            WHERE Discriminator = 'Teacher'
                AND Guid IN (
                SELECT OwnersTeachersGuid FROM ClassTeacher WHERE OwnedClassesGuid = @classGuid
            )
        ", new
        {
            classGuid
        });
    }

    public async Task<bool> IsClassOwner(Guid classGuid, Guid personGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<bool>(@"
                SELECT EXISTS (
                    SELECT * 
                    FROM Person 
                    WHERE Guid IN (
                        SELECT OwnersTeachersGuid FROM ClassTeacher WHERE OwnedClassesGuid = @classGuid
                    )
                    AND Guid = @personGuid
                )
            ", new
        {
            classGuid,
            personGuid
        });
    }

    public async Task<IPagedList<StudentDto>> SearchStudentsCandidatesToClassWithCurrent(Guid classGuid, string query, Pager pager)
    {
        var builder = new SqlBuilder();
        builder.SELECT("Name, Surname, SchoolRole, Birthday, Guid, SchoolGuid");
        builder.FROM("Person AS ps");
        builder.LEFT_JOIN("ClassStudent AS cs ON ps.Guid = cs.StudentsGuid");
        builder.WHERE("(cs.ClassesGuid = @classGuid OR cs.ClassesGuid IS null)");
        builder.WHERE("Discriminator = 'Student'");
        builder.WHERE("IsDeleted = 0");
        builder.WHERE(@"SchoolGuid = (
            SELECT SchoolGuid FROM Classes WHERE Guid = @classGuid
        )");
        if (!string.IsNullOrEmpty(query))
            builder.WHERE("CONCAT(Name, ' ', Surname) like @query");
        builder.ORDER_BY("Name, Surname");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<StudentDto>(builder.ToString(), new
        {
            classGuid,
            query = $"%{query}%",
        }, pager);
    }

    public async Task<bool> IsStudentInAnyClass(Guid studentGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstAsync<bool>(@"
                SELECT EXISTS (
                    SELECT * 
                    FROM Person AS ps
                    JOIN ClassStudent AS cs
                        ON cs.StudentsGuid = ps.Guid
                    WHERE ps.Guid = @studentGuid
                )
            ", new
        {
            studentGuid
        });
    }

    public async Task<IPagedList<ClassDto>> GetClassesForPerson(Guid personGuid, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<ClassDto>(@"
            SELECT Name, Description, CreatedDate, Guid
            FROM Classes
            WHERE IsDeleted = 0 
                AND Guid IN (
                    SELECT OwnedClassesGuid AS 'Guid'
                    FROM ClassTeacher
                    WHERE OwnersTeachersGuid = @personGuid
                    UNION
                    SELECT ClassesGuid AS 'Guid'
                    FROM ClassStudent
                    WHERE StudentsGuid = @personGuid
                )
            ORDER BY CreatedDate DESC
         ", new { personGuid }, pager);
    }

    public async Task<SubjectDto> GetSubject(Guid subjectGuid)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryFirstOrDefaultAsync<SubjectDto>(@"
            SELECT Name, SchoolGuid, Guid
            FROM Subjects
            WHERE IsDeleted = 0 
                AND Guid = @subjectGuid
            ORDER BY Name DESC
         ", new { subjectGuid });
    }

    public async Task<IPagedList<SubjectDto>> GetSubjectsForSchool(Guid schoolGuid, Pager pager, string query)
    {
        var builder = new SqlBuilder();
        builder.SELECT("Name, SchoolGuid, Guid");
        builder.FROM("Subjects");
        builder.WHERE("IsDeleted = 0");
        builder.WHERE("SchoolGuid = @schoolGuid");

        if (!string.IsNullOrEmpty(query))
            builder.WHERE("Name like @query");
        builder.ORDER_BY("Name");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<SubjectDto>(builder.ToString(), new { schoolGuid, query = $"%{query}%", }, pager);
    }

    public async Task<IPagedList<TeacherDto>> GetTeachersForSubject(Guid subjectGuid, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<TeacherDto>(@"
            SELECT Name, Surname, SchoolRole, Birthday, CreatorGuid, Guid, UserGuid, SchoolGuid
            FROM Person
            WHERE IsDeleted = 0 
                AND Discriminator = 'Teacher'
                AND Guid IN (
                    SELECT TeachersGuid 
                    FROM SubjectTeacher
                    WHERE SubjectsGuid = @subjectGuid
                )
            ORDER BY Name, Surname
         ", new { subjectGuid }, pager);
    }

    public async Task<IPagedList<SubjectDto>> GetSubjectsForTeacher(Guid teacherGuid, Pager pager)
    {
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<SubjectDto>(@"
            SELECT Name, SchoolGuid, Guid
            FROM Subjects
            WHERE Guid IN (
                SELECT st.SubjectsGuid
                FROM SubjectTeacher st
                JOIN Person ps
                ON ps.Guid = st.TeachersGuid 
                    AND ps.Discriminator = 'Teacher'
                WHERE ps.Guid = @teacherGuid
                    AND ps.IsDeleted = 0 
            )
            ORDER BY Name
         ", new { teacherGuid }, pager);
    }

    public async Task<IPagedList<EducationCycleDto>> GetEducationCyclesInSchool(Guid schoolGuid, Pager pager)
    {
        var builder = new SqlBuilder();
        builder.SELECT("Name, SchoolGuid, Guid, CreatedDate, CreatorGuid");
        builder.FROM("EducationCycles");
        builder.WHERE("IsDeleted = 0");
        builder.WHERE("SchoolGuid = @schoolGuid");
        builder.ORDER_BY("Name");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryPagedAsync<EducationCycleDto>(builder.ToString(), new { schoolGuid }, pager);
    }

    public async Task<EducationCycleExtendedDto?> GetEducationCycle(Guid educationCycleGuid)
        => (await GetEducationCyclesByGuids(educationCycleGuid.AsEnumerable(), new Pager(0))).FirstOrDefault();

    public async Task<IEnumerable<EducationCycleStepDto>> GetStepsForEducationCycle(Guid educationCycleGuid)
    {
        var builder = new SqlBuilder();
        builder.SELECT("Guid, `Order`, Name");
        builder.FROM("EducationCycleSteps");
        builder.WHERE("IsDeleted = 0");
        builder.WHERE("EducationCycleGuid = @educationCycleGuid");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<EducationCycleStepDto>(builder.ToString(), new { educationCycleGuid });
    }

    public async Task<IEnumerable<EducationCycleStepSubjectDto>> GetStepsSubjectsForEducationCycleStep(Guid educationCycleStepGuid)
    {
        var builder = new SqlBuilder();
        builder.SELECT("Guid, SubjectGuid, HoursInStep, IsMandatory, GroupsAllowed");
        builder.FROM("EducationCycleStepSubjects");
        builder.WHERE("IsDeleted = 0");
        builder.WHERE("EducationCycleStepGuid = @educationCycleStepGuid");
        using var cn = await GetOpenConnectionAsync();
        return await cn.QueryAsync<EducationCycleStepSubjectDto>(builder.ToString(), new { educationCycleStepGuid });
    }
}
