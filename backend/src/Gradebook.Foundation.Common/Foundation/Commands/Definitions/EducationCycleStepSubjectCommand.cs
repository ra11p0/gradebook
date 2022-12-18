namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class EducationCycleStepSubjectCommand : Validatable
{
    public Guid? Guid { get; set; }
    public Guid SubjectGuid { get; set; }
    public int HoursNo { get; set; }
    public bool IsMandatory { get; set; }
    public bool CanUseGroups { get; set; }

    protected override StatusResponse Validate()
    {
        return new StatusResponse(SubjectGuid != default(Guid) && HoursNo > 0);
    }
}
