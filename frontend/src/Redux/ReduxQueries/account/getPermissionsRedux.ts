import PermissionLevelEnum from '../../../Common/Enums/Permissions/PermissionLevelEnum';
import { GlobalState } from '../../../store';

export default (state: GlobalState): PermissionLevelEnum[] =>
  state.common.permissions ?? [];
