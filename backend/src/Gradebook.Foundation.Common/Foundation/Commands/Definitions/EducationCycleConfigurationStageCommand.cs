namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class EducationCycleConfigurationStageCommand
{
    public Guid Guid { get; set; }
    public DateTime? DateSince { get; set; }
    public DateTime? DateUntil { get; set; }
    public List<EducationCycleConfigurationSubjectCommand> Subjects { get; set; } = new();
}
