import { SET_SCHOOL } from "../Constraints/actionTypes";

export const setSchool = {
    type: SET_SCHOOL
}
export interface setSchoolAction {
    schoolGuid: string;
    schoolName: string;
}

export const setSchoolWrapper = (dispatch: any, action: setSchoolAction) => {
    dispatch({ ...setSchool, ...action })
};
