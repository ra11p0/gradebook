import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Tippy from '@tippyjs/react';
import React, { ReactElement, useEffect, useState } from 'react';
import { Alert, Button, Card, Stack } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import Swal from 'sweetalert2';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import EducationCyclesInClassResponse, {
  EducationCycleStepInstance,
} from '../../ApiClient/Classes/Definitions/Responses/EducationCyclesInClassResponse';
import EducationCyclesProxy from '../../ApiClient/EducationCycles/EducationCyclesProxy';
import Notifications from '../../Notifications/Notifications';
import ConfigureEducationCycle from '../EducationCycle/ConfigureEducationCycle/ConfigureEducationCycle';
import EducationCyclePicker from '../Shared/EducationCyclePicker/EducationCyclePicker';
import LoadingScreen from '../Shared/LoadingScreen';
import moment from 'moment';
import EducationCycleStepInstanceCurrent from './EducationCycleStepInstanceCurrent';
import EducationCycleStepInstanceSmall from './EducationCycleStepInstanceSmall';

interface Props {
  classGuid: string;
}

function EducationCycle({ classGuid }: Props): ReactElement {
  const { t } = useTranslation('educationCycles');

  const [educationCycle, setEducationCycle] = useState<
    EducationCyclesInClassResponse | undefined
  >(undefined);

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
          <div className="d-flex gap-2 justify-content-between m-1 p-1">
            <Stack className="my-auto">
              {`${t('educationCycle')} ${
                educationCycle?.activeEducationCycle?.name
                  ? ' - ' + educationCycle?.activeEducationCycle?.name
                  : ''
              }`}
              {educationCycle?.activeEducationCycleInstance && (
                <small>
                  {`${moment
                    .utc(educationCycle.activeEducationCycleInstance.dateSince)
                    .local()
                    .format('ll')}
                         - 
                      ${moment
                        .utc(
                          educationCycle.activeEducationCycleInstance.dateUntil
                        )
                        .local()
                        .format('ll')}`}
                </small>
              )}
            </Stack>

            {educationCycle?.activeEducationCycle && (
              <div>
                <Tippy
                  content={t('removeEducationCycleBinding')}
                  arrow={true}
                  animation={'scale'}
                >
                  <Button
                    variant="danger"
                    onClick={async () => {
                      await Swal.fire({
                        showLoaderOnConfirm: true,
                        title: t('unbindEducationCycle'),
                        text: t('youSureUnbindEducationCycle'),
                        showDenyButton: true,
                        showCancelButton: false,
                        confirmButtonText: t('yes'),
                        denyButtonText: t('no'),
                        icon: 'warning',
                        preConfirm: async () => {
                          await ClassesProxy.educationCycles
                            .deleteActiveEducationCycleFromClass(classGuid)
                            .catch(Notifications.showApiError);
                        },
                      }).then(async (resp) => {
                        if (resp.isConfirmed) await prepareEducationCycle();
                      });
                    }}
                  >
                    <FontAwesomeIcon icon={faTrash} />
                  </Button>
                </Tippy>
              </div>
            )}
          </div>
        </Card.Title>
      </Card.Header>
      <Card.Body>
        <LoadingScreen isReady={!!educationCycle}>
          <>
            {educationCycle?.activeEducationCycle ? (
              <>
                {educationCycle?.activeEducationCycleInstance ? (
                  <>
                    <Stack>
                      <div>
                        <Stack>
                          {getCurrentAndSurroundingStepInstances()
                            ?.previous && (
                            <EducationCycleStepInstanceSmall
                              {...getCurrentAndSurroundingStepInstances()!
                                .previous!}
                            />
                          )}
                          {getCurrentAndSurroundingStepInstances()?.current && (
                            <EducationCycleStepInstanceCurrent
                              {...getCurrentAndSurroundingStepInstances()!
                                .current!}
                              isLast={
                                !getCurrentAndSurroundingStepInstances()?.next
                              }
                              stateChanged={async () => {
                                await prepareEducationCycle();
                              }}
                              classGuid={classGuid}
                            />
                          )}
                          {getCurrentAndSurroundingStepInstances()?.next && (
                            <EducationCycleStepInstanceSmall
                              {...getCurrentAndSurroundingStepInstances()!
                                .next!}
                            />
                          )}
                        </Stack>
                      </div>
                    </Stack>
                  </>
                ) : (
                  <>
                    <Alert variant="warning">
                      <div className="d-flex justify-content-between">
                        <div>{t('educationCycleNotConfigured')}</div>
                      </div>
                      <div className="d-flex justify-content-end m-1 p-1">
                        <ConfigureEducationCycle
                          classGuid={classGuid}
                          onSubmit={async () => {
                            await prepareEducationCycle();
                          }}
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
                  </div>
                  <div className="d-flex justify-content-end m-1 p-1">
                    <EducationCyclePicker
                      onCyclesSelected={async (selected) => {
                        const selectedOne = selected.find(() => true);
                        if (!selectedOne) return;
                        await EducationCyclesProxy.setEducationCycleForClass(
                          selectedOne,
                          classGuid
                        );
                        await prepareEducationCycle();
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
