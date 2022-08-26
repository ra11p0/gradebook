using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

namespace Gradebook.Foundation.Common.Foundation.Commands;

public interface IFoundationCommands
{
    Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand newStudentDto);
}
