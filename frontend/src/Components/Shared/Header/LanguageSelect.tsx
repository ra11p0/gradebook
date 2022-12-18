import { connect } from 'react-redux';
import React, { ReactElement } from 'react';
import { GlobalState } from '../../../store';
import getIsLoggedInRedux from '../../../Redux/ReduxQueries/account/getIsLoggedInRedux';
import { Dropdown } from 'react-bootstrap';
import getApplicationLanguageRedux from '../../../Redux/ReduxQueries/account/getApplicationLanguageRedux';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck, faLanguage } from '@fortawesome/free-solid-svg-icons';
import setApplicationLanguageRedux from '../../../Redux/ReduxCommands/account/setApplicationLanguageRedux';
import AccountsProxy from '../../../ApiClient/Accounts/AccountsProxy';
import { useTranslation } from 'react-i18next';

interface Props {
  isUserLoggedIn: boolean;
  language?: string;
}

function LanguageSelect(props: Props): ReactElement {
  const { t } = useTranslation('header');
  const setLanguage = async (language: string): Promise<void> => {
    await setApplicationLanguageRedux(language);
    if (props.isUserLoggedIn)
      await AccountsProxy.settings.setLanguage(language);
  };
  return (
    <>
      <Dropdown drop="start">
        <Dropdown.Toggle variant="outline-secondary">
          <FontAwesomeIcon icon={faLanguage} />
        </Dropdown.Toggle>
        <Dropdown.Menu>
          <Dropdown.Item onClick={async () => await setLanguage('pl')}>
            {t('polish')} (Polish)
            {props.language === 'pl' && <FontAwesomeIcon icon={faCheck} />}
          </Dropdown.Item>
          <Dropdown.Item onClick={async () => await setLanguage('en')}>
            {t('english')} (English)
            {props.language === 'en' && <FontAwesomeIcon icon={faCheck} />}
          </Dropdown.Item>
        </Dropdown.Menu>
      </Dropdown>
    </>
  );
}

export default connect(
  (state: GlobalState) => ({
    isUserLoggedIn: getIsLoggedInRedux(state),
    language: getApplicationLanguageRedux(state),
  }),
  () => ({})
)(LanguageSelect);
