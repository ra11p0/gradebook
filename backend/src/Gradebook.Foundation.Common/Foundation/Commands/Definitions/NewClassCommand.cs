namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class NewClassCommand
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid SchoolGuid { get; set; }
    public DateTime CreatedDate { get; set; }
}
