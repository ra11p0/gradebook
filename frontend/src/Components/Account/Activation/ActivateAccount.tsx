import React from "react";
import { connect } from "react-redux";
import { withTranslation } from "react-i18next";
import { Button, Card, Col, Row } from "react-bootstrap";
import ActivateStudent from "./ActivateStudent";
import ActivateTeacher from "./ActivateTeacher";
import ActivateAdministrator from "./ActivateAdministrator";
import getIsUserActivatedReduxProxy from "../../../Redux/ReduxQueries/account/getIsUserActivatedRedux";
import getIsLoggedInReduxProxy from "../../../Redux/ReduxQueries/account/getIsLoggedInRedux";
import { ActivateAdministratorPersonValues } from "./ActivateAdministratorPerson";

interface ActivateAccountProps {
  t: any;
  isUserLoggedIn: boolean;
  isUserActivated: boolean;
  onSubmit?: () => void;
  person?: ActivateAdministratorPersonValues;
}

interface ActivateAccountState {
  role?: string;
}

class ActivateAccount extends React.Component<ActivateAccountProps, ActivateAccountState> {
  constructor(props: ActivateAccountProps) {
    super(props);
    this.state = {
      role: undefined,
    };
  }
  render(): React.ReactNode {
    const { t } = this.props;

    const defaultOnBackHandler = () => {
      this.setState({
        ...this.state,
        role: undefined,
      });
    };

    return (
      <>
        <Card className="m-3">
          <Card.Body>
            <div>
              <div className="display-6">{t("activateToUseGradebook")}</div>
              <Row className="m-3 p-3">
                <Col lg={1} md={3} />
                <Col className="text-center">
                  {!this.state.role && (
                    <>
                      <Row className="text-center">
                        <div>{t("iAmA...")}</div>
                      </Row>
                      <Row>
                        <Col>
                          <Button
                            className="fs-3 m-3 p-3 activateStudent"
                            variant="outline-secondary"
                            onClick={() =>
                              this.setState({
                                ...this.state,
                                role: "student",
                              })
                            }
                          >
                            {t("student")}
                          </Button>
                          <Button
                            className="fs-3 m-3 p-3 activateTeacher"
                            variant="outline-secondary"
                            onClick={() =>
                              this.setState({
                                ...this.state,
                                role: "teacher",
                              })
                            }
                          >
                            {t("teacher")}
                          </Button>
                          <Button
                            className="fs-3 m-3 p-3 activateAdministrator"
                            variant="outline-secondary"
                            onClick={() =>
                              this.setState({
                                ...this.state,
                                role: "administrator",
                              })
                            }
                          >
                            {t("administrator")}
                          </Button>
                        </Col>
                      </Row>
                    </>
                  )}
                  {this.state.role === "teacher" && (
                    <ActivateTeacher defaultOnBackHandler={defaultOnBackHandler} onSubmit={this.props.onSubmit} />
                  )}
                  {this.state.role === "administrator" && (
                    <ActivateAdministrator
                      defaultOnBackHandler={defaultOnBackHandler}
                      onSubmit={this.props.onSubmit}
                      person={this.props.person}
                    />
                  )}
                  {this.state.role === "student" && (
                    <ActivateStudent defaultOnBackHandler={defaultOnBackHandler} onSubmit={this.props.onSubmit} />
                  )}
                </Col>
                <Col lg={1} md={3} />
              </Row>
            </div>
          </Card.Body>
        </Card>
      </>
    );
  }
}

export default withTranslation("activateAccount")(
  connect(
    (state: any) => ({
      isUserLoggedIn: getIsLoggedInReduxProxy(state),
      isUserActivated: getIsUserActivatedReduxProxy(state),
    }),
    (dispatch: any) => ({})
  )(ActivateAccount)
);
