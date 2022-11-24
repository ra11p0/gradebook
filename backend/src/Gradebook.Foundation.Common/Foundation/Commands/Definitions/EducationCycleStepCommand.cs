namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class EducationCycleStepCommand : Validatable<EducationCycleStepCommand>
{
    public Guid? Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<EducationCycleStepSubjectCommand> Subjects { get; set; } = new();

    protected override bool Validate(EducationCycleStepCommand validatable)
    {
        var isAnySubjectInvalid = Subjects.Any(subject => !subject.IsValid);
        var areAllSubjectsUnique = Subjects.Select(sub => sub.SubjectGuid).Distinct().Count() == Subjects.Count;
        return !string.IsNullOrEmpty(Name) && !isAnySubjectInvalid && Subjects.Count() > 0 && areAllSubjectsUnique;
    }
}
