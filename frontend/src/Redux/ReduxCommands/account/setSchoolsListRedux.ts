import GetAccessibleSchoolsResponse from '../../../ApiClient/Accounts/Definitions/Responses/GetAccessibleSchoolsResponse';
import ActionTypes from '../../ActionTypes/accountActionTypes';

const setSchoolsList = {
  type: ActionTypes.SetSchoolsList,
};

export interface setSchoolsListAction {
  schoolsList: GetAccessibleSchoolsResponse[];
}

export default (dispatch: any, action: setSchoolsListAction): void => {
  dispatch({ ...setSchoolsList, payload: { ...action } });
};
