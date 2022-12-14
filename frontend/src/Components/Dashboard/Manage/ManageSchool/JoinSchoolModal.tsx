import React, { ReactElement } from 'react';
import { Modal } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { CurrentPersonProxyResult } from '../../../../Redux/ReduxQueries/account/getCurrentPersonRedux';
import ActivateAccount from '../../../Account/Activation/ActivateAccount';

interface JoinSchoolModalProps {
  show: boolean;
  onHide: () => void;
  person?: CurrentPersonProxyResult;
}
function JoinSchoolModal(props: JoinSchoolModalProps): ReactElement {
  const { t } = useTranslation('activateAccount');
  return (
    <Modal show={props.show} onHide={props.onHide} size="lg">
      <Modal.Header closeButton>
        <Modal.Title>{t('joinSchool')}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <ActivateAccount
          onSubmit={props.onHide}
          person={
            props.person
              ? { ...props.person, birthday: props.person?.birthday }
              : undefined
          }
        />
      </Modal.Body>
    </Modal>
  );
}

export default JoinSchoolModal;
