import React, { ReactElement, useState } from 'react';
import { Button } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import NewSubjectModal from './NewSubjectModal';

interface Props {
  onHide: () => void;
}

function NewSubjectModalWithButton(props: Props): ReactElement {
  const [showNewSubjectModal, setShowNewSubjectModal] = useState(false);
  const { t } = useTranslation('subjects');
  return (
    <div>
      <Button
        onClick={() => {
          setShowNewSubjectModal(true);
        }}
      >
        {t('newSubject')}
      </Button>
      <NewSubjectModal
        show={showNewSubjectModal}
        onHide={() => {
          setShowNewSubjectModal(false);
          props.onHide();
        }}
      />
    </div>
  );
}

export default NewSubjectModalWithButton;
