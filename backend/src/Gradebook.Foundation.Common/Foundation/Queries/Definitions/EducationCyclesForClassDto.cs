namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCyclesForClassDto
{
    public EducationCycleInstanceDto? ActiveEducationCycle { get; set; }
    public List<EducationCycleInstanceDto>? EducationCycles { get; set; }
}
