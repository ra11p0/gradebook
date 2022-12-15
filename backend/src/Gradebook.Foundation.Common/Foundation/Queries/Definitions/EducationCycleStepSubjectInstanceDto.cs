namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class EducationCycleStepSubjectInstanceDto
{
    public Guid Guid { get; set; }
    public Guid AssignedTeacherGuid { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public string TeacherLastName { get; set; } = string.Empty;
    public Guid SubjectGuid { get; set; }
    public Guid EducationCycleStepInstanceGuid { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public int HoursInStep { get; set; }
    public bool IsMandatory { get; set; }
    public bool GroupsAllowed { get; set; }
}
