namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCyclesForClassDto
{
    public EducationCycleInstanceDto? ActiveEducationCycleInstance { get; set; }
    public Guid? ActiveEducationCycleGuid { get; set; }
    public bool HasPreparedAcriveEducationCycle => ActiveEducationCycleGuid.HasValue && ActiveEducationCycleInstance is not null;
    public List<EducationCycleInstanceDto>? EducationCycles { get; set; }
}
