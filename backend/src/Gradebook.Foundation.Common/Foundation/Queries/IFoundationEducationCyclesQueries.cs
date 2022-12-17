using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Common.Foundation.Queries;

public interface IFoundationEducationCyclesQueries
{
    Task<ResponseWithStatus<IPagedList<ClassDto>>> GetAvalibleClassesInForEducationCycle(Guid educationCycleGuid, int page, string? query);
    Task<ResponseWithStatus<EducationCycleExtendedDto>> GetEducationCycle(Guid educationCycleGuid);
    Task<ResponseWithStatus<IPagedList<EducationCycleDto>>> GetEducationCyclesInSchool(Guid schoolGuid, int page, string query);
}
