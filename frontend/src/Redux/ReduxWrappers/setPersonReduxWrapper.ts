import PeopleProxy from "../../ApiClient/People/PeopleProxy";
import { SET_PERSON } from "../../Constraints/actionTypes";
import setPermissionsReduxWrapper from "./setPermissionsReduxWrapper";

export const setPerson = {
    type: SET_PERSON
}

export interface setPersonAction {
    personGuid: string;
    fullName: string;
}

export default (dispatch: any, action: setPersonAction) => {
    dispatch({ ...setPerson, ...action });
    PeopleProxy.permissions.getPermissions(action.personGuid).then(permissionsResponse => setPermissionsReduxWrapper(dispatch, {
        permissions: permissionsResponse.data.map(e => e.permissionLevel)
    }));
};
