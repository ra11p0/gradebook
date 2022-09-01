using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Logic.Commands;

public class FoundationCommandsRepositoryCached : BaseRepositoryCached<FoundationCommandsRepository, object>, IFoundationCommandsRepository
{
    public FoundationCommandsRepositoryCached(FoundationCommandsRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public Task<ResponseWithStatus<bool>> AddAdministratorToSchool(Guid administratorGuid, Guid schoolGuid)
        => Base.AddAdministratorToSchool(administratorGuid, schoolGuid);

    public Task<ResponseWithStatus<Guid, bool>> AddNewAdministrator(NewAdministratorCommand command)
        => Base.AddNewAdministrator(command);

    public Task<ResponseWithStatus<Guid, bool>> AddNewSchool(NewSchoolCommand command)
        => Base.AddNewSchool(command);

    public Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand newStudentDto)
        => Base.AddNewStudent(newStudentDto);

    public Task<ResponseWithStatus<bool>> AddNewTeacher(NewTeacherCommand newTeacherDto)
        => Base.AddNewTeacher(newTeacherDto);

    public Task<ResponseWithStatus<bool>> AddPersonToSchool(Guid schoolGuid, Guid personGuid)
        => Base.AddPersonToSchool(schoolGuid, personGuid);

    public Task<ResponseWithStatus<bool>> AssignUserToAdministrator(string userId, Guid personGuid)
        => Base.AssignUserToAdministrator(userId, personGuid);

    public Task<ResponseWithStatus<bool>> AssignUserToStudent(string userId, Guid personGuid)
        => Base.AssignUserToStudent(userId, personGuid);

    public Task<ResponseWithStatus<bool>> AssignUserToTeacher(string userId, Guid personGuid)
        => Base.AssignUserToTeacher(userId, personGuid);

    public Task<string?> GenerateSystemInvitation(Guid invitedPersonGuid, Guid invitingPersonGuid, SchoolRoleEnum role)
        => Base.GenerateSystemInvitation(invitedPersonGuid, invitingPersonGuid, role);

    public void SaveChanges()
    {
        Base.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return Base.SaveChangesAsync();
    }

    public Task<ResponseWithStatus<bool>> UseInvitation(UseInvitationCommand command)
        => Base.UseInvitation(command);
}
