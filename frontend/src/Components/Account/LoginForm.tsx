import React from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import AccountProxy from "../../ApiClient/Account/AccountProxy";
import { withTranslation } from "react-i18next";
import { Button } from "react-bootstrap";
import { logInAction, loginWrapper } from "../../ReduxWrappers/loginWrapper";
import {
  refreshTokenAction,
  refreshTokenWrapper,
} from "../../ReduxWrappers/refreshTokenWrapper";

const mapStateToProps = (state: any) => ({
  isLoggedIn: state.common.isLoggedIn,
});

const mapDispatchToProps = (dispatch: any) => ({
  onLogIn: (action: logInAction) => loginWrapper(dispatch, action),
  refreshToken: (action: refreshTokenAction) =>
    refreshTokenWrapper(dispatch, action),
});

interface LogInProps {
  onLogIn?: (action: logInAction) => void;
  refreshToken: (action: refreshTokenAction) => void;
  isLoggedIn: boolean;
  t: any;
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
    var refresh = localStorage.getItem("refresh");
    if (access && refresh) {
      AccountProxy.refreshAccessToken(access, refresh).then(
        (refreshAccessTokenResponse) => {
          const { accessToken, refreshToken } = refreshAccessTokenResponse.data;
          this.props.refreshToken({ token: accessToken, refreshToken });
          AccountProxy.getMe().then((getMeResponse) => {
            this.props.onLogIn!({
              ...refreshAccessTokenResponse.data,
              ...getMeResponse.data,
              access_token: accessToken,
              refreshToken,
              userId: getMeResponse.data.id,
            });
            localStorage.setItem("access_token", accessToken);
            localStorage.setItem("refresh", refreshToken);
          });
        }
      );
    }
  }

  onLogIn() {
    AccountProxy.logIn({
      username: this.state.username!,
      password: this.state.password!,
    })
      .then((r) => {
        this.props.onLogIn!({ ...r.data });
        localStorage.setItem("access_token", r.data.access_token);
        localStorage.setItem("refresh", r.data.refreshToken);
      })
      .catch((r) => {
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
          {this.state.loginFailed && (
            <div className="m-1 p-1 alert alert-danger">{t("loginFailed")}</div>
          )}
          <div className="m-1 p-1">
            <label>{t("email")}</label>
            <input
              className="form-control"
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
            <Button
              variant="outline-primary"
              onClick={() => this.onLogIn!()}
              type="submit"
            >
              {t("logIn")}
            </Button>
          </div>
        </div>
      </div>
    );
  }
}

export default withTranslation("loginForm")(
  connect(mapStateToProps, mapDispatchToProps)(LoginForm)
);
