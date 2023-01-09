using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class AdminDto
{
    public Guid Guid { get; set; }
    public Guid CreatorGuid { get; set; }
    public string? UserGuid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public SchoolRoleEnum SchoolRole { get; set; }
    public DateTime? Birthday { get; set; }
    public bool IsActive => UserGuid is not null;
}
