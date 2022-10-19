import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Header from "./Components/Shared/Header";
import Index from "./Routes/Index";
import Dashboard from "./Routes/Dashboard";
import { connect } from "react-redux";
import Account from "./Routes/Account";
import ActivateAccount from "./Components/Account/Activation/ActivateAccount";
import RegisterForm from "./Components/Account/Register/RegisterForm";
import { ReactNotifications } from "react-notifications-component";
import School from "./Routes/School";
import Student from "./Routes/Student";
import Person from "./Routes/Person";
import Class from "./Routes/Class";
import getIsLoggedInReduxProxy from "./Redux/ReduxProxy/getIsLoggedInReduxProxy";
import getIsUserActivatedReduxProxy from "./Redux/ReduxProxy/getIsUserActivatedReduxProxy";
import setAppLoadReduxWrapper from "./Redux/ReduxWrappers/setAppLoadReduxWrapper";
import LoadingScreen from "./Components/Shared/LoadingScreen";
import AccountProxy from "./ApiClient/Accounts/AccountsProxy";
import setLoginReduxWrapper from './Redux/ReduxWrappers/setLoginReduxWrapper';
import { store } from './store';
import setLogOutReduxWrapper from "./Redux/ReduxWrappers/setLogOutReduxWrapper";

interface AppProps {
  onLoad: (isAppLoaded: boolean) => {};
  appLoaded: boolean;
  isLoggedIn: boolean;
  isUserActivated: boolean;
}

class App extends React.Component<AppProps> {
  componentDidMount() {
    var access = localStorage.getItem("access_token");
    var refresh = localStorage.getItem("refresh_token");
    if (access && refresh) {
      AccountProxy.refreshAccessToken(access, refresh).then(async (refreshAccessTokenResponse) => {
        await setLoginReduxWrapper(store.dispatch, {
          accessToken: refreshAccessTokenResponse.data.access_token,
          refreshToken: refreshAccessTokenResponse.data.refresh_token,
        });
        this.props.onLoad(true);
      }).catch(() => {
        setLogOutReduxWrapper();
      });
    }
    else {
      this.props.onLoad(true);
    }
  }
  render(): React.ReactNode {
    return (
      <div>
        <LoadingScreen
          isReady={this.props.appLoaded}>
          <BrowserRouter>
            <ReactNotifications />
            <Header />
            <Routes>
              {
                //Public
                !this.props.isLoggedIn && (
                  <>
                    <Route path="/account/register" element={<RegisterForm />} />
                    <Route path="*" element={<Index />} />
                  </>
                )
              }
              {
                //Only for logged in and inactive
                this.props.isLoggedIn && !this.props.isUserActivated &&
                <Route path="*" element={<ActivateAccount />} />
              }
              {
                // only for logged in and activated
                this.props.isLoggedIn && this.props.isUserActivated && (
                  <>
                    <Route path="*" element={<Dashboard />} />
                    <Route path="/account/*" element={<Account />} />
                    <Route path="/dashboard/*" element={<Dashboard />} />
                    <Route path="/school/*" element={<School />} />
                    <Route path="/student/*" element={<Student />} />
                    <Route path="/person/*" element={<Person />} />
                    <Route path="/class/*" element={<Class />} />
                  </>
                )
              }
            </Routes>
          </BrowserRouter>
        </LoadingScreen>

      </div>
    );
  }
}

export default connect(
  (state: any) => ({
    appLoaded: state.common.appLoaded,
    isLoggedIn: getIsLoggedInReduxProxy(state),
    isUserActivated: getIsUserActivatedReduxProxy(state),
  }),
  (dispatch: any) => ({
    onLoad: (isAppLoaded: boolean) => setAppLoadReduxWrapper(dispatch, isAppLoaded),
  })
)(App);
