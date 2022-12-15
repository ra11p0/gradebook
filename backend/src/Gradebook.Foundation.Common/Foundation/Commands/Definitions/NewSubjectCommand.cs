namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class NewSubjectCommand : Validatable
{
    public string Name { get; set; } = string.Empty;

    protected override StatusResponse Validate()
    {
        return new StatusResponse(!string.IsNullOrEmpty(Name));
    }
}
