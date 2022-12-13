namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class EducationCycleConfigurationStageCommand : Validatable<EducationCycleConfigurationStageCommand>
{
    public Guid EducationCycleStageGuid { get; set; }
    public int Order { get; set; }
    public DateTime? DateSince { get; set; }
    public DateTime? DateUntil { get; set; }
    public List<EducationCycleConfigurationSubjectCommand> Subjects { get; set; } = new();

    protected override bool Validate(EducationCycleConfigurationStageCommand validatable)
    {
        if (!(DateSince.HasValue && DateUntil.HasValue) || (!DateSince.HasValue && !DateUntil.HasValue)) return false;
        if (DateSince.HasValue && DateUntil.HasValue && DateSince.Value > DateUntil.Value)
            return false;
        if (!Subjects.Any()) return false;
        return true;
    }
}
