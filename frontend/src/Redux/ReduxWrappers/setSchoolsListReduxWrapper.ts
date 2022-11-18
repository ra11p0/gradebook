import GetAccessibleSchoolsResponse from "../../ApiClient/Accounts/Definitions/Responses/GetAccessibleSchoolsResponse";
import { SET_SCHOOLS_LIST } from "../../Constraints/accountActionTypes";
import ActionTypes from "../ActionTypes/accountActionTypes";

const setSchoolsList = {
    type: ActionTypes.SetSchoolsList
}

export interface setSchoolsListAction {
    schoolsList: GetAccessibleSchoolsResponse[]
}

export default (dispatch: any, action: setSchoolsListAction) => {
    dispatch({ ...setSchoolsList, payload: { ...action } })
};
