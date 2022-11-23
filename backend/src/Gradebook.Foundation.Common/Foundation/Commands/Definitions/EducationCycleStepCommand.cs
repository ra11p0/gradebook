namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class EducationCycleStepCommand
{
    public Guid? Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<EducationCycleStepSubjectCommand> Subjects { get; set; } = new();
}
