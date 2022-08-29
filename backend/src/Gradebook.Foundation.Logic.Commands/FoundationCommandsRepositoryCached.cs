using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

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

    public void SaveChanges()
    {
        Base.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return Base.SaveChangesAsync();
    }
}
