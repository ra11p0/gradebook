namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCyclesForClassDto
{
    public EducationCycleInstanceDto? ActiveEducationCycleInstance { get; set; }
    public EducationCycleDto? ActiveEducationCycle { get; set; }
    public Guid? ActiveEducationCycleGuid { get; set; }
    public bool HasPreparedActiveEducationCycle => ActiveEducationCycleGuid.HasValue && ActiveEducationCycle is not null;
    public List<EducationCycleInstanceDto>? EducationCyclesInstances { get; set; }
}
