import React, { ReactElement } from 'react';
import { Modal } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import getIsLoggedInRedux from '../../../Redux/ReduxQueries/account/getIsLoggedInRedux';
import { GlobalState } from '../../../store';
import ChangePasswordLoggedIn from './ChangePasswordLoggedIn';
import ChangePasswordNotLoggedIn from './ChangePasswordNotLoggedIn';

interface Props {
  show: boolean;
  onHide: () => void;
  isLoggedIn: boolean;
}

function ChangePasswordModal(props: Props): ReactElement {
  const { t } = useTranslation('changePassword');
  return (
    <Modal show={props.show} onHide={props.onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{t('changePasswordModalTitle')}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {props.isLoggedIn && <ChangePasswordLoggedIn />}
        {!props.isLoggedIn && (
          <ChangePasswordNotLoggedIn
            onRequestCompleted={() => {
              props.onHide();
            }}
          />
        )}
      </Modal.Body>
    </Modal>
  );
}

export default connect(
  (state: GlobalState) => ({
    isLoggedIn: getIsLoggedInRedux(state),
  }),
  () => ({})
)(ChangePasswordModal);
