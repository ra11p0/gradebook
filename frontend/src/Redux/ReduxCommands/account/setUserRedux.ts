import AccountsProxy from '../../../ApiClient/Accounts/AccountsProxy';
import { store } from '../../../store';
import ActionTypes from '../../ActionTypes/accountActionTypes';
import setApplicationLanguageRedux from './setApplicationLanguageRedux';

export const setUser = {
  type: ActionTypes.SetUser,
};

export interface setUserAction {
  userId: string;
}

export default async (
  action: setUserAction,
  dispatch: any = store.dispatch
): Promise<void> => {
  dispatch({ ...setUser, payload: { ...action } });
  const userSettings = (await AccountsProxy.settings.getUserSettings()).data;

  if (userSettings.language)
    await setApplicationLanguageRedux(userSettings.language);
};
