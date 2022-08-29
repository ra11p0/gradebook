using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

namespace Gradebook.Foundation.Logic.Commands;

public interface IFoundationCommandsRepository : IBaseRepository
{
    Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand newStudentDto);
    Task<ResponseWithStatus<Guid, bool>> AddNewSchool(NewSchoolCommand command);
    Task<ResponseWithStatus<bool>> AddAdministratorToSchool(Guid administratorGuid, Guid schoolGuid);
    Task<ResponseWithStatus<Guid, bool>> AddNewAdministrator(NewAdministratorCommand command);
}
