namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCycleExtendedDto
{
    public Guid? Guid { get; set; }
    public Guid SchoolGuid { get; set; }
    public Guid CreatorGuid { get; set; }
    public PersonDto? Creator { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<EducationCycleStepDto> Stages { get; set; } = new();
}
