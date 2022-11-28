import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import AccountProxy from '../../../ApiClient/Accounts/AccountsProxy';
import { withTranslation } from 'react-i18next';
import { Button } from 'react-bootstrap';
import getIsLoggedInReduxProxy from '../../../Redux/ReduxQueries/account/getIsLoggedInRedux';
import getCurrentSchoolReduxProxy from '../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import setLoginReduxWrapper, {
  logInAction,
} from '../../../Redux/ReduxCommands/account/setLoginRedux';
import setSchoolsListReduxWrapper, {
  setSchoolsListAction,
} from '../../../Redux/ReduxCommands/account/setSchoolsListRedux';
import setUserReduxWrapper, {
  setUserAction,
} from '../../../Redux/ReduxCommands/account/setUserRedux';
import setAppLoadReduxWrapper from '../../../Redux/ReduxCommands/account/setAppLoadRedux';
import { store } from '../../../store';

interface LogInProps {
  logIn?: (action: logInAction) => void;
  setSchools?: (action: setSchoolsListAction) => void;
  setUser?: (action: setUserAction) => void;
  isLoggedIn: boolean;
  t: any;
  currentSchool?: any;
}

interface LogInState {
  email?: string;
  password?: string;
  loginFailed?: boolean;
}

class LoginForm extends React.Component<LogInProps, LogInState> {
  constructor(props: LogInProps) {
    super(props);
    this.state = {
      email: '',
      password: '',
      loginFailed: false,
    };
  }

  onLogIn(): void {
    AccountProxy.logIn({
      email: this.state.email!,
      password: this.state.password!,
    })
      .then(async (loginResponse) => {
        setAppLoadReduxWrapper(store.dispatch, false);
        await this.props.logIn!({
          refreshToken: loginResponse.data.refresh_token,
          accessToken: loginResponse.data.access_token,
        });
        setAppLoadReduxWrapper(store.dispatch, true);
      })
      .catch(() => {
        this.setState({
          ...this.state,
          loginFailed: true,
        });
      });
  }

  render(): React.ReactNode {
    const { t } = this.props;
    return (
      <div className="card m-3 p-3">
        <div className="card-body">
          <div className="m-1 p-1 display-6">
            <label>{t('loging')}</label>
          </div>
          {this.state.loginFailed && (
            <div className="m-1 p-1 alert alert-danger">{t('loginFailed')}</div>
          )}
          <div className="m-1 p-1">
            <label>{t('email')}</label>
            <input
              className="form-control"
              name="email"
              value={this.state.email}
              onChange={(e) =>
                this.setState({
                  ...this.state,
                  email: e.target.value,
                })
              }
            ></input>
          </div>
          <div className="m-1 p-1">
            <label>{t('password')}</label>
            <input
              className="form-control"
              name="password"
              type="password"
              value={this.state.password}
              onChange={(e) =>
                this.setState({
                  ...this.state,
                  password: e.target.value,
                })
              }
            ></input>
          </div>
          <div className="m-1 p-1 d-flex justify-content-between">
            <div className="my-auto d-flex gap-2">
              <Link to={'register'}>{t('register')}</Link>
              <Link to={''}>{t('changePassword')}</Link>
              <Link to={''}>{t('recoverAccess')}</Link>
            </div>
            <Button
              variant="outline-primary"
              onClick={() => this.onLogIn()}
              type="submit"
            >
              {t('logIn')}
            </Button>
          </div>
        </div>
      </div>
    );
  }
}

export default withTranslation('loginForm')(
  connect(
    (state) => ({
      isLoggedIn: getIsLoggedInReduxProxy(state),
      currentSchool: getCurrentSchoolReduxProxy(state),
    }),
    (dispatch: any) => ({
      logIn: async (action: logInAction) =>
        await setLoginReduxWrapper(dispatch, action),
      setSchools: (action: setSchoolsListAction) =>
        setSchoolsListReduxWrapper(dispatch, action),
      setUser: (action: setUserAction) => setUserReduxWrapper(dispatch, action),
    })
  )(LoginForm)
);
