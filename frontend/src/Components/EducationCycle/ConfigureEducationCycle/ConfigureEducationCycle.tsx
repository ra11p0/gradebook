import React, { ReactElement, useState } from 'react';
import { Button } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import ConfigureEducationCycleModal from './ConfigureEducationCycleModal';

interface Props {
  onSubmit: () => void;
  educationCycleGuid: string;
}

function ConfigureEducationCycle(props: Props): ReactElement {
  const { t } = useTranslation('configureEducationCycleForm');
  const [isModalVisible, setIsModalVisible] = useState(false);
  return (
    <>
      <ConfigureEducationCycleModal
        educationCycleGuid={props.educationCycleGuid}
        isModalVisible={isModalVisible}
        onHide={() => {
          setIsModalVisible(false);
        }}
        onSubmit={props.onSubmit}
      />
      <Button
        onClick={() => {
          setIsModalVisible(true);
        }}
      >
        {t('configureEducationCycle')}
      </Button>
    </>
  );
}

export default ConfigureEducationCycle;
