using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

namespace Gradebook.Foundation.Logic.Commands;

public class FoundationCommandsRepositoryCached : BaseRepositoryCached<FoundationCommandsRepository, object>, IFoundationCommandsRepository
{
    public FoundationCommandsRepositoryCached(FoundationCommandsRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }

    public Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand newStudentDto)
        => Base.AddNewStudent(newStudentDto);

    public void SaveChanges()
    {
        Base.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return Base.SaveChangesAsync();
    }
}
