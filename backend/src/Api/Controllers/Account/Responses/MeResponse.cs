using Gradebook.Foundation.Common.Foundation.Enums;

namespace Api.Controllers.Account.Responses;

public class MeResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public Guid PersonGuid { get; set; }
    public string[]? Roles { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime? Birthday { get; set; }
    public SchoolRoleEnum? SchoolRole { get; set; }
}
