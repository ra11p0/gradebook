namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class EducationCycleConfigurationCommand
{
    public Guid Guid { get; set; }
    public DateTime DateSince { get; set; }
    public DateTime DateUntil { get; set; }
    public List<EducationCycleConfigurationStageCommand> Stages { get; set; } = new();
}
