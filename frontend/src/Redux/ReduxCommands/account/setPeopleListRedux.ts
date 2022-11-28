import GetAccessiblePeopleResponse from '../../../ApiClient/Accounts/Definitions/Responses/GetAccessiblePeopleResponse';
import ActionTypes from '../../ActionTypes/accountActionTypes';

const setPeopleList = {
  type: ActionTypes.SetPeopleList,
};

export interface setPeopleListAction {
  peopleList: GetAccessiblePeopleResponse[];
}

export default (dispatch: any, action: setPeopleListAction): void => {
  dispatch({ ...setPeopleList, payload: { ...action } });
};
