import React, { ReactElement } from 'react';
import { Button } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import NotificationsHub from '../../../../ApiClient/SignalR/NotificationsHub/NotificationsHub';
import AddNewStudentModal from './AddNewStudentModal';

interface Props {
  setShowAddStudentModal: (visible: boolean) => void;
  showAddStudentModal: boolean;
}

function AddNewStudentModalWithButton(props: Props): ReactElement {
  const { t } = useTranslation('studentsList');
  return (
    <>
      <AddNewStudentModal
        show={props.showAddStudentModal}
        onHide={() => {
          props.setShowAddStudentModal(false);
        }}
      />
      <Button
        onClick={async () => {
          props.setShowAddStudentModal(true);
          await NotificationsHub.sendNotification();
        }}
        className="addNewStudentButton"
      >
        {t('addStudent')}
      </Button>
    </>
  );
}

export default AddNewStudentModalWithButton;
