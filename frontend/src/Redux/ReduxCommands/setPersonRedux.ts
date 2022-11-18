import PeopleProxy from "../../ApiClient/People/PeopleProxy";
import { SET_PERSON } from "../../Constraints/accountActionTypes";
import ActionTypes from "../ActionTypes/accountActionTypes";
import setPermissionsReduxWrapper from "./setPermissionsRedux";

export const setPerson = {
    type: ActionTypes.SetPerson
}

export interface setPersonAction {
    personGuid: string;
    fullName: string;
}

export default (dispatch: any, action: setPersonAction) => {
    dispatch({ ...setPerson, payload: { ...action } });
    PeopleProxy.permissions.getPermissions(action.personGuid).then(permissionsResponse => setPermissionsReduxWrapper(dispatch, {
        permissions: permissionsResponse.data.map(e => e.permissionLevel)
    }));
};
