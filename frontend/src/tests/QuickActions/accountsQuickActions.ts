import { store } from '../../store';
import setLogInReduxWrapper from '../../Redux/ReduxCommands/account/setLoginRedux'
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy'
import setLogOutReduxWrapper from '../../Redux/ReduxCommands/account/setLogOutRedux';

const logIn = (email: string, password: string) => {
    return AccountsProxy.logIn({ email, password }).then(response => {
        setLogInReduxWrapper(store.dispatch, {
            accessToken: response.data.access_token,
            refreshToken: response.data.refresh_token
        })
    })
}

const logOut = () => setLogOutReduxWrapper(store.dispatch);

export default {
    logIn, logOut
}
