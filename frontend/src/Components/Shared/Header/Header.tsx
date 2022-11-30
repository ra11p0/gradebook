import React from 'react';
import { withTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import getApplicationLanguageReduxProxy from '../../../Redux/ReduxQueries/account/getApplicationLanguageRedux';
import getCurrentPersonReduxProxy, {
  CurrentPersonProxyResult,
} from '../../../Redux/ReduxQueries/account/getCurrentPersonRedux';
import getIsLoggedInReduxProxy from '../../../Redux/ReduxQueries/account/getIsLoggedInRedux';
import getIsUserActivatedReduxProxy from '../../../Redux/ReduxQueries/account/getIsUserActivatedRedux';
import setLogOutReduxWrapper from '../../../Redux/ReduxCommands/account/setLogOutRedux';
import LoadingScreen from '../LoadingScreen';
import SchoolSelect from './SchoolSelect';
import LanguageSelect from './LanguageSelect';

interface HeaderProps {
  isLoggedIn?: boolean;
  logOutHandler?: () => void;
  currentPerson?: CurrentPersonProxyResult;
  isActive: boolean;
  i18n: any;
  t: any;
  language: string;
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
    const { t } = this.props;
    return (
      <header className="p-4 bg-grey-light bg-gradient">
        <div className="d-flex justify-content-between">
          <Link
            to="/"
            className="text-dark display-6 text-decoration-none my-auto"
          >
            Gradebook
          </Link>
          <div className="my-auto d-flex gap-2">
            {this.props.isLoggedIn && (
              <div className="d-flex gap-2">
                {this.props.isActive && (
                  <>
                    <LoadingScreen isReady={!!this.props.currentPerson}>
                      <>
                        <div>
                          <SchoolSelect />
                        </div>

                        <div className="my-auto">
                          <Link
                            to="/account/profile"
                            className="btn btn-outline-primary"
                          >
                            {`${this.props.currentPerson!.name} ${
                              this.props.currentPerson!.surname
                            }`}
                          </Link>
                        </div>
                      </>
                    </LoadingScreen>

                    <div className="my-auto">
                      <Link to="/dashboard" className="btn btn-outline-primary">
                        {t('dashboard')}
                      </Link>
                    </div>
                  </>
                )}
                <div className="my-auto">
                  <a
                    className="btn btn-outline-primary logoutButton"
                    onClick={() => this.logOut()}
                  >
                    {t('logout')}
                  </a>
                </div>
              </div>
            )}
            <div className="d-flex gap-2 my-auto">
              <LanguageSelect />
            </div>
          </div>
        </div>
      </header>
    );
  }
}

export default withTranslation('header')(
  connect(
    (state) => ({
      isLoggedIn: getIsLoggedInReduxProxy(state),
      currentPerson: getCurrentPersonReduxProxy(state),
      isActive: getIsUserActivatedReduxProxy(state),
      language: getApplicationLanguageReduxProxy(state),
    }),
    (dispatch) => ({
      logOutHandler: () => setLogOutReduxWrapper(dispatch),
    })
  )(Header)
);
