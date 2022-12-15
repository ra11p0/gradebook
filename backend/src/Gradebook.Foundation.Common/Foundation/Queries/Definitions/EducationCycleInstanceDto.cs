namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCycleInstanceDto
{
    public Guid Guid { get; set; }
    public Guid EducationCycleGuid { get; set; }
    public string EducationCycleName { get; set; } = string.Empty;
    public DateTime DateSince { get; set; }
    public DateTime DateUntil { get; set; }
    public Guid CreatorGuid { get; set; }

    public List<EducationCycleStepInstanceDto> EducationCycleStepInstances { get; set; } = new();
}
