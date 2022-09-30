using Gradebook.Foundation.Common.Permissions.Enums;

namespace Api.Models.People;

public class GetPermissionsResponseModel
{
    public PermissionGroupEnum PermissionGroup { get; set; }
    public PermissionEnum PermissionId { get; set; }
    public PermissionLevelEnum PermissionLevel { get; set; }
    public PermissionLevelEnum[]? PermissionLevels { get; set; }
}
