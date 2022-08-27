using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

namespace Gradebook.Foundation.Logic.Commands;

public interface IFoundationCommandsRepository : IBaseRepository
{
    Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand newStudentDto);
    Task<ResponseWithStatus<bool>> ActivateAdministrator(ActivateAdministratorCommand command);
}
