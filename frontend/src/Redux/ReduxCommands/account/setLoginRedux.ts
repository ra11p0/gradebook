import { Moment } from 'moment';
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
  expiresIn: Moment;
}

export default async (
  action: logInAction,
  dispatch: any = store.dispatch
): Promise<void> => {
  localStorage.setItem('access_token', action.accessToken);
  localStorage.setItem('refresh_token', action.refreshToken);
  localStorage.setItem('expires_in', action.expiresIn.toISOString());

  dispatch({
    ...logIn,
    payload: { ...action, expiresIn: action.expiresIn.toISOString() },
  });
  const getMeResponse = await AccountsProxy.getMe();

  setSchoolsListReduxWrapper({
    schoolsList: getMeResponse.data.schools,
  });

  await setUserReduxWrapper({
    ...getMeResponse.data,
  });

  await connectAllHubs();
};
