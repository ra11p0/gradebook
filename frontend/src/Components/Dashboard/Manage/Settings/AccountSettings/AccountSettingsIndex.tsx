import { Button } from '@mui/material';
import React, { ReactElement } from 'react';
import { ListGroup } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import AccountProxy from '../../../../../ApiClient/Accounts/AccountsProxy';
import { connect } from 'react-redux';
import getCurrentUserIdReduxProxy from '../../../../../Redux/ReduxQueries/account/getCurrentUserIdRedux';

interface Props {
  currentUserGuid?: string;
}

function AccountSettingsIndex(props: Props): ReactElement {
  const { t } = useTranslation('settings');
  const settings: any = {};
  return (
    <>
      <ListGroup>
        <ListGroup.Item></ListGroup.Item>
      </ListGroup>
      <div className="d-flex justify-content-end m-2 p-2">
        <Button
          className="saveSettingsButton"
          variant="outlined"
          onClick={async () => {
            if (!props.currentUserGuid) return;
            await AccountProxy.settings.setSettings(settings);
            console.dir(settings);
          }}
        >
          {t('save')}
        </Button>
      </div>
    </>
  );
}

export default connect(
  (state) => ({
    currentUserGuid: getCurrentUserIdReduxProxy(state),
  }),
  () => ({})
)(AccountSettingsIndex);
