using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries;

public class FoundationQueriesRepositoryCached : BaseRepositoryCached<FoundationQueriesRepository, object>, IFoundationQueriesRepository
{
    public FoundationQueriesRepositoryCached(FoundationQueriesRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public Task<IEnumerable<StudentDto>> GetAllAccessibleStudents(Guid relatedPersonGuid)
        => Base.GetAllAccessibleStudents(relatedPersonGuid);

    public Task<IEnumerable<TeacherDto>> GetAllAccessibleTeachers(Guid relatedPersonGuid)
        => Base.GetAllAccessibleTeachers(relatedPersonGuid);

    public Task<IEnumerable<StudentDto>> GetAllInactiveAccessibleStudents(Guid schoolGuid)
        => Base.GetAllInactiveAccessibleStudents(schoolGuid);

    public Task<ClassDto> GetClassByGuid(Guid guid)
        => Base.GetClassByGuid(guid);

    public Task<GroupDto> GetGroupByGuid(Guid guid)
        => Base.GetGroupByGuid(guid);

    public Task<InvitationDto> GetInvitationByActivationCode(string activationCode)
        => Base.GetInvitationByActivationCode(activationCode);

    public Task<IEnumerable<InvitationDto>> GetInvitations(Guid personGuid)
        => Base.GetInvitations(personGuid);

    public Task<IEnumerable<PersonDto>> GetPeopleInSchool(Guid schoolGuid)
        => Base.GetPeopleInSchool(schoolGuid);

    public Task<PersonDto> GetPersonByGuid(Guid guid)
        => Base.GetPersonByGuid(guid);

    public Task<Guid?> GetPersonGuidForUser(string userId)
        => Base.GetPersonGuidForUser(userId);

    public Task<IEnumerable<SchoolDto>> GetSchoolsForPerson(Guid personGuid)
        => Base.GetSchoolsForPerson(personGuid);

    public Task<StudentDto> GetStudentByGuid(Guid guid)
        => Base.GetStudentByGuid(guid);

    public Task<TeacherDto> GetTeacherByGuid(Guid guid)
        => Base.GetTeacherByGuid(guid);

    public void BeginTransaction()
        => Base.BeginTransaction();

    public void CommitTransaction()
        => Base.CommitTransaction();

    public void RollbackTransaction()
        => Base.RollbackTransaction();

    public void SaveChanges()
        => Base.SaveChanges();

    public Task SaveChangesAsync()
        => Base.SaveChangesAsync();

    public Task<SchoolDto> GetSchoolByGuid(Guid guid)
        => Base.GetSchoolByGuid(guid);

    public Task<IPagedList<StudentDto>> GetStudentsInSchool(Guid schoolGuid, Pager pager)
        => Base.GetStudentsInSchool(schoolGuid, pager);

    public Task<IPagedList<InvitationDto>> GetInvitationsToSchool(Guid schoolGuid, Pager pager)
        => Base.GetInvitationsToSchool(schoolGuid, pager);

    public Task<IPagedList<ClassDto>> GetClassesInSchool(Guid schoolGuid, Pager pager)
        => Base.GetClassesInSchool(schoolGuid, pager);

    public Task<IPagedList<StudentDto>> GetStudentsInClass(Guid schoolGuid, Pager pager)
        => Base.GetStudentsInClass(schoolGuid, pager);

    public Task<IPagedList<TeacherDto>> GetTeachersInClass(Guid schoolGuid, Pager pager)
        => Base.GetTeachersInClass(schoolGuid, pager);

    public Task<IPagedList<PersonDto>> GetPeopleInSchool(Guid schoolGuid, string schoolRole, string query, Pager pager)
        => Base.GetPeopleInSchool(schoolGuid, schoolRole, query, pager);
}
