import { faLanguage } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import { Dropdown } from "react-bootstrap";
import { withTranslation } from "react-i18next";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { logOutWrapper } from "../../ReduxWrappers/logOutWrapper";
import SchoolSelect from "./SchoolSelect";

const mapStateToProps = (state: any) => {
  return {
    isLoggedIn: state.common.isLoggedIn,
    username: state.common.session?.username,
    name: state.common.session?.name,
    surname: state.common.session?.surname,
    isActive: state.common.session?.roles.length != 0,
  };
};

const mapDispatchToProps = (dispatch: any) => ({
  logOutHandler: () => logOutWrapper(dispatch),
});

interface HeaderProps {
  isLoggedIn?: boolean;
  logOutHandler?: () => void;
  username: string;
  name: string;
  surname: string;
  isActive: boolean;
  i18n: any;
  t: any;
}

interface HeaderState {
  isLoggedIn?: boolean;
}

class Header extends React.Component<HeaderProps, HeaderState> {
  constructor(props: HeaderProps) {
    super(props);
    this.state = {
      isLoggedIn: props.isLoggedIn,
    };
  }
  logOut(): void {
    this.props.logOutHandler!();
  }
  render(): React.ReactNode {
    const { i18n, t } = this.props;
    return (
      <header className="p-4 bg-grey-light bg-gradient">
        <div className="d-flex justify-content-between">
          <Link to="/" className="text-dark display-6 text-decoration-none my-auto">
            Gradebook
          </Link>
          <div className="my-auto d-flex gap-2">
            {this.props.isLoggedIn && (
              <div className="d-flex gap-2">
                {this.props.isActive && (
                  <>
                    <div>
                      <SchoolSelect />
                    </div>

                    <div className="my-auto">
                      <Link to="/account/profile" className="btn btn-outline-primary">
                        {`${this.props.name} ${this.props.surname}`}
                      </Link>
                    </div>
                    <div className="my-auto">
                      <Link to="/dashboard" className="btn btn-outline-primary">
                        {t("dashboard")}
                      </Link>
                    </div>
                  </>
                )}
                <div className="my-auto">
                  <a className="btn btn-outline-primary" onClick={() => this.logOut()}>
                    {t("logout")}
                  </a>
                </div>
              </div>
            )}
            <div className="d-flex gap-2 my-auto">
              <Dropdown>
                <Dropdown.Toggle variant="outline-secondary">
                  <FontAwesomeIcon icon={faLanguage} />
                </Dropdown.Toggle>
                <Dropdown.Menu>
                  <Dropdown.Item onClick={() => i18n.changeLanguage("pl")}>{t("polish")} (Polish)</Dropdown.Item>
                  <Dropdown.Item onClick={() => i18n.changeLanguage("en")}>{t("english")} (English)</Dropdown.Item>
                </Dropdown.Menu>
              </Dropdown>
            </div>
          </div>
        </div>
      </header>
    );
  }
}

export default withTranslation("header")(connect(mapStateToProps, mapDispatchToProps)(Header));
