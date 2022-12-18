import React, { ReactElement, useEffect, useState } from 'react';
import { Card } from 'react-bootstrap';
import ClassesProxy from '../../../ApiClient/Classes/ClassesProxy';
import EducationCyclesInClassResponse, {
  EducationCycleStepInstance,
} from '../../../ApiClient/Classes/Definitions/Responses/EducationCyclesInClassResponse';
import Notifications from '../../../Notifications/Notifications';
import LoadingScreen from '../../Shared/LoadingScreen';
import EducationCycleHeader from './EducationCycleHeader';
import EducationCycleNotConfigured from './EducationCycleNotConfigured';
import EducationCycleNotAttached from './EducationCycleNotAttached';
import EducationCycleStepInstances from './EducationCycleStepInstances';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
import { useTranslation } from 'react-i18next';

interface Props {
  classGuid: string;
}

function EducationCycle({ classGuid }: Props): ReactElement {
  const [educationCycle, setEducationCycle] = useState<
    EducationCyclesInClassResponse | undefined
  >(undefined);
  const { t } = useTranslation('educationCycles');

  const prepareEducationCycle = async (): Promise<void> => {
    await ClassesProxy.educationCycles
      .getEducationCyclesInClass(classGuid)
      .then((resp) => {
        setEducationCycle(resp.data);
      })
      .catch(Notifications.showApiError);
  };

  const getCurrentAndSurroundingStepInstances = ():
    | undefined
    | {
        current?: EducationCycleStepInstance;
        previous?: EducationCycleStepInstance;
        next?: EducationCycleStepInstance;
      } => {
    return {
      current: educationCycle?.currentStepInstance,
      previous: educationCycle?.previousStepInstance,
      next: educationCycle?.nextStepInstance,
    };
  };

  useEffect(() => {
    void (async () => {
      await prepareEducationCycle();
    })();
  }, []);

  return (
    <Card>
      <Card.Header>
        <Card.Title>
          <EducationCycleHeader
            educationCycle={educationCycle}
            classGuid={classGuid}
            prepareEducationCycle={prepareEducationCycle}
          />
        </Card.Title>
      </Card.Header>
      <Card.Body>
        <LoadingScreen isReady={!!educationCycle}>
          <>
            {educationCycle?.activeEducationCycle ? (
              <>
                {educationCycle?.activeEducationCycleInstance ? (
                  <>
                    {educationCycle.activeEducationCycleInstance &&
                    !educationCycle.currentStepInstance &&
                    !educationCycle.nextStepInstance &&
                    !educationCycle.previousStepInstance ? (
                      <p className="text-success">
                        <FontAwesomeIcon icon={faCheck} />{' '}
                        {t('finishedEducationCycle')}
                      </p>
                    ) : (
                      <>
                        <EducationCycleStepInstances
                          getCurrentAndSurroundingStepInstances={
                            getCurrentAndSurroundingStepInstances
                          }
                          prepareEducationCycle={prepareEducationCycle}
                          classGuid={classGuid}
                        />
                      </>
                    )}
                  </>
                ) : (
                  <EducationCycleNotConfigured
                    classGuid={classGuid}
                    prepareEducationCycle={prepareEducationCycle}
                    educationCycle={educationCycle}
                  />
                )}
              </>
            ) : (
              <EducationCycleNotAttached
                classGuid={classGuid}
                prepareEducationCycle={prepareEducationCycle}
              />
            )}
          </>
        </LoadingScreen>
      </Card.Body>
    </Card>
  );
}

export default EducationCycle;
