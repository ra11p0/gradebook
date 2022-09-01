using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Common.Foundation.Commands;

public interface IFoundationCommands
{
    Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand newStudentDto);
    Task<ResponseWithStatus<bool>> AddNewTeacher(NewTeacherCommand newTeacherCommand);
    Task<ResponseWithStatus<bool>> NewAdministrator(NewAdministratorCommand command);
    Task<ResponseWithStatus<bool>> NewAdministratorWithSchool(NewAdministratorCommand administratorCommand, NewSchoolCommand schoolCommand);
    Task<ResponseWithStatus<string, bool>> GenerateSystemInvitation(Guid personGuid, SchoolRoleEnum role);
    Task<ResponseWithStatus<bool>> AddPersonToSchool(Guid schoolGuid, Guid? personGuid = null);
    Task<ResponseWithStatus<bool>> ActivatePerson(string activationCode);
}
