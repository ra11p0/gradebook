import { SET_SCHOOL } from "../../Constraints/actionTypes";
import { store } from "../../store";
import getPeopleReduxProxy from "../ReduxProxy/getPeopleReduxProxy";
import setPersonReduxWrapper from "./setPersonReduxWrapper";

export const setSchool = {
    type: SET_SCHOOL
}
export interface setSchoolAction {
    schoolGuid: string;
    schoolName: string;
}

export default (dispatch: any, action: setSchoolAction) => {
    dispatch({ ...setSchool, ...action })
    var schoolRelatedPerson = getPeopleReduxProxy(store.getState()).find(e => e.schoolGuid == action.schoolGuid);
    setPersonReduxWrapper(dispatch, {
        personGuid: schoolRelatedPerson?.guid ?? "",
        fullName: `${schoolRelatedPerson?.name} ${schoolRelatedPerson?.surname}`
    });
};
