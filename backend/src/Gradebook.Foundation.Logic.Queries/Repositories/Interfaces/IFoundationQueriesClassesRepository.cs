using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries.Repositories.Interfaces;

public interface IFoundationQueriesClassesRepository
{
    Task<IEnumerable<EducationCycleStepInstanceDto>> GetAllEducationCycleStepInstancesForClass(Guid classGuid);
    Task<IEnumerable<EducationCycleInstanceDto>> GetAllEducationCycleInstancesForClass(Guid classGuid);
}
