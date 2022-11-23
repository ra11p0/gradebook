namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCycleDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public Guid CreatorGuid { get; set; }
    public PersonDto? Creator { get; set; }
}
