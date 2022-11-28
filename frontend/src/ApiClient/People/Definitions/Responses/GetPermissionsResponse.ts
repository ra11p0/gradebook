import PermissionEnum from '../../../../Common/Enums/Permissions/PermissionEnum';
import PermissionGroupEnum from '../../../../Common/Enums/Permissions/PermissionGroupEnum';
import PermissionLevelEnum from '../../../../Common/Enums/Permissions/PermissionLevelEnum';

export default interface GetPermissionsResponse {
  permissionId: PermissionEnum;
  permissionLevel: PermissionLevelEnum;
  permissionLevels: PermissionLevelEnum[];
  permissionGroup: PermissionGroupEnum;
}
