import React from 'react';
import { withTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import getCurrentPersonReduxProxy, {
  CurrentPersonProxyResult,
} from '../../../Redux/ReduxQueries/account/getCurrentPersonRedux';
import getIsLoggedInReduxProxy from '../../../Redux/ReduxQueries/account/getIsLoggedInRedux';
import getIsUserActivatedReduxProxy from '../../../Redux/ReduxQueries/account/getIsUserActivatedRedux';
import LoadingScreen from '../LoadingScreen';
import SchoolSelect from './SchoolSelect';
import LanguageSelect from './LanguageSelect';
import { GlobalState } from '../../../store';
import setLogOutRedux from '../../../Redux/ReduxCommands/account/setLogOutRedux';
import { Container, Nav, Navbar, Row, Col } from 'react-bootstrap';

interface HeaderProps {
  isLoggedIn?: boolean;
  currentPerson?: CurrentPersonProxyResult;
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

  async logOut(): Promise<void> {
    await setLogOutRedux();
  }

  render(): React.ReactNode {
    const { t } = this.props;
    return (
      <Navbar className="p-4 bg-grey-light bg-gradient" expand="md">
        <Container fluid>
          <Navbar.Brand>
            <Link to="/" className="text-reset">
              Gradebook
            </Link>
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="navbarScroll" />
          <Navbar.Collapse id="navbarScroll">
            <Nav>
              {this.props.isLoggedIn && (
                <>
                  {this.props.isActive && (
                    <>
                      <LoadingScreen isReady={!!this.props.currentPerson}>
                        <>
                          <Link to="/account/profile" className="nav-link">
                            {`${this.props.currentPerson!.name} ${
                              this.props.currentPerson!.surname
                            }`}
                          </Link>
                        </>
                      </LoadingScreen>

                      <Link to="/dashboard" className="nav-link">
                        {t('dashboard')}
                      </Link>
                    </>
                  )}
                  <Nav.Link onClick={async () => await this.logOut()}>
                    {t('logout')}
                  </Nav.Link>
                </>
              )}
            </Nav>
          </Navbar.Collapse>
          <Row className="">
            <Col>
              <SchoolSelect />
            </Col>
            <Col lg={2} md={12} className="mx-auto">
              <LanguageSelect />
            </Col>
          </Row>
        </Container>
      </Navbar>
    );
  }
}

export default withTranslation('header')(
  connect(
    (state: GlobalState) => ({
      isLoggedIn: getIsLoggedInReduxProxy(state),
      currentPerson: getCurrentPersonReduxProxy(state),
      isActive: getIsUserActivatedReduxProxy(state),
    }),
    () => ({})
  )(Header)
);
