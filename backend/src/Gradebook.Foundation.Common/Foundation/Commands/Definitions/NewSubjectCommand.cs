namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class NewSubjectCommand : Validatable<NewSubjectCommand>
{
    public string Name { get; set; } = string.Empty;

    protected override bool Validate(NewSubjectCommand validatable)
    {
        return !string.IsNullOrEmpty(Name);
    }
}
