namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class NewPersonCommand
{
    public Guid CreatorGuid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
}
