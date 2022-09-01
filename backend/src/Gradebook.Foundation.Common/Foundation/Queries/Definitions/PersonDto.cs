using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class PersonDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public SchoolRoleEnum SchoolRole { get; set; }
    public DateTime? Birthday { get; set; }
}
