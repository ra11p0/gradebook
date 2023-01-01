import React, { ReactElement, useState } from 'react';
import { Button } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import ChangePasswordModal from './ChangePasswordModal';

function ChangePassword(): ReactElement {
  const [showModal, setShowModal] = useState(false);
  const { t } = useTranslation('settings');
  return (
    <>
      <Button test-id="changeSchoolButton" onClick={() => setShowModal(true)}>
        {t('changePassword')}
      </Button>
      <ChangePasswordModal
        show={showModal}
        onHide={() => setShowModal(false)}
      />
    </>
  );
}

export default ChangePassword;
