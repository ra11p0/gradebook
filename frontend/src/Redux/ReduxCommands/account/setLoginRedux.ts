import AccountsProxy from '../../../ApiClient/Accounts/AccountsProxy';
import { connectAllHubs } from '../../../ApiClient/SignalR/HubsResolver';
import { store } from '../../../store';
import ActionTypes from '../../ActionTypes/accountActionTypes';
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
  localStorage.setItem('access_token', action.accessToken);
  localStorage.setItem('refresh_token', action.refreshToken);

  dispatch({ ...logIn, payload: { ...action } });
  const getMeResponse = await AccountsProxy.getMe();

  await setSchoolsListReduxWrapper({
    schoolsList: getMeResponse.data.schools,
  });

  await setUserReduxWrapper({ userId: getMeResponse.data.userId });

  await connectAllHubs();
};
