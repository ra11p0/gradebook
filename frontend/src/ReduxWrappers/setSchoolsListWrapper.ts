import GetAccessibleSchoolsResponse from "../ApiClient/People/Definitions/GetAccessibleSchoolsResponse";
import { SET_SCHOOLS_LIST } from "../Constraints/actionTypes";

const setSchoolsList = {
    type: SET_SCHOOLS_LIST
}

export interface setSchoolsListAction {
    schoolsList: GetAccessibleSchoolsResponse[]
}

export const setSchoolsListWrapper = (dispatch: any, action: setSchoolsListAction) => dispatch({ ...setSchoolsList, ...action });