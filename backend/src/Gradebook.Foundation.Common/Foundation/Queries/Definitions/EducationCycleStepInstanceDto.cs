namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCycleStepInstanceDto
{
    public DateTime? DateSince { get; set; }
    public DateTime? DateUntil { get; set; }
    public Guid Guid { get; set; }
    public Guid EducationCycleInstanceGuid { get; set; }
    public int Order { get; set; }
    public bool Started => DateSince.HasValue && !StartedDate.HasValue ? Time.UtcNow > DateSince.Value : StartedDate.HasValue;
    public bool Finished => DateUntil.HasValue && !FinishedDate.HasValue ? Time.UtcNow > DateUntil.Value : FinishedDate.HasValue;
    public DateTime? StartedDate { get; set; }
    public DateTime? FinishedDate { get; set; }
    public string EducationCycleStepName { get; set; } = string.Empty;
    public List<EducationCycleStepSubjectInstanceDto> EducationCycleStepSubjectInstances { get; set; } = new();
}
