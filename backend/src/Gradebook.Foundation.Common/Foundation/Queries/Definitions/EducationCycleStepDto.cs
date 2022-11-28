namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCycleStepDto
{
    public Guid? Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<EducationCycleStepSubjectDto> Subjects { get; set; } = new();
}
