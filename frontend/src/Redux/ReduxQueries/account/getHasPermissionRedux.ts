import PermissionLevelEnum from '../../../Common/Enums/Permissions/PermissionLevelEnum';
import { GlobalState, store } from '../../../store';
import getPermissionsReduxProxy from './getPermissionsRedux';

export default (
  allowingPermissions: PermissionLevelEnum[],
  state: GlobalState = store.getState()
): boolean => {
  const currentPermissions = getPermissionsReduxProxy(state);
  let canSee = false;
  currentPermissions.forEach((permission) => {
    if (allowingPermissions.includes(permission)) canSee = true;
  });
  return canSee;
};
