import { SET_SCHOOL } from "../../Constraints/actionTypes";

export const setSchool = {
    type: SET_SCHOOL
}
export interface setSchoolAction {
    schoolGuid: string;
    schoolName: string;
}

export default (dispatch: any, action: setSchoolAction) => {
    dispatch({ ...setSchool, ...action })
};
