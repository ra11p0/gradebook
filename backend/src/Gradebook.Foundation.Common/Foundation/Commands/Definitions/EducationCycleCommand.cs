namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class EducationCycleCommand
{
    public Guid? Guid { get; set; }
    public Guid SchoolGuid { get; set; }
    public Guid CreatorGuid { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<EducationCycleStepCommand> Stages { get; set; } = new();
}
