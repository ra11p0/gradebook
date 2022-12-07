import GetAccessibleSchoolsResponse from '../../../ApiClient/Accounts/Definitions/Responses/GetAccessibleSchoolsResponse';
import { store } from '../../../store';
import ActionTypes from '../../ActionTypes/accountActionTypes';

const setSchoolsList = {
  type: ActionTypes.SetSchoolsList,
};

export interface setSchoolsListAction {
  schoolsList: GetAccessibleSchoolsResponse[];
}

export default (
  action: setSchoolsListAction,
  dispatch: any = store.dispatch
): void => {
  dispatch({ ...setSchoolsList, payload: { ...action } });
};
