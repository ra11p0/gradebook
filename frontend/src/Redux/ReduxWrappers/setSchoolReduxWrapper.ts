import { store } from "../../store";
import ActionTypes from "../ActionTypes/accountActionTypes";
import getPeopleReduxProxy from "../ReduxProxy/getPeopleReduxProxy";
import setPersonReduxWrapper from "./setPersonReduxWrapper";

export const setSchool = {
    type: ActionTypes.SetSchool
}
export interface setSchoolAction {
    schoolGuid: string;
    schoolName: string;
}

export default (dispatch: any, action: setSchoolAction) => {
    dispatch({ ...setSchool, payload: { ...action } })
    var schoolRelatedPerson = getPeopleReduxProxy(store.getState()).find(e => e.schoolGuid == action.schoolGuid);
    setPersonReduxWrapper(dispatch, {
        personGuid: schoolRelatedPerson?.guid ?? "",
        fullName: `${schoolRelatedPerson?.name} ${schoolRelatedPerson?.surname}`
    });
};
