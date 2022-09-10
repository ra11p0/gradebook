using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Logic.Commands;

public class FoundationCommandsRepositoryCached : BaseRepositoryCached<FoundationCommandsRepository, object>, IFoundationCommandsRepository
{
    public FoundationCommandsRepositoryCached(FoundationCommandsRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public Task<StatusResponse<bool>> AddAdministratorToSchool(Guid administratorGuid, Guid schoolGuid)
        => Base.AddAdministratorToSchool(administratorGuid, schoolGuid);

    public Task<ResponseWithStatus<Guid, bool>> AddNewAdministrator(NewAdministratorCommand command)
        => Base.AddNewAdministrator(command);

    public Task<ResponseWithStatus<Guid, bool>> AddNewSchool(NewSchoolCommand command)
        => Base.AddNewSchool(command);

    public Task<StatusResponse<bool>> AddNewStudent(NewStudentCommand newStudentDto)
        => Base.AddNewStudent(newStudentDto);

    public Task<StatusResponse<bool>> AddNewTeacher(NewTeacherCommand newTeacherDto)
        => Base.AddNewTeacher(newTeacherDto);

    public Task<StatusResponse<bool>> AddPersonToSchool(Guid schoolGuid, Guid personGuid)
        => Base.AddPersonToSchool(schoolGuid, personGuid);

    public Task<StatusResponse<bool>> AssignUserToAdministrator(string userId, Guid personGuid)
        => Base.AssignUserToAdministrator(userId, personGuid);

    public Task<StatusResponse<bool>> AssignUserToStudent(string userId, Guid personGuid)
        => Base.AssignUserToStudent(userId, personGuid);

    public Task<StatusResponse<bool>> AssignUserToTeacher(string userId, Guid personGuid)
        => Base.AssignUserToTeacher(userId, personGuid);

    public Task<string?> GenerateSystemInvitation(Guid invitedPersonGuid, Guid invitingPersonGuid, SchoolRoleEnum role)
        => Base.GenerateSystemInvitation(invitedPersonGuid, invitingPersonGuid, role);
    public void BeginTransaction()
    {
        Base.BeginTransaction();
    }

    public void CommitTransaction()
    {
        Base.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        Base.RollbackTransaction();
    }

    public void SaveChanges()
    {
        Base.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return Base.SaveChangesAsync();
    }

    public Task<StatusResponse<bool>> UseInvitation(UseInvitationCommand command)
        => Base.UseInvitation(command);
}
