import React from 'react';
import { Button, Container, Nav, Navbar } from 'react-bootstrap';
import { withTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import setLogOutRedux from '../../../Redux/ReduxCommands/account/setLogOutRedux';
import getCurrentPersonReduxProxy, {
  CurrentPersonProxyResult,
} from '../../../Redux/ReduxQueries/account/getCurrentPersonRedux';
import getIsLoggedInReduxProxy from '../../../Redux/ReduxQueries/account/getIsLoggedInRedux';
import getIsUserActivatedReduxProxy from '../../../Redux/ReduxQueries/account/getIsUserActivatedRedux';
import { GlobalState } from '../../../store';
import LoadingScreen from '../LoadingScreen';
import LanguageSelect from './LanguageSelect';
import SchoolSelect from './SchoolSelect';

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
      <div className="py-4 px-2 bg-grey-light bg-gradient">
        <Navbar expand="lg">
          <Container fluid>
            <Navbar.Brand data-testid="brand">
              <Link to="/" className="text-reset">
                Gradebook
              </Link>
            </Navbar.Brand>
            <Navbar.Toggle aria-controls="navbarScroll" />
            <Navbar.Collapse id="navbarScroll" className="">
              <Nav>
                {this.props.isLoggedIn && (
                  <>
                    {this.props.isActive && (
                      <>
                        <Link to="/dashboard" className="nav-link">
                          {t('dashboard')}
                        </Link>
                        <LoadingScreen isReady={!!this.props.currentPerson}>
                          {this.props.currentPerson && (
                            <Link to="/account/profile" className="nav-link">
                              {`${this.props.currentPerson.name} ${this.props.currentPerson.surname}`}
                            </Link>
                          )}
                        </LoadingScreen>
                      </>
                    )}
                  </>
                )}
              </Nav>
              <div className="ms-auto d-flex justify-content-center flex-wrap gap-3">
                {this.props.isLoggedIn && this.props.isActive && (
                  <div className="w-auto">
                    <SchoolSelect />
                  </div>
                )}
                <div>
                  <LanguageSelect />
                </div>
                {this.props.isLoggedIn && (
                  <div className="w-auto">
                    <Button
                      id="logOutButton"
                      variant="outline-danger"
                      onClick={async () => await this.logOut()}
                    >
                      {t('logout')}
                    </Button>
                  </div>
                )}
              </div>
            </Navbar.Collapse>
          </Container>
        </Navbar>
      </div>
    );
  }
}

export default connect(
  (state: GlobalState) => ({
    isLoggedIn: getIsLoggedInReduxProxy(state),
    currentPerson: getCurrentPersonReduxProxy(state),
    isActive: getIsUserActivatedReduxProxy(state),
  }),
  () => ({})
)(withTranslation('header')(Header));
