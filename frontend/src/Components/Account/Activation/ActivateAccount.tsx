import React from 'react';
import { Button, Card, Col, Row } from 'react-bootstrap';
import { withTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import getIsLoggedInReduxProxy from '../../../Redux/ReduxQueries/account/getIsLoggedInRedux';
import getIsUserActivatedReduxProxy from '../../../Redux/ReduxQueries/account/getIsUserActivatedRedux';
import ActivateAdministrator from './ActivateAdministrator';
import { ActivateAdministratorPersonValues } from './ActivateAdministratorPerson';
import ActivateWithCode from './ActivateWithCode';

enum ActivateWith {
  Code,
  New,
  Unset,
}

interface ActivateAccountProps {
  t: any;
  isUserLoggedIn: boolean;
  isUserActivated: boolean;
  onSubmit?: () => void;
  person?: ActivateAdministratorPersonValues;
}

interface ActivateAccountState {
  activateWith?: ActivateWith;
}

class ActivateAccount extends React.Component<
  ActivateAccountProps,
  ActivateAccountState
> {
  constructor(props: ActivateAccountProps) {
    super(props);
    this.state = {
      activateWith: ActivateWith.Unset,
    };
  }

  render(): React.ReactNode {
    const { t } = this.props;

    const defaultOnBackHandler = (): void => {
      this.setState({
        ...this.state,
        activateWith: ActivateWith.Unset,
      });
    };

    return (
      <>
        <Card className="m-3">
          <Card.Body>
            <div>
              <div className="display-6">{t('activateToUseGradebook')}</div>
              <Row className="m-lg-3 p-lg-3 m-sm-1 p-sm-1">
                <Col lg={1} md={2} xs={0} />
                <Col className="text-center">
                  {this.state.activateWith === ActivateWith.Unset && (
                    <>
                      <Row className="text-center">
                        <div>{t('activationMethod')}</div>
                      </Row>
                      <Row>
                        <Col xs={12} lg={8} className={'mx-auto '}>
                          <Row className="children-m-1">
                            <Col>
                              <Button
                                className="fs-3 p-3 activateStudent w-100"
                                variant="outline-secondary"
                                onClick={() =>
                                  this.setState({
                                    ...this.state,
                                    activateWith: ActivateWith.Code,
                                  })
                                }
                              >
                                {t('ActivateWithCode')}
                              </Button>
                            </Col>

                            <Col>
                              <Button
                                className="fs-3 p-3 activateAdministrator w-100"
                                variant="outline-secondary"
                                onClick={() =>
                                  this.setState({
                                    ...this.state,
                                    activateWith: ActivateWith.New,
                                  })
                                }
                              >
                                {t('administrator')}
                              </Button>
                            </Col>
                          </Row>
                        </Col>
                      </Row>
                    </>
                  )}
                  {this.state.activateWith === ActivateWith.New && (
                    <ActivateAdministrator
                      defaultOnBackHandler={defaultOnBackHandler}
                      onSubmit={this.props.onSubmit}
                      person={this.props.person}
                    />
                  )}
                  {this.state.activateWith === ActivateWith.Code && (
                    <ActivateWithCode
                      defaultOnBackHandler={defaultOnBackHandler}
                      onSubmit={this.props.onSubmit}
                    />
                  )}
                </Col>
                <Col lg={1} md={2} xs={0} />
              </Row>
            </div>
          </Card.Body>
        </Card>
      </>
    );
  }
}

export default connect(
  (state: any) => ({
    isUserLoggedIn: getIsLoggedInReduxProxy(state),
    isUserActivated: getIsUserActivatedReduxProxy(state),
  }),
  () => ({})
)(withTranslation('activateAccount')(ActivateAccount));
