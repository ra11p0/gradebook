namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class ClassDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid SchoolGuid { get; set; }
}
