import React from "react";
import { connect } from "react-redux";
import { withTranslation } from "react-i18next";
import { Button, Card, Col, Row } from "react-bootstrap";
import RegisterStudent from "./RegisterStudent";
import RegisterTeacher from "./RegisterTeacher";
import RegisterAdministrator from "./RegisterAdministrator";
import { isUserActivatedProxy } from "../../ReduxProxy/isUserAcrivatedProxy";
import { isLoggedInProxy } from "../../ReduxProxy/isLoggedInProxy";

const mapStateToProps = (state: any) => ({
  isUserLoggedIn: isLoggedInProxy(state),
  isUserActivated: isUserActivatedProxy(state),
});

const mapDispatchToProps = (dispatch: any) => ({});

interface ActivateAccountProps {
  t: any;
  isUserLoggedIn: boolean;
  isUserActivated: boolean;
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
                <Col xs={3} />
                <Col className="text-center">
                  {!this.state.role && (
                    <>
                      <Row className="text-center">
                        <div>{t("iAmA...")}</div>
                      </Row>
                      <Row>
                        <Col>
                          <Button
                            className="fs-3 m-3 p-3"
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
                            className="fs-3 m-3 p-3"
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
                            className="fs-3 m-3 p-3"
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
                  {this.state.role === "teacher" && <RegisterTeacher defaultOnBackHandler={defaultOnBackHandler} />}
                  {this.state.role === "administrator" && <RegisterAdministrator defaultOnBackHandler={defaultOnBackHandler} />}
                  {this.state.role === "student" && <RegisterStudent defaultOnBackHandler={defaultOnBackHandler} />}
                </Col>
                <Col xs={3} />
              </Row>
            </div>
          </Card.Body>
        </Card>
      </>
    );
  }
}

export default withTranslation("activateAccount")(connect(mapStateToProps, mapDispatchToProps)(ActivateAccount));
