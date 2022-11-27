import { store } from '../../store';
import setLogInReduxWrapper from '../../Redux/ReduxCommands/account/setLoginRedux';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import setLogOutReduxWrapper from '../../Redux/ReduxCommands/account/setLogOutRedux';

const logIn = async (email: string, password: string): Promise<void> => {
  return await AccountsProxy.logIn({ email, password }).then(
    async (response) => {
      await setLogInReduxWrapper(store.dispatch, {
        accessToken: response.data.access_token,
        refreshToken: response.data.refresh_token,
      });
    }
  );
};

const logOut = (): void => setLogOutReduxWrapper(store.dispatch);

export default {
  logIn,
  logOut,
};
