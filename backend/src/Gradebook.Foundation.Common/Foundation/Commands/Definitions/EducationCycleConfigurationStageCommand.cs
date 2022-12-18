namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class EducationCycleConfigurationStageCommand : Validatable
{
    public Guid EducationCycleStageGuid { get; set; }
    public int Order { get; set; }
    public DateTime? DateSince { get; set; }
    public DateTime? DateUntil { get; set; }
    public List<EducationCycleConfigurationSubjectCommand> Subjects { get; set; } = new();

    protected override StatusResponse Validate()
    {
        if ((DateSince.HasValue && !DateUntil.HasValue) || (!DateSince.HasValue && DateUntil.HasValue)) return new StatusResponse(false);
        if (DateSince.HasValue && DateUntil.HasValue && DateSince.Value > DateUntil.Value)
            return new StatusResponse(false);
        if (!Subjects.Any()) return new StatusResponse(false);
        return new StatusResponse(true);
    }
}
