import { faWarning } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { ReactElement, useEffect, useState } from 'react';
import { Alert, Card } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import EducationCyclesInClassResponse from '../../ApiClient/Classes/Definitions/Responses/EducationCyclesInClassResponse';
import EducationCyclesProxy from '../../ApiClient/EducationCycles/EducationCyclesProxy';
import Notifications from '../../Notifications/Notifications';
import ConfigureEducationCycle from '../EducationCycle/ConfigureEducationCycle/ConfigureEducationCycle';
import EducationCyclePicker from '../Shared/EducationCyclePicker/EducationCyclePicker';
import LoadingScreen from '../Shared/LoadingScreen';

interface Props {
  classGuid: string;
}

function EducationCycle({ classGuid }: Props): ReactElement {
  const { t } = useTranslation('educationCycles');
  const [refreshKey, setRefreshKey] = useState(0);
  const [educationCycle, setEducationCycle] = useState<
    EducationCyclesInClassResponse | undefined
  >(undefined);
  useEffect(() => {
    void (async () => {
      await ClassesProxy.educationCycles
        .getEducationCyclesInClass(classGuid)
        .then((resp) => {
          setEducationCycle(resp.data);
        })
        .catch(Notifications.showApiError);
    })();
  }, [refreshKey]);

  return (
    <Card>
      <Card.Header>
        <Card.Title>
          {`${t('educationCycle')} ${
            educationCycle?.activeEducationCycle?.name
              ? ' - ' + educationCycle?.activeEducationCycle?.name
              : ''
          }`}
        </Card.Title>
      </Card.Header>
      <Card.Body>
        <LoadingScreen isReady={!!educationCycle}>
          <>
            {educationCycle?.activeEducationCycle ? (
              <>
                {educationCycle?.activeEducationCycleInstance ? (
                  <>education cycle configured</>
                ) : (
                  <>
                    <Alert variant="warning">
                      <div className="d-flex justify-content-between">
                        <div>{t('educationCycleNotConfigured')}</div>
                        <FontAwesomeIcon icon={faWarning} />
                      </div>
                      <div className="d-flex justify-content-end">
                        <ConfigureEducationCycle
                          classGuid={classGuid}
                          onSubmit={() => {}}
                          educationCycleGuid={
                            educationCycle.activeEducationCycle.guid
                          }
                        />
                      </div>
                    </Alert>
                  </>
                )}
              </>
            ) : (
              <>
                <Alert variant="warning">
                  <div className="d-flex justify-content-between">
                    <div>{t('educationCycleNotAttached')}</div>
                    <FontAwesomeIcon icon={faWarning} />
                  </div>
                  <div className="d-flex justify-content-end">
                    <EducationCyclePicker
                      onCyclesSelected={async (selected) => {
                        const selectedOne = selected.find(() => true);
                        if (!selectedOne) return;
                        await EducationCyclesProxy.setEducationCycleForClass(
                          selectedOne,
                          classGuid
                        );
                        setRefreshKey((e) => e + 1);
                      }}
                      onlyOne={true}
                    />
                  </div>
                </Alert>
              </>
            )}
          </>
        </LoadingScreen>
      </Card.Body>
    </Card>
  );
}

export default EducationCycle;
