using Gradebook.Foundation.Common.Permissions.Enums;

namespace Api.Models.People;

public class GetPermissionsResponseModel
{
    public PermissionEnum PermissionId { get; set; }
    public PermissionLevelEnum PermissionLevel { get; set; }
}
