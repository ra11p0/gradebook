namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCyclesForClassDto
{
    public EducationCycleInstanceDto? ActiveEducationCycleInstance { get; set; }
    public EducationCycleStepInstanceDto? CurrentStepInstance { get; set; }
    public EducationCycleStepInstanceDto? PreviousStepInstance { get; set; }
    public EducationCycleStepInstanceDto? NextStepInstance { get; set; }
    public EducationCycleDto? ActiveEducationCycle { get; set; }
    public Guid? ActiveEducationCycleGuid => ActiveEducationCycle?.Guid;
    public bool HasPreparedActiveEducationCycle => ActiveEducationCycleGuid.HasValue && ActiveEducationCycle is not null;
}
