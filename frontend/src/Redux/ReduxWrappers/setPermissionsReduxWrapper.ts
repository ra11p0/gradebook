import { SET_PERMISSIONS } from "../../Constraints/actionTypes";
import PermissionLevelEnum from '../../Common/Enums/Permissions/PermissionLevelEnum';

const setPermissions = {
    type: SET_PERMISSIONS
}

export interface setPermissionsAction {
    permissions: PermissionLevelEnum[]
}

export default (dispatch: any, action: setPermissionsAction) => {
    dispatch({ ...setPermissions, ...action })
};
