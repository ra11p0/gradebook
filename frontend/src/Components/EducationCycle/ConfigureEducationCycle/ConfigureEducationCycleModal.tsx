import React, { ReactElement, useEffect, useState } from 'react';
import { Modal } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import LoadingScreen from '../../Shared/LoadingScreen';
import ConfigureEducationCycleForm from './ConfigureEducationCycleForm';

interface Props {
  isModalVisible: boolean;
  onHide: () => void;
  onSubmit: () => void;
  educationCycleGuid: string;
  classGuid: string;
}

function ConfigureEducationCycleModal(props: Props): ReactElement {
  const { t } = useTranslation('configureEducationCycleForm');
  const [educationCycle, setEducationCycle] = useState<string | undefined>(
    undefined
  );

  useEffect(() => {
    void (async () => {
      setEducationCycle('true');
    })();
  }, [props.isModalVisible]);

  return (
    <>
      <Modal show={props.isModalVisible} size={'lg'} onHide={props.onHide}>
        <LoadingScreen isReady={educationCycle !== undefined}>
          <>
            <Modal.Header closeButton>
              {t('configureEducationCycle')}
            </Modal.Header>
            <Modal.Body>
              <ConfigureEducationCycleForm
                educationCycleGuid={props.educationCycleGuid}
                classGuid={props.classGuid}
                onSubmit={() => {
                  props.onSubmit();
                  props.onHide();
                }}
              />
            </Modal.Body>
          </>
        </LoadingScreen>
      </Modal>
    </>
  );
}

export default ConfigureEducationCycleModal;
