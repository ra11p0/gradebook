import React, { ReactElement, useState } from 'react';
import { Button } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import LoadingScreen from '../../Shared/LoadingScreen';
import ConfigureEducationCycleModal from './ConfigureEducationCycleModal';

interface Props {
  onSubmit: () => void;
  educationCycleGuid?: string;
  classGuid: string;
}

function ConfigureEducationCycle(props: Props): ReactElement {
  const { t } = useTranslation('configureEducationCycleForm');
  const [isModalVisible, setIsModalVisible] = useState(false);
  return (
    <>
      <LoadingScreen isReady={!!props.educationCycleGuid}>
        <>
          <ConfigureEducationCycleModal
            classGuid={props.classGuid}
            educationCycleGuid={props.educationCycleGuid!}
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
      </LoadingScreen>
    </>
  );
}

export default ConfigureEducationCycle;
