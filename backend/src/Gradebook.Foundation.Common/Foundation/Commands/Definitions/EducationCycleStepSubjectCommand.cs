namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class EducationCycleStepSubjectCommand
{
    public Guid? Guid { get; set; }
    public Guid SubjectGuid { get; set; }
    public int HoursNo { get; set; }
    public bool IsMandatory { get; set; }
    public bool CanUseGroups { get; set; }
}
