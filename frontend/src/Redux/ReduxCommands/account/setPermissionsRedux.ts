import PermissionLevelEnum from '../../../Common/Enums/Permissions/PermissionLevelEnum';
import ActionTypes from "../../ActionTypes/accountActionTypes";

const setPermissions = {
    type: ActionTypes.SetPermissions
}

export interface setPermissionsAction {
    permissions: PermissionLevelEnum[]
}

export default (dispatch: any, action: setPermissionsAction) => {
    dispatch({ ...setPermissions, payload: { ...action } })
};
