namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class EducationCycleCommand : Validatable<EducationCycleCommand>
{
    public Guid? Guid { get; set; }
    public Guid SchoolGuid { get; set; }
    public Guid CreatorGuid { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<EducationCycleStepCommand> Stages { get; set; } = new();

    protected override bool Validate(EducationCycleCommand validatable)
    {
        var isAnyStageInvalid = Stages.Any(stage => !stage.IsValid);
        return SchoolGuid != default && !string.IsNullOrEmpty(Name) && !isAnyStageInvalid && Stages.Count > 0;
    }
}
