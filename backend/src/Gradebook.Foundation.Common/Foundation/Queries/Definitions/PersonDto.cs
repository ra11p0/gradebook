using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class PersonDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public SchoolRoleEnum SchoolRole { get; set; }
    public Guid? SchoolGuid { get; set; }
    public DateTime? Birthday { get; set; }
}
