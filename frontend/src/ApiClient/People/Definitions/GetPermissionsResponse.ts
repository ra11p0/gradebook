import PermissionEnum from "../../../Common/Enums/Permissions/PermissionEnum";
import PermissionLevelEnum from "../../../Common/Enums/Permissions/PermissionLevelEnum";

export default interface GetPermissionsResponse {
    permissionId: PermissionEnum;
    permissionLevel: PermissionLevelEnum;
}
