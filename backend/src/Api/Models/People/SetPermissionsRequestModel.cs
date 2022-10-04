using Gradebook.Foundation.Common.Permissions.Enums;

namespace Api.Models.People;

public class SetPermissionsRequestModel
{
    public PermissionEnum PermissionId { get; set; }
    public PermissionLevelEnum PermissionLevel { get; set; }
}
