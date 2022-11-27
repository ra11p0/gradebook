import PermissionLevelEnum from '../../../Common/Enums/Permissions/PermissionLevelEnum';

export default (state: any): PermissionLevelEnum[] =>
  state.common.permissions ?? [];
