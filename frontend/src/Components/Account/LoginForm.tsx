import React from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import AccountProxy from "../../ApiClient/Accounts/AccountsProxy";
import { withTranslation } from "react-i18next";
import { Button } from "react-bootstrap";
import { isLoggedInProxy } from "../../Redux/ReduxProxy/getIsLoggedInReduxProxy";
import { currentSchoolProxy } from "../../Redux/ReduxProxy/getCurrentSchoolReduxProxy";
import { logInAction, loginWrapper } from "../../Redux/ReduxWrappers/setLoginReduxWrapper";
import { setSchoolsListAction, setSchoolsListWrapper } from "../../Redux/ReduxWrappers/setSchoolsListReduxWrapper";
import { setUserAction, setUserWrapper } from "../../Redux/ReduxWrappers/setUserReduxWrapper";

const mapStateToProps = (state: any) => ({
  isLoggedIn: isLoggedInProxy(state),
  currentSchool: currentSchoolProxy(state),
});

const mapDispatchToProps = (dispatch: any) => ({
  logIn: (action: logInAction) => loginWrapper(dispatch, action),
  setSchools: (action: setSchoolsListAction) => setSchoolsListWrapper(dispatch, action),
  setUser: (action: setUserAction) => setUserWrapper(dispatch, action),
});

interface LogInProps {
  logIn?: (action: logInAction) => void;
  setSchools?: (action: setSchoolsListAction) => void;
  setUser?: (action: setUserAction) => void;
  isLoggedIn: boolean;
  t: any;
  currentSchool?: any;
}

interface LogInState {
  username?: string;
  password?: string;
  loginFailed?: boolean;
}

class LoginForm extends React.Component<LogInProps, LogInState> {
  constructor(props: LogInProps) {
    super(props);
    this.state = {
      username: "",
      password: "",
      loginFailed: false,
    };
  }

  componentDidMount() {
    var access = localStorage.getItem("access_token");
    var refresh = localStorage.getItem("refresh_token");
    if (access && refresh) {
      AccountProxy.refreshAccessToken(access, refresh).then((refreshAccessTokenResponse) => {
        this.props.logIn!({
          accessToken: refreshAccessTokenResponse.data.access_token,
          refreshToken: refreshAccessTokenResponse.data.refresh_token,
        });
        AccountProxy.getMe().then((getMeResponse) => {
          this.props.setUser!({ userId: getMeResponse.data.userId });
          this.props.setSchools!({ schoolsList: getMeResponse.data.schools });
        });
      });
    }
  }

  onLogIn() {
    AccountProxy.logIn({ username: this.state.username!, password: this.state.password! })
      .then((loginResponse) => {
        this.props.logIn!({ refreshToken: loginResponse.data.refresh_token, accessToken: loginResponse.data.access_token });
        AccountProxy.getMe().then((getMeResponse) => {
          this.props.setUser!({ userId: getMeResponse.data.userId });
          this.props.setSchools!({ schoolsList: getMeResponse.data.schools });
        });
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
            <label>{t("loging")}</label>
          </div>
          {this.state.loginFailed && <div className="m-1 p-1 alert alert-danger">{t("loginFailed")}</div>}
          <div className="m-1 p-1">
            <label>{t("email")}</label>
            <input
              className="form-control"
              name="email"
              value={this.state.username}
              onChange={(e) =>
                this.setState({
                  ...this.state,
                  username: e.target.value,
                })
              }
            ></input>
          </div>
          <div className="m-1 p-1">
            <label>{t("password")}</label>
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
              <Link to={"register"}>{t("register")}</Link>
              <Link to={""}>{t("changePassword")}</Link>
              <Link to={""}>{t("recoverAccess")}</Link>
            </div>
            <Button variant="outline-primary" onClick={() => this.onLogIn!()} type="submit">
              {t("logIn")}
            </Button>
          </div>
        </div>
      </div>
    );
  }
}

export default withTranslation("loginForm")(connect(mapStateToProps, mapDispatchToProps)(LoginForm));
