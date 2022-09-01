using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Logic.Commands;

public interface IFoundationCommandsRepository : IBaseRepository
{
    Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand newStudentDto);
    Task<ResponseWithStatus<bool>> AddNewTeacher(NewTeacherCommand newTeacherDto);
    Task<ResponseWithStatus<Guid, bool>> AddNewSchool(NewSchoolCommand command);
    Task<ResponseWithStatus<bool>> AddAdministratorToSchool(Guid administratorGuid, Guid schoolGuid);
    Task<ResponseWithStatus<Guid, bool>> AddNewAdministrator(NewAdministratorCommand command);
    Task<string?> GenerateSystemInvitation(Guid invitedPersonGuid, Guid invitingPersonGuid, SchoolRoleEnum role);
    Task<ResponseWithStatus<bool>> AddPersonToSchool(Guid schoolGuid, Guid personGuid);
    Task<ResponseWithStatus<bool>> AssignUserToStudent(string userId, Guid personGuid);
    Task<ResponseWithStatus<bool>> AssignUserToAdministrator(string userId, Guid personGuid);
    Task<ResponseWithStatus<bool>> AssignUserToTeacher(string userId, Guid personGuid);
    Task<ResponseWithStatus<bool>> UseInvitation(UseInvitationCommand command);
}
