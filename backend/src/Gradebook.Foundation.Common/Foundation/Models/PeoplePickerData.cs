using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Common.Foundation.Models;

public class PeoplePickerData
{
    public Guid SchoolGuid { get; set; }
    public string? Query { get; set; }
    public bool IncludeDeleted { get; set; } = false;
    public Guid? ActiveClassGuid { get; set; }
    public DateTime? BirthdaySince { get; set; }
    public DateTime? BirthdayUntil { get; set; }
    public SchoolRoleEnum SchoolRole { get; set; }
}
