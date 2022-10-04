import GetAccessiblePeopleResponse from "../../ApiClient/Accounts/Definitions/Responses/GetAccessiblePeopleResponse";
import { SET_PEOPLE_LIST } from "../../Constraints/actionTypes";

const setPeopleList = {
    type: SET_PEOPLE_LIST
}

export interface setPeopleListAction {
    peopleList: GetAccessiblePeopleResponse[]
}

export default (dispatch: any, action: setPeopleListAction) => {
    dispatch({ ...setPeopleList, ...action })
};
