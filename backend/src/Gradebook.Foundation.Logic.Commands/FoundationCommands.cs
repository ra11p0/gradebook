using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

namespace Gradebook.Foundation.Logic.Commands;

public class FoundationCommands : BaseLogic<IFoundationCommandsRepository>, IFoundationCommands
{
    public FoundationCommands(IFoundationCommandsRepository repository) : base(repository)
    {
    }

    public async Task<ResponseWithStatus<bool>> ActivateAdministrator(ActivateAdministratorCommand command)
    {
        var resp = await Repository.ActivateAdministrator(command);
        await Repository.SaveChangesAsync();
        return resp;
    }

    public async Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand newStudentDto)
    {
        var resp = await Repository.AddNewStudent(newStudentDto);
        await Repository.SaveChangesAsync();
        return resp;
    }
}
