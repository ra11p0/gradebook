import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from './Components/Shared/Header/Header';
import Index from './Routes/Index';
import Dashboard from './Routes/Dashboard';
import { connect } from 'react-redux';
import Account from './Routes/Account';
import ActivateAccount from './Components/Account/Activation/ActivateAccount';
import RegisterForm from './Components/Account/Register/RegisterForm';
import { ReactNotifications } from 'react-notifications-component';
import School from './Routes/School';
import Student from './Routes/Student';
import Person from './Routes/Person';
import Class from './Routes/Class';
import getIsLoggedInReduxProxy from './Redux/ReduxQueries/account/getIsLoggedInRedux';
import getIsUserActivatedReduxProxy from './Redux/ReduxQueries/account/getIsUserActivatedRedux';
import setAppLoadReduxWrapper from './Redux/ReduxCommands/account/setAppLoadRedux';
import LoadingScreen from './Components/Shared/LoadingScreen';
import AccountProxy from './ApiClient/Accounts/AccountsProxy';
import setLoginReduxWrapper from './Redux/ReduxCommands/account/setLoginRedux';
import { GlobalState } from './store';
import setLogOutReduxWrapper from './Redux/ReduxCommands/account/setLogOutRedux';
import setApplicationLanguageReduxWrapper from './Redux/ReduxCommands/account/setApplicationLanguageRedux';
import Subject from './Routes/Subject';
import EducationCycle from './Routes/EducationCycle';
import PermissionLevelEnum from './Common/Enums/Permissions/PermissionLevelEnum';
import getHasPermissionRedux from './Redux/ReduxQueries/account/getHasPermissionRedux';
import Service from './Routes/Service';

interface AppProps {
  onLoad: (isAppLoaded: boolean) => void;
  appLoaded: boolean;
  isLoggedIn: boolean;
  isUserActivated: boolean;
  permissions: {
    canSeeEducationCycleRoute: boolean;
  };
}

class App extends React.Component<AppProps> {
  onLoad(loaded: boolean): void {
    this.props.onLoad(loaded);
  }

  async componentDidMount(): Promise<void> {
    const userLang = navigator.language;
    await setApplicationLanguageReduxWrapper(userLang);
    const access = localStorage.getItem('access_token');
    const refresh = localStorage.getItem('refresh_token');
    if (access && refresh) {
      await AccountProxy.refreshAccessToken(access, refresh)
        .then(async (refreshAccessTokenResponse) => {
          await setLoginReduxWrapper({
            accessToken: refreshAccessTokenResponse.data.access_token,
            refreshToken: refreshAccessTokenResponse.data.refresh_token,
          });
        })
        .catch(async () => {
          await setLogOutReduxWrapper();
        });
    }
    this.onLoad(true);
  }

  render(): React.ReactNode {
    return (
      <div>
        {import.meta.env.VITE_APP_BUILD && (
          <div className="position-fixed">{import.meta.env.VITE_APP_BUILD}</div>
        )}

        <LoadingScreen isReady={this.props.appLoaded}>
          <BrowserRouter>
            <ReactNotifications />
            <Header />
            <Routes>
              {
                // Public
                !this.props.isLoggedIn && (
                  <>
                    <Route
                      path="/account/register"
                      element={<RegisterForm />}
                    />
                    <Route path="/Service/*" element={<Service />} />
                    <Route path="*" element={<Index />} />
                  </>
                )
              }
              {
                // Only for logged in and inactive
                this.props.isLoggedIn && !this.props.isUserActivated && (
                  <Route path="*" element={<ActivateAccount />} />
                )
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
                    <Route path="/subject/*" element={<Subject />} />
                    {this.props.permissions.canSeeEducationCycleRoute && (
                      <>
                        <Route
                          path="/educationCycle/*"
                          element={<EducationCycle />}
                        />
                      </>
                    )}
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
  (state: GlobalState) => ({
    appLoaded: state.common.appLoaded,
    isLoggedIn: getIsLoggedInReduxProxy(state),
    isUserActivated: getIsUserActivatedReduxProxy(state),
    permissions: {
      canSeeEducationCycleRoute: getHasPermissionRedux(
        [
          PermissionLevelEnum.EducationCycles_CanCreateAndDelete,
          PermissionLevelEnum.EducationCycles_ViewOnly,
        ],
        state
      ),
    },
  }),
  (dispatch) => ({
    onLoad: (isAppLoaded: boolean): void =>
      setAppLoadReduxWrapper(dispatch, isAppLoaded),
  })
)(App);
