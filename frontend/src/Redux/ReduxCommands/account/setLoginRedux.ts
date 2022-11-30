import AccountsProxy from '../../../ApiClient/Accounts/AccountsProxy';
import { connectAllHubs } from '../../../ApiClient/SignalR/HubsResolver';
import { store } from '../../../store';
import ActionTypes from '../../ActionTypes/accountActionTypes';
import setSchoolReduxWrapper from './setSchoolRedux';
import setSchoolsListReduxWrapper from './setSchoolsListRedux';
import setUserReduxWrapper from './setUserRedux';

const logIn = {
  type: ActionTypes.LogIn,
};

export interface logInAction {
  accessToken: string;
  refreshToken: string;
}

export default async (
  action: logInAction,
  dispatch: any = store.dispatch
): Promise<void> => {
  return await new Promise((resolve, reject) => {
    localStorage.setItem('access_token', action.accessToken);
    localStorage.setItem('refresh_token', action.refreshToken);

    dispatch({ ...logIn, payload: { ...action } });
    void AccountsProxy.getMe()
      .then(async (getMeResponse) => {
        await setUserReduxWrapper({ userId: getMeResponse.data.userId });
        await setSchoolsListReduxWrapper(dispatch, {
          schoolsList: getMeResponse.data.schools,
        });
        const defaultSchool = getMeResponse.data.schools.find(() => true);
        if (defaultSchool)
          await setSchoolReduxWrapper(dispatch, {
            schoolGuid: defaultSchool.school.guid,
            schoolName: defaultSchool.school.name,
          });
      })
      .then(async () => {
        await connectAllHubs();
      })
      .then(resolve)
      .catch(reject);
  });
};
