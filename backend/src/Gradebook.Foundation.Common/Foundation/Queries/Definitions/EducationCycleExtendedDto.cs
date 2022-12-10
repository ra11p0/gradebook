namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCycleExtendedDto : EducationCycleDto
{

    public Guid SchoolGuid { get; set; }
    public List<EducationCycleStepDto> Stages { get; set; } = new();
}
