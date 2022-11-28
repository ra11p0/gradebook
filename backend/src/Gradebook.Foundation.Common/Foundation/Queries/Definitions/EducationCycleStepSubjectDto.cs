namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCycleStepSubjectDto
{
    public Guid? Guid { get; set; }
    public Guid SubjectGuid { get; set; }
    public int HoursInStep { get; set; }
    public bool IsMandatory { get; set; }
    public bool GroupsAllowed { get; set; }
}
