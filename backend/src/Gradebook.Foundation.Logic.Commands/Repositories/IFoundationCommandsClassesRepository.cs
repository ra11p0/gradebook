using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;

namespace Gradebook.Foundation.Logic.Commands.Repositories;

public interface IFoundationCommandsClassesRepository
{
    Task<StatusResponse> SetActiveEducationCycleToClasses(IEnumerable<Guid> classesGuids, Guid educationCycleGuid);
    Task<StatusResponse> DeleteActiveEducationCycleFromClasses(IEnumerable<Guid> classesGuids);
    Task<ResponseWithStatus<Guid>> ConfigureEducationCycleForClass(Guid classGuid, Guid creatorGuid, EducationCycleConfigurationCommand configuration);
    Task<StatusResponse> ConfigureEducationCycleStageInstanceForEducationCycleInstance(Guid educationCycleInstanceGuid, IEnumerable<EducationCycleConfigurationStageCommand> stageCommands);
    Task<ResponseWithStatus<Guid>> ConfigureEducationCycleStageInstanceForEducationCycleInstance(Guid educationCycleInstanceGuid, EducationCycleConfigurationStageCommand stageCommand);
    Task<StatusResponse> ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(Guid educationCycleStepInstanceGuid, IEnumerable<EducationCycleConfigurationSubjectCommand> subjectCommands);
    Task<ResponseWithStatus<Guid>> ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(Guid educationCycleStepInstanceGuid, EducationCycleConfigurationSubjectCommand subjectCommand);
    Task<StatusResponse> AddStudentsToClass(Guid classGuid, IEnumerable<Guid> studentsGuids);
    Task<StatusResponse> AddTeachersToClass(Guid classGuid, IEnumerable<Guid> teachersGuids);
    Task<StatusResponse> DeleteStudentsFromClass(Guid classGuid, IEnumerable<Guid> studentsGuids);
    Task<StatusResponse> DeleteTeachersFromClass(Guid classGuid, IEnumerable<Guid> teachersGuids);
    Task<StatusResponse> SetStudentActiveClass(Guid classGuid, Guid studentGuid);
    Task<StatusResponse> RemoveStudentActiveClass(Guid studentGuid);
}
