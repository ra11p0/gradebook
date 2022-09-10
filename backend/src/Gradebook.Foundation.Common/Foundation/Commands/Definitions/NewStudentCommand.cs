namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class NewStudentCommand
{
    public Guid CreatorGuid { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
}
